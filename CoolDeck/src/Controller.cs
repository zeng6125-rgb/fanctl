// CoolDeck — high-level fan controller.
// Wraps the raw DCHU protocol in a typed, poll-friendly API.

using System;
using System.Globalization;
using System.IO;
using System.Threading;

namespace CoolDeck
{
    public enum FanMode
    {
        Automatic = 0,
        Maximum = 1,
        Noiseless = 2,
        Silent = 3,
        MaxQ = 5,
        Custom = 6,
        NoiselessEx = 8,
        IFSC = 9
    }

    public class FanChannel
    {
        public string Name = "";
        public int RawTach;      // EC tachometer count (period-like, inversely proportional to RPM)
        public int Duty;         // 0..255
        public int TempRaw;      // raw EC temperature code, == degrees Celsius on this firmware
        public bool Present;

        public int Rpm { get { return RawTach <= 0 ? 0 : (int)Math.Round(2156000.0 / RawTach); } }
        public int DutyPct { get { return (int)Math.Round(Duty / 255.0 * 100.0); } }
        public int TempC { get { return TempRaw; } }
    }

    public class FanPoint
    {
        public int Temp;   // deg C
        public int Duty;   // percent 0..100
        public FanPoint(int t, int d) { Temp = t; Duty = d; }
    }

    public class Snapshot
    {
        public FanChannel[] Fans = new FanChannel[4];
        public FanMode Mode = FanMode.Automatic;
        public int Offset;
        public FanPoint[][] Curves = new FanPoint[4][];
        public DateTime At = DateTime.Now;
        public string Error;
        /// <summary>True when this frame reused the previous good reading because the EC
        /// answered with its post-traffic stub body. The UI shows this rather than pretending
        /// the numbers are live.</summary>
        public bool Stale;

        public Snapshot()
        {
            for (int i = 0; i < 4; i++) Fans[i] = new FanChannel();
            for (int i = 0; i < 4; i++) Curves[i] = new FanPoint[4];
        }
    }

    public sealed class Controller : IDisposable
    {
        public const int CmdSensors = 12;
        public const int CmdCurve = 13;
        public const int CmdCurveUpload = 14;
        public const int StoreCurveHi = 4;

        // store4 curve block: 18 bytes per fan, fan0 @16, fan1 @34, fan2 @52, fan3 @70
        const int CurveBlock = 16;
        const int CurveStride = 18;

        public bool Ready { get; private set; }
        public string DllPath { get; private set; }
        public string EcVersion = "?";
        public string EcChip = "?";
        public string Board = "?";

        static readonly string[] FanNames = { "CPU Fan", "GPU Fan", "GPU Fan 2", "Case Fan" };
        static readonly int[] RpmOff = { 2, 4, 6, 36 };
        static readonly int[] DutyOff = { 16, 19, 22, 38 };
        static readonly int[] TempOff = { 18, 21, 24, 40 };

        public Controller() { }

        public string Init()
        {
            DllPath = Native.Init();
            if (DllPath == null) return null;
            Ready = true;

            byte[] v = Native.SetEx(1, 222);
            if (v[0] == 0xFA) EcVersion = Native.Ansi(v, 1, 8).Trim();
            byte[] c = Native.SetEx(3, 222);
            if (c[0] == 0xFA) EcChip = Native.Ansi(c, 1, 8).Trim();
            Board = ReadBoardName();
            return DllPath;
        }

        string ReadBoardName()
        {
            try
            {
                using (var k = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(
                    @"HARDWARE\DESCRIPTION\System\BIOS"))
                {
                    object o = k == null ? null : k.GetValue("SystemProductName");
                    if (o != null) return o.ToString();
                }
            }
            catch { }
            return "?";
        }

        /// <summary>
        /// Sticky fan-presence memory. A single poll is a weak signal: a fan idling at its
        /// floor can read tach 0, which would wrongly hide a real fan. So a channel counts as
        /// present if EITHER its tachometer or its duty byte is non-zero, and once seen it
        /// stays seen for the life of the process.
        /// </summary>
        readonly bool[] _fanSeen = new bool[4];
        public bool FanSeen(int i) { return i >= 0 && i < 4 && _fanSeen[i]; }

        /// <summary>
        /// Last plausible sensor buffer. GetDCHU_Data_Buffer(12) intermittently hands back an
        /// all-zero 256-byte block — same failure already documented for ReadAppSettings — and
        /// a single such poll made the UI flash "1 °C / not present" right after launch. A
        /// fan controller that lies about temperature for one frame is worse than one that
        /// repeats its previous good value, so stale-but-real wins over fresh-but-zero.
        /// </summary>
        byte[] _lastSensors;

        static bool AllZero(byte[] b)
        {
            for (int i = 0; i < b.Length; i++) if (b[i] != 0) return false;
            return true;
        }

        /// <summary>
        /// Recognise the EC's post-traffic stub answer. After heavy DCHU activity cmd 12 keeps
        /// returning rc==12 (success) but the body is a placeholder: both temperatures pinned
        /// near 1 °C and every tach and duty zero, for up to ~10 s. It is NOT all-zero, so a
        /// plain zero test does not catch it — that is what made the UI flash "1 °C / not
        /// present". A real CPU sitting at 1 °C is not a thing.
        /// </summary>
        bool IsStub(byte[] b)
        {
            if (b[TempOff[0]] > 2 || b[TempOff[1]] > 2) return false;
            for (int i = 0; i < 4; i++)
            {
                if (((b[RpmOff[i]] << 8) | b[RpmOff[i] + 1]) != 0) return false;
                if (b[DutyOff[i]] != 0) return false;
            }
            return true;
        }

        /// <summary>
        /// Presence is decided by voting across polls, never from one sample: a channel counts
        /// as live only if at least two separate reads showed tach, duty or a plausible
        /// temperature. Combined with the sticky flag this survives both the stub window above
        /// and a fan legitimately idling at zero tach.
        /// </summary>
        readonly int[] _fanVotes = new int[4];
        const int VotesToConfirm = 2;

        static readonly TimeSpan SlowTtl = TimeSpan.FromSeconds(4);
        DateTime _slowAt = DateTime.MinValue;
        FanMode _slowMode = FanMode.Automatic;
        int _slowOffset;

        public Snapshot Read()
        {
            var s = new Snapshot();
            if (!Ready) { s.Error = "InsydeDCHU.dll not loaded"; return s; }
            DateTime now = DateTime.UtcNow;
            try
            {
                byte[] b = Native.GetBuffer(CmdSensors);
                if (IsStub(b) && _lastSensors != null)
                {
                    // Keep showing the last real reading rather than a lie about temperature.
                    b = _lastSensors;
                    s.Stale = true;
                }
                else if (AllZero(b) && _lastSensors != null)
                {
                    b = _lastSensors;
                    s.Stale = true;
                }
                else if (!IsStub(b) && !AllZero(b))
                {
                    _lastSensors = b;
                }

                for (int i = 0; i < 4; i++)
                {
                    var f = s.Fans[i];
                    f.Name = FanNames[i];
                    f.RawTach = (b[RpmOff[i]] << 8) | b[RpmOff[i] + 1];
                    f.Duty = b[DutyOff[i]];
                    f.TempRaw = b[TempOff[i]];

                    bool signal = f.RawTach > 0 || f.Duty > 0 || f.TempRaw > 5;
                    if (signal && !_fanSeen[i] && _fanVotes[i] < VotesToConfirm) _fanVotes[i]++;
                    if (_fanVotes[i] >= VotesToConfirm) _fanSeen[i] = true;

                    // Once confirmed a fan stays confirmed; a single zero poll means nothing.
                    f.Present = _fanSeen[i];
                }
                s.Curves = ReadCurves();

                // Mode and offset come from the settings store, not the sensor block, and they
                // change rarely — only on a user action or another app writing. The GUI polls
                // at 0.9 s, so refreshing them every tick meant ~4 DCHU round-trips per poll.
                // That matters here: heavy DCHU traffic is exactly what makes the EC answer
                // cmd 12 with its stub body, so an unhurried poll was helping to manufacture
                // the "1 °C / not present" glitch. Caching them cuts the read pressure roughly
                // in half without any visible staleness in practice.
                if (now - _slowAt > SlowTtl)
                {
                    _slowMode = (FanMode)ReadMode();
                    _slowOffset = ReadOffset();
                    _slowAt = now;
                }
                s.Mode = _slowMode;
                s.Offset = _slowOffset;
            }
            catch (Exception ex) { s.Error = ex.Message; }
            return s;
        }

        public int ReadMode()
        {
            byte[] b = Native.ReadStore(4, 5, 1);
            return b.Length > 0 ? b[0] : 0;
        }

        public int ReadOffset()
        {
            byte[] b = Native.ReadStore(4, 7, 1);
            return b.Length > 0 ? b[0] : 0;
        }

        public void SetMode(FanMode m)
        {
            int v = (int)m;
            Native.SetDchu(121, 1, v);                       // live EC channel
            byte[] s = new byte[] { (byte)v };
            Native.WriteStore(4, 5, s);                      // persisted settings store
            _slowMode = m; _slowAt = DateTime.MinValue;   // our own write must show at once
        }

        public void SetOffset(int percent)
        {
            if (percent < 0) percent = 0;
            if (percent > 100) percent = 100;
            int duty = (int)Math.Round(255.0 * percent / 100.0, 0);
            Native.SetDchu(121, 14, duty);
            byte[] s = new byte[] { (byte)percent };
            Native.WriteStore(4, 7, s);
            _slowOffset = percent; _slowAt = DateTime.MinValue;
        }

        public FanPoint[][] ReadCurves()
        {
            byte[] c = Native.GetBuffer(CmdCurve);
            var res = new FanPoint[4][];
            for (int f = 0; f < 4; f++)
            {
                res[f] = new FanPoint[4];
                for (int p = 0; p < 4; p++)
                {
                    int o = CurveBlock + f * 8 + p * 2;
                    res[f][p] = new FanPoint(c[o], (int)Math.Round(c[o + 1] / 255.0 * 100.0));
                }
            }
            return res;
        }

        /// <summary>
        /// Uploads a fan curve to the EC live. This is the real control path — DCHU cmd 14,
        /// a 32-byte payload carrying (t1,d1) and (t2,d2) for up to three fans plus three
        /// signed BE16 slopes per fan. The outer points are owned by the EC, so only the two
        /// middle points are settable — exactly what the stock Fan Speed Setting app allows.
        /// Those EC-owned endpoints differ per fan (CPU 40..100, GPU 48..95), so the settable
        /// window is read back from the fan rather than assumed.
        /// Takes effect immediately; verified on DR521 / IT858 FW 07.02.
        /// </summary>
        public bool ApplyCurve(int fan, FanPoint[] pts)
        {
            if (fan < 0 || fan > 2 || pts == null || pts.Length != 4) return false;

            byte[] live = Native.GetBuffer(CmdCurve);

            // This fan's own editable window, taken from the two endpoints we cannot change.
            int floorT = live[CurveBlock + fan * 8];
            int ceilT = live[CurveBlock + fan * 8 + 6];
            if (floorT <= 0) floorT = 40;
            if (ceilT <= floorT + 4) ceilT = 100;

            // Clamping to the fan's window rather than a global 20..100 keeps the upload
            // honest: a point outside it would be silently rewritten by the EC, so what the
            // user drew would not be what got saved.
            int t1 = Clamp(pts[1].Temp, floorT + 1, ceilT - 1);
            int d1 = Clamp(pts[1].Duty, 0, 100);
            int t2 = Clamp(pts[2].Temp, t1 + 1, ceilT - 1);
            int d2 = Clamp(pts[2].Duty, 0, 100);

            // ORDER MATTERS. While the EC is in Automatic it continuously re-derives its curve
            // table, so an upload made in Auto mode gets overwritten within seconds — which is
            // exactly the "sometimes it works, mostly it doesn't" symptom. Switching to Custom
            // FIRST stops that re-derivation, and the upload then sticks.
            // Measured: Custom-first -> 0 rewrites in 90 s; Auto-first -> clobbered.
            SetMode(FanMode.Custom);

            byte[] p = new byte[256];
            // Carry the untouched fans' values from the SETTINGS STORE, not from cmd 13.
            // cmd 13 turned out to be a lazily-refreshed mirror rather than live EC memory, so
            // reading it and writing the result straight back can push a stale curve onto a fan
            // the user never touched — which is exactly how one probe run corrupted the CPU and
            // GPU channels while only meaning to write the (absent) third one. The store is the
            // authoritative persisted copy and its duties are already percentages.
            for (int f = 0; f < 3; f++)
            {
                int o = 2 + f * 4;
                int fd0, ft0, fd1, ft1, fd2, ft2;
                if (f == fan)
                {
                    ft1 = t1; fd1 = d1; ft2 = t2; fd2 = d2;
                    byte[] blk = Native.ReadStore(StoreCurveHi, CurveBlock + f * CurveStride, 4);
                    fd0 = (blk != null && blk.Length > 0) ? blk[0] : 35;
                    ft0 = live[CurveBlock + f * 8];
                    if (ft0 <= 0) ft0 = 40;
                }
                else
                {
                    byte[] blk = Native.ReadStore(StoreCurveHi, CurveBlock + f * CurveStride, 12);
                    if (blk != null && blk.Length >= 12 && (blk[1] | blk[2] | blk[7] | blk[8]) != 0)
                    {
                        fd0 = blk[0]; fd1 = blk[1]; fd2 = blk[2];
                        ft0 = blk[6]; ft1 = blk[7]; ft2 = blk[8];
                    }
                    else
                    {
                        // store block looks uninitialised — fall back to the mirror
                        int lo2 = CurveBlock + f * 8;
                        fd0 = (int)Math.Round(live[lo2 + 1] / 255.0 * 100.0);
                        ft0 = live[lo2];
                        fd1 = (int)Math.Round(live[lo2 + 3] / 255.0 * 100.0);
                        ft1 = live[lo2 + 2];
                        fd2 = (int)Math.Round(live[lo2 + 5] / 255.0 * 100.0);
                        ft2 = live[lo2 + 4];
                    }
                    if (ft0 <= 0) ft0 = 40;
                    if (ft1 <= ft0) { ft1 = floorT + 1; fd1 = 35; }
                    if (ft2 <= ft1) { ft2 = 100; fd2 = 100; }
                }
                p[o + 0] = (byte)ft1;
                p[o + 1] = (byte)Math.Round(fd1 / 100.0 * 255.0, 0);
                p[o + 2] = (byte)ft2;
                p[o + 3] = (byte)Math.Round(fd2 / 100.0 * 255.0, 0);
                int so = 14 + f * 6;
                PutBE16(p, so + 0, Slope(fd0, fd1, ft0, ft1));
                PutBE16(p, so + 2, Slope(fd1, fd2, ft1, ft2));
                PutBE16(p, so + 4, Slope(fd2, 100, ft2, 100));
            }
            Native.SetDchuRaw(CmdCurveUpload, p);

            // Give the EC a moment to latch the new table before the store write, then
            // persist so the curve survives a mode change or reboot.
            Thread.Sleep(150);
            return StageCurve(fan, pts);
        }

        static void PutBE16(byte[] b, int o, int v) { b[o] = (byte)(v >> 8); b[o + 1] = (byte)v; }
        static int Clamp(int v, int lo, int hi) { return v < lo ? lo : (v > hi ? hi : v); }

        /// <summary>
        /// Stages a curve into the vendor settings store (store4), exactly as the stock
        /// Fan Speed Setting app does. Note this only persists it — the live value is pushed
        /// by <see cref="ApplyCurve"/>.
        /// </summary>
        public bool StageCurve(int fan, FanPoint[] pts)
        {
            if (fan < 0 || fan > 3 || pts == null || pts.Length != 4) return false;
            // Partial 18-byte write at the fan's own block. Deliberately NOT a
            // read-modify-write of the whole 256-byte store: ReadAppSettings(4,0,256)
            // intermittently returns an all-zero buffer (observed on this firmware),
            // which would wipe the mode/offset bytes at [5]/[7]. A slice write leaves
            // every other byte untouched.
            int o = CurveBlock + fan * CurveStride;
            byte[] blk = new byte[CurveStride];
            int[] d = { pts[0].Duty, pts[1].Duty, pts[2].Duty, 100 };
            int[] t = { pts[0].Temp, pts[1].Temp, pts[2].Temp, 100 };
            blk[0] = (byte)d[0]; blk[1] = (byte)d[1]; blk[2] = (byte)d[2]; blk[3] = 100;
            blk[4] = (byte)d[1]; blk[5] = (byte)d[2];
            blk[6] = (byte)t[0]; blk[7] = (byte)t[1]; blk[8] = (byte)t[2]; blk[9] = 100;
            blk[10] = (byte)t[1]; blk[11] = (byte)t[2];
            int s1 = Slope(d[0], d[1], t[0], t[1]);
            int s2 = Slope(d[1], d[2], t[1], t[2]);
            int s3 = Slope(d[2], 100, t[2], 100);
            Put16(blk, 12, s1); Put16(blk, 14, s2); Put16(blk, 16, s3);
            return Native.WriteStore(StoreCurveHi, o, blk) == 1;
        }

        static int Slope(int d0, int d1, int t0, int t1)
        {
            int dt = t1 - t0;
            if (dt == 0) return 0;
            return (int)Math.Round((d1 - d0) / (double)dt * 2.55 * 16.0, 0);
        }
        static void Put16(byte[] b, int o, int v) { b[o] = (byte)v; b[o + 1] = (byte)(v >> 8); }

        /// <summary>Replicates the stock app's anti-dust trigger (DCHU cmd 118, sub 1).</summary>
        public void AntiDust()
        {
            var n = DateTime.Now;
            int packed = n.Second + (n.Minute << 6) + (n.Hour << 12) + ((int)n.DayOfWeek << 17);
            Native.SetDchu(118, 1, packed);
        }

        public void Dispose() { }
    }
}
