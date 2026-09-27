// CoolDeck — user curve profile: the thing that makes a custom curve PERMANENT.
//
// WHY THIS EXISTS
// Writing a curve to the EC is not persistence. The EC keeps it in a table that is mirrored
// from the vendor settings store, and the fan MODE is a separate piece of state that a boot
// can well come back in Automatic with — and in Automatic the EC drives the fan from its own
// internal table, so an uploaded custom table simply has no effect. So "my curve is saved"
// really needs an owner that re-asserts it, and that owner has to be CoolDeck itself.
//
// Format is deliberately a flat key=value text file rather than JSON: no serializer is
// available (no NuGet, csc 4.0 only) and a hand-editable file is friendlier anyway.

using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace CoolDeck
{
    public class FanCurve
    {
        public int[] T = new int[4];
        public int[] D = new int[4];

        public FanPoint[] ToPoints()
        {
            var r = new FanPoint[4];
            for (int i = 0; i < 4; i++) r[i] = new FanPoint(T[i], D[i]);
            return r;
        }

        public void Set(FanPoint[] pts)
        {
            for (int i = 0; i < 4 && i < pts.Length; i++) { T[i] = pts[i].Temp; D[i] = pts[i].Duty; }
        }

        public string Encode()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < 4; i++)
            {
                if (i > 0) sb.Append(',');
                sb.Append(T[i].ToString(CultureInfo.InvariantCulture));
                sb.Append(':');
                sb.Append(D[i].ToString(CultureInfo.InvariantCulture));
            }
            return sb.ToString();
        }

        public void Decode(string s)
        {
            string[] parts = (s ?? "").Split(',');
            for (int i = 0; i < 4 && i < parts.Length; i++)
            {
                string[] td = parts[i].Split(':');
                if (td.Length != 2) continue;
                int t, d;
                if (int.TryParse(td[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out t)) T[i] = t;
                if (int.TryParse(td[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out d)) D[i] = d;
            }
        }
    }

    public class Profile
    {
        public int Mode = 0;
        public int Offset = 0;
        public bool AutoApply = true;
        public FanCurve[] Fans = new FanCurve[4];

        public Profile()
        {
            for (int i = 0; i < 4; i++) Fans[i] = new FanCurve();
        }

        // ---------------- location ----------------

        static string Dir
        {
            get
            {
                return Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CoolDeck");
            }
        }

        public static string FilePath { get { return Path.Combine(Dir, "profile.txt"); } }

        // ---------------- capture / compare ----------------

        public static Profile Capture(Snapshot s)
        {
            var p = new Profile();
            p.Mode = (int)s.Mode;
            p.Offset = s.Offset;
            for (int i = 0; i < 4 && i < s.Curves.Length; i++) p.Fans[i].Set(s.Curves[i]);
            return p;
        }

        /// <summary>True when the EC already looks like this profile, so re-applying can be
        /// skipped. Duties are compared with a 1 % tolerance because the EC round-trips them
        /// through a 0..255 byte.</summary>
        public bool Matches(Snapshot s)
        {
            if (s == null || s.Error != null) return false;
            if ((int)s.Mode != Mode) return false;
            if (s.Offset != Offset) return false;
            for (int i = 0; i < 4 && i < s.Curves.Length; i++)
            {
                for (int p2 = 0; p2 < 4; p2++)
                {
                    if (s.Curves[i][p2].Temp != Fans[i].T[p2]) return false;
                    if (Math.Abs(s.Curves[i][p2].Duty - Fans[i].D[p2]) > 1) return false;
                }
            }
            return true;
        }

        /// <summary>Which fan actually differs — used to only re-upload what is needed, so a
        /// startup re-apply never disturbs a channel that is already correct.</summary>
        public int[] DifferingFans(Snapshot s)
        {
            var list = new System.Collections.Generic.List<int>();
            for (int i = 0; i < 4 && s != null && i < s.Curves.Length; i++)
            {
                for (int p2 = 0; p2 < 4; p2++)
                {
                    if (s.Curves[i][p2].Temp != Fans[i].T[p2] ||
                        Math.Abs(s.Curves[i][p2].Duty - Fans[i].D[p2]) > 1)
                    { list.Add(i); break; }
                }
            }
            int[] r = new int[list.Count];
            for (int i = 0; i < r.Length; i++) r[i] = list[i];
            return r;
        }

        // ---------------- persistence ----------------

        public static Profile Load()
        {
            var p = new Profile();
            try
            {
                string f = FilePath;
                if (!File.Exists(f)) return null;
                foreach (string raw in File.ReadAllLines(f))
                {
                    string line = raw.Trim();
                    if (line.Length == 0 || line.StartsWith("#")) continue;
                    int eq = line.IndexOf('=');
                    if (eq <= 0) continue;
                    string k = line.Substring(0, eq).Trim();
                    string v = line.Substring(eq + 1).Trim();
                    switch (k)
                    {
                        case "mode": int m; if (int.TryParse(v, out m)) p.Mode = m; break;
                        case "offset": int o; if (int.TryParse(v, out o)) p.Offset = o; break;
                        case "autoapply": p.AutoApply = v == "1" || v.Equals("true", StringComparison.OrdinalIgnoreCase); break;
                        case "fan0": case "fan1": case "fan2": case "fan3":
                            p.Fans[k[3] - '0'].Decode(v);
                            break;
                    }
                }
                return p;
            }
            catch { return null; }
        }

        public bool Save()
        {
            try
            {
                Directory.CreateDirectory(Dir);
                var sb = new StringBuilder();
                sb.AppendLine("# CoolDeck fan profile — the curve CoolDeck re-asserts at startup.");
                sb.AppendLine("# Edit by hand if you like; unknown lines are ignored.");
                sb.AppendLine("mode=" + Mode.ToString(CultureInfo.InvariantCulture));
                sb.AppendLine("offset=" + Offset.ToString(CultureInfo.InvariantCulture));
                sb.AppendLine("autoapply=" + (AutoApply ? "1" : "0"));
                string[] names = { "CPU Fan", "GPU Fan", "GPU Fan 2", "Case Fan" };
                for (int i = 0; i < 4; i++)
                    sb.AppendLine("fan" + i + "=" + Fans[i].Encode());   // t:d,t:d,t:d,t:d
                File.WriteAllText(FilePath, sb.ToString());
                return true;
            }
            catch { return false; }
        }

        public string Describe()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < 4; i++)
            {
                if (Fans[i].T[0] == 0 && Fans[i].T[3] == 0) continue;
                sb.Append(Fans[i].Encode());
                if (i < 3) sb.Append("  ");
            }
            return sb.ToString();
        }
    }
}
