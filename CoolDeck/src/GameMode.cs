// CoolDeck — one-click "game mode".
//
// What this is: a single switch that stops the session from competing with itself. Every value
// it writes is user-scope (HKCU) and fully reversible: the previous value of each key is
// snapshotted before the write and put back on toggle-off, so nothing is left changed behind
// the user's back.
//
// Deliberately NOT included, and why:
//   * Killing background processes — a wrong guess there costs unsaved work, and "which of my
//     271 processes is expendable" is a decision only the owner can make.
//   * Defender / security settings — CoolDeck never touches security configuration.
//   * Anything machine-wide (HKLM), services, or drivers — those need elevation, and this app
//     runs unelevated by design (see the ExclusivityDialog note: DCHUService is LocalSystem).

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Threading;
using Microsoft.Win32;

namespace CoolDeck
{
    public static class GameMode
    {
        const string GameBar = "Software\\Microsoft\\GameBar";
        const string GameConfigStore = "System\\GameConfigStore";
        const string GameDvr = "Software\\Microsoft\\Windows\\CurrentVersion\\GameDVR";
        const string SysProfile = "Software\\Microsoft\\Windows NT\\CurrentVersion\\Multimedia\\SystemProfile";
        const string GamesTask = "Software\\Microsoft\\Windows NT\\CurrentVersion\\Multimedia\\SystemProfile\\Tasks\\Games";
        const string GpuPrefs = "Software\\Microsoft\\DirectX\\UserGpuPreferences";
        const string Layers = "Software\\Microsoft\\Windows NT\\CurrentVersion\\AppCompatFlags\\Layers";

        /// <summary>The executable that actually renders the game. VALORANT's launcher is a
        /// browser shell and must not be promoted — raising its priority accomplishes nothing.</summary>
        public static string WatchedExe = "VALORANT-Win64-Shipping";

        public static bool Enabled { get; private set; }

        /// <summary>Raised whenever the on/off state changes, so the tray tick and the window
        /// switch can never disagree.</summary>
        public static event Action Changed;

        /// <summary>One-line progress notes for the status bar (game detected, exe not found).</summary>
        public static event Action<string> Note;

        static readonly List<Snap> _snap = new List<Snap>();
        static DispatcherTimer _watch;
        static int _boostedPid = -1;

        class Snap
        {
            public string Path;
            public string Name;
            public object Value;
            public bool Existed;
            public RegistryValueKind Kind;
        }

        // ---------------- public surface ----------------

        public static void Toggle()
        {
            if (Enabled) Off(); else On();
        }

        public static void On()
        {
            if (Enabled) return;
            _snap.Clear();
            try
            {
                // Windows' own switches. Game Mode was OFF on this machine, and the DVR
                // background recorder keeps a hardware encoder session alive whether or not
                // anyone is recording.
                SetDword(GameBar, "AutoGameModeEnabled", 1);
                SetDword(GameBar, "AllowAutoGameMode", 1);
                SetDword(GameConfigStore, "GameDVR_Enabled", 0);
                SetDword(GameDvr, "AppCaptureEnabled", 0);
                SetDword(GameDvr, "HistoricalCaptureEnabled", 0);

                // MMCSS. The default reserves 20 % of the CPU for non-multimedia work; 0 gives
                // the game's threads the room they were being denied. The Games task profile
                // is what MMCSS reads when a process registers as a game.
                SetDword(SysProfile, "SystemResponsiveness", 0);
                SetDword(GamesTask, "GPU Priority", 8);
                SetDword(GamesTask, "Priority", 6);
                SetString(GamesTask, "Scheduling Category", "High");
                SetString(GamesTask, "SFIO Priority", "High");

                // This machine had SwapEffectUpgradeEnable=0 (a leftover from some "optimizer"),
                // which suppresses the flip-model swap chain that DX11 titles want.
                EnableFlipModel();

                // Exclusive fullscreen only works if Windows stops intercepting it.
                DisableFullscreenOptimizations();

                Enabled = true;
            }
            catch (Exception ex)
            {
                Trace.W("game mode on failed: " + ex.Message);
                Rollback();
                Enabled = false;
            }
            SaveSnapshot();
            StartWatch();
            RaiseChanged();
        }

        public static void Off()
        {
            StopWatch();
            if (!Enabled) { DeleteSnapshot(); return; }
            Rollback();
            DeleteSnapshot();
            Enabled = false;
            RaiseChanged();
        }

        /// <summary>Called on app exit: never leave the machine in a tweaked state just because
        /// the tray icon was closed while game mode happened to be on.</summary>
        public static void Shutdown()
        {
            if (Enabled) Off();
        }

        /// <summary>Called at startup. If a previous run died while game mode was on, the
        /// snapshot file is still on disk and the registry is still tweaked — restore it now,
        /// so "reversible" survives a crash or a kill in Task Manager.</summary>
        public static void RecoverIfNeeded()
        {
            try
            {
                if (!System.IO.File.Exists(SnapshotFile())) return;
                Trace.W("game mode: found stale snapshot, restoring");
                _snap.Clear();
                Rollback();
                DeleteSnapshot();
            }
            catch (Exception ex) { Trace.W("game mode recovery failed: " + ex.Message); }
        }

        // ---------------- registry plumbing ----------------

        static void Remember(string path, string name)
        {
            var s = new Snap { Path = path, Name = name };
            try
            {
                using (RegistryKey k = Registry.CurrentUser.OpenSubKey(path, false))
                {
                    if (k != null)
                    {
                        object v = k.GetValue(name, null);
                        if (v != null)
                        {
                            s.Existed = true;
                            s.Value = v;
                            s.Kind = k.GetValueKind(name);
                        }
                    }
                }
            }
            catch { }
            _snap.Add(s);
        }

        static void SetDword(string path, string name, int value)
        {
            Remember(path, name);
            using (RegistryKey k = Registry.CurrentUser.CreateSubKey(path))
                if (k != null) k.SetValue(name, value, RegistryValueKind.DWord);
        }

        static void SetString(string path, string name, string value)
        {
            Remember(path, name);
            using (RegistryKey k = Registry.CurrentUser.CreateSubKey(path))
                if (k != null) k.SetValue(name, value, RegistryValueKind.String);
        }

        static void Rollback()
        {
            if (_snap.Count == 0) LoadSnapshot();
            for (int i = _snap.Count - 1; i >= 0; i--)
            {
                Snap s = _snap[i];
                try
                {
                    using (RegistryKey k = Registry.CurrentUser.OpenSubKey(s.Path, true))
                    {
                        if (k == null) continue;
                        if (s.Existed) k.SetValue(s.Name, s.Value, s.Kind);
                        else k.DeleteValue(s.Name, false);
                    }
                }
                catch { }
            }
            _snap.Clear();
        }

        // ---------------- crash-safe snapshot ----------------
        // The in-memory list is enough for a clean toggle. The file is what makes "reversible"
        // true when the process is killed mid-session: the next start finds it and rolls back.

        static string SnapshotFile()
        {
            return System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "CoolDeck", "gamemode.snapshot");
        }

        static void SaveSnapshot()
        {
            try
            {
                string dir = System.IO.Path.GetDirectoryName(SnapshotFile());
                if (!System.IO.Directory.Exists(dir)) System.IO.Directory.CreateDirectory(dir);
                var sb = new System.Text.StringBuilder();
                for (int i = 0; i < _snap.Count; i++)
                {
                    Snap s = _snap[i];
                    sb.Append(s.Path).Append('|')
                      .Append(s.Name).Append('|')
                      .Append(((int)s.Kind).ToString(CultureInfo.InvariantCulture)).Append('|')
                      .Append(s.Existed ? "1" : "0").Append('|')
                      .Append(s.Existed ? Convert.ToString(s.Value, CultureInfo.InvariantCulture) : "")
                      .Append('\n');
                }
                System.IO.File.WriteAllText(SnapshotFile(), sb.ToString(), System.Text.Encoding.UTF8);
            }
            catch (Exception ex) { Trace.W("game mode: snapshot save failed: " + ex.Message); }
        }

        static void LoadSnapshot()
        {
            try
            {
                if (!System.IO.File.Exists(SnapshotFile())) return;
                string[] lines = System.IO.File.ReadAllLines(SnapshotFile(), System.Text.Encoding.UTF8);
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] f = lines[i].Split('|');
                    if (f.Length < 5) continue;
                    var s = new Snap();
                    s.Path = f[0];
                    s.Name = f[1];
                    s.Kind = (RegistryValueKind)int.Parse(f[2], CultureInfo.InvariantCulture);
                    s.Existed = f[3] == "1";
                    if (s.Existed)
                    {
                        string raw = string.Join("|", f, 4, f.Length - 4);
                        s.Value = s.Kind == RegistryValueKind.DWord
                            ? (object)int.Parse(raw, CultureInfo.InvariantCulture)
                            : raw;
                    }
                    _snap.Add(s);
                }
            }
            catch (Exception ex) { Trace.W("game mode: snapshot load failed: " + ex.Message); }
        }

        static void DeleteSnapshot()
        {
            try
            {
                if (System.IO.File.Exists(SnapshotFile())) System.IO.File.Delete(SnapshotFile());
            }
            catch { }
        }

        static void EnableFlipModel()
        {
            Remember(GpuPrefs, "DirectXUserGlobalSettings");
            string cur = "";
            try
            {
                using (RegistryKey k = Registry.CurrentUser.OpenSubKey(GpuPrefs, false))
                    if (k != null)
                    {
                        object v = k.GetValue("DirectXUserGlobalSettings");
                        if (v != null) cur = v.ToString();
                    }
            }
            catch { }

            // Only this one key is rewritten. HighPerfAdapter and friends are carried over
            // verbatim: this value is a semicolon list the whole system shares.
            string next = ReplaceSetting(cur, "SwapEffectUpgradeEnable", "1");
            using (RegistryKey k = Registry.CurrentUser.CreateSubKey(GpuPrefs))
                if (k != null) k.SetValue("DirectXUserGlobalSettings", next, RegistryValueKind.String);
        }

        static string ReplaceSetting(string s, string key, string val)
        {
            var parts = new List<string>();
            bool found = false;
            string[] seg = (s == null ? "" : s).Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < seg.Length; i++)
            {
                int eq = seg[i].IndexOf('=');
                string k = eq < 0 ? seg[i] : seg[i].Substring(0, eq);
                if (string.Equals(k, key, StringComparison.OrdinalIgnoreCase))
                {
                    parts.Add(key + "=" + val);
                    found = true;
                }
                else parts.Add(seg[i]);
            }
            if (!found) parts.Add(key + "=" + val);
            return string.Join(";", parts.ToArray()) + ";";
        }

        /// <summary>The game's install path is not hard-coded: it is already recorded as a value
        /// NAME under UserGpuPreferences (the "high performance GPU" override), so a Riot or
        /// WeGame reinstall that moves the folder is followed automatically.</summary>
        static string FindGameExe()
        {
            try
            {
                using (RegistryKey k = Registry.CurrentUser.OpenSubKey(GpuPrefs, false))
                {
                    if (k != null)
                    {
                        string[] names = k.GetValueNames();
                        for (int i = 0; i < names.Length; i++)
                            if (names[i].EndsWith("\\" + WatchedExe + ".exe", StringComparison.OrdinalIgnoreCase))
                                return names[i];
                    }
                }
            }
            catch { }
            try
            {
                Process[] ps = Process.GetProcessesByName(WatchedExe);
                if (ps.Length > 0)
                {
                    string file = null;
                    try { file = ps[0].MainModule.FileName; }
                    catch { }
                    for (int i = 0; i < ps.Length; i++) ps[i].Dispose();
                    if (!string.IsNullOrEmpty(file)) return file;
                }
            }
            catch { }
            return null;
        }

        static void DisableFullscreenOptimizations()
        {
            string exe = FindGameExe();
            if (string.IsNullOrEmpty(exe))
            {
                RaiseNote(Loc.T("game mode: VALORANT not found, skipped the fullscreen tweak"));
                return;
            }
            Remember(Layers, exe);
            string cur = null;
            try
            {
                using (RegistryKey k = Registry.CurrentUser.OpenSubKey(Layers, false))
                    if (k != null)
                    {
                        object v = k.GetValue(exe);
                        if (v != null) cur = v.ToString();
                    }
            }
            catch { }

            const string flag = "DISABLEDXMAXIMIZEDWINDOWEDMODE";
            string next;
            if (string.IsNullOrEmpty(cur)) next = "~ " + flag;
            else if (cur.IndexOf(flag, StringComparison.OrdinalIgnoreCase) >= 0) next = cur;
            else next = cur + " " + flag;

            using (RegistryKey k = Registry.CurrentUser.CreateSubKey(Layers))
                if (k != null) k.SetValue(exe, next, RegistryValueKind.String);
        }

        // ---------------- game process watcher ----------------

        static void StartWatch()
        {
            if (_watch == null)
            {
                _watch = new DispatcherTimer();
                _watch.Interval = TimeSpan.FromSeconds(2);
                _watch.Tick += delegate { Poll(); };
            }
            _watch.Start();
            Poll();
        }

        static void StopWatch()
        {
            if (_watch != null) _watch.Stop();
            _boostedPid = -1;
        }

        /// <summary>Raises the game's scheduling priority once per process instance. Re-applied
        /// on relaunch automatically, because the pid changes.</summary>
        static void Poll()
        {
            if (!Enabled) return;
            Process[] ps;
            try { ps = Process.GetProcessesByName(WatchedExe); }
            catch { return; }
            for (int i = 0; i < ps.Length; i++)
            {
                Process p = ps[i];
                try
                {
                    if (p.Id == _boostedPid) continue;
                    p.PriorityClass = ProcessPriorityClass.High;
                    _boostedPid = p.Id;
                    Trace.W("game mode: raised pid " + p.Id + " to High");
                    RaiseNote(Loc.T("game detected — priority raised to High"));
                }
                catch (Exception ex)
                {
                    Trace.W("game mode: priority raise failed: " + ex.Message);
                }
                finally { p.Dispose(); }
            }
        }

        static void RaiseChanged()
        {
            Action h = Changed;
            if (h != null) h();
        }

        static void RaiseNote(string s)
        {
            Action<string> h = Note;
            if (h != null) h(s);
        }
    }
}
