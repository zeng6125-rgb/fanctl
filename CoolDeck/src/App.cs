// CoolDeck — entry point, single-instance guard, tray icon, minimise-to-tray,
// and a small CLI so the controller is scriptable (and testable headlessly).

using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;       // the exclusivity dialog is built from these (in code — no XAML)
using Microsoft.Win32;               // HKCU Run key for "Start with Windows"
// WinForms is imported for NotifyIcon, so these two control names would be ambiguous;
// alias them to the WPF versions the dialog needs. (csc 4.0 = C# 5 only: plain using
// directives and delegates throughout — no interpolation, no local functions.)
using Button = System.Windows.Controls.Button;
using TextBox = System.Windows.Controls.TextBox;

namespace CoolDeck
{
    public static class App
    {
        static Mutex _single;
        static System.Windows.Application _app;
        static NotifyIcon _tray;
        static MainWindow _win;
        static ToolStripMenuItem _miAuto, _miMax, _miSilent, _miCustom;

        /// <summary>True when the process was launched only to sit in the tray (boot
        /// autostart). The window is never shown, but the controller still starts and
        /// the saved profile is still re-applied — silence is about the UI, not the job.</summary>
        public static bool SilentStart { get; private set; }

        static bool HasFlag(string[] args, string flag)
        {
            if (args == null) return false;
            for (int i = 0; i < args.Length; i++)
                if (string.Equals(args[i], flag, StringComparison.OrdinalIgnoreCase)) return true;
            return false;
        }

        [STAThread]
        public static void Main(string[] args)
        {
            if (args != null && args.Length > 0 && Cli.Run(args)) return;

            // --tray is a GUI-mode flag, not a Cli command (Cli.Run ignores it and
            // returns false), so it must be picked out before the instance handshake.
            SilentStart = HasFlag(args, "--tray") || HasFlag(args, "-tray") || HasFlag(args, "/tray");

            bool created;
            _single = new Mutex(true, "Global\\CoolDeck.SingleInstance", out created);
            if (!created)
            {
                // A previous instance is alive (usually sitting in the tray). Silently
                // returning made the app look broken — "double-click does nothing". Ask the
                // running instance to show itself instead — unless this launch is itself a
                // silent one (a boot Run entry firing while the app already runs): popping
                // the window over the user's login would defeat the whole point of --tray.
                if (!SilentStart) SignalExisting();
                return;
            }

            var app = new System.Windows.Application();
            _app = app;
            app.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            // Software rendering: keeps the window paintable over RDP, on locked
            // sessions and on machines with flaky WPF hardware acceleration.
            System.Windows.Media.RenderOptions.ProcessRenderMode =
                System.Windows.Interop.RenderMode.SoftwareOnly;

            Trace.Init();
            Trace.W("app start");
            _win = new MainWindow();
            Trace.W("window built");
            BuildTray();
            Trace.W("tray built");

            // The window hides itself on close (MainWindow.Closing) so fan control keeps
            // running; show the tray icon whenever that happens.
            _win.IsVisibleChanged += delegate { _tray.Visible = !_win.IsVisible; };

            // Listen for a second launch asking us to come back from the tray.
            _showEvent = new EventWaitHandle(false, EventResetMode.AutoReset, "Global\\CoolDeck.Show");
            var waiter = new Thread(delegate()
            {
                while (true)
                {
                    try
                    {
                        _showEvent.WaitOne();
                        _win.Dispatcher.BeginInvoke((Action)delegate
                        {
                            _win.Show();
                            _win.WindowState = WindowState.Normal;
                            _win.Activate();
                            _tray.Visible = false;
                        });
                    }
                    catch { return; }
                }
            });
            waiter.IsBackground = true;
            waiter.Start();

            if (SilentStart)
            {
                // Never shown: no taskbar button, no window. The tray is the whole UI.
                // SilentStart() kicks the controller + profile restore that Loaded would
                // otherwise trigger (Loaded does not fire until the first Show).
                _tray.Visible = true;
                _win.SilentStart();
                app.Run();
            }
            else
            {
                app.Run(_win);
            }
        }

        static EventWaitHandle _showEvent;

        /// <summary>Ask an already-running instance to bring its window to the front.
        /// Uses a named event the first instance waits on in a background thread.</summary>
        static void SignalExisting()
        {
            try
            {
                EventWaitHandle e;
                if (EventWaitHandle.TryOpenExisting("Global\\CoolDeck.Show", out e))
                {
                    using (e) { e.Set(); }
                }
            }
            catch { }
        }

        static void BuildTray()
        {
            _tray = new NotifyIcon();
            _tray.Icon = TrayIcon();
            _tray.Text = Loc.T("CoolDeck — fan control");
            _tray.Visible = false;

            var menu = new ContextMenuStrip();
            menu.Items.Add(Loc.T("Open CoolDeck"), null, delegate { Show(); });
            menu.Items.Add(new ToolStripSeparator());

            // The four fan modes, in the EC's own vocabulary so the tray and the mode pills in
            // the window can never disagree about what a name means. The tick is re-read from
            // the firmware every time the menu opens, not remembered from the last click.
            _miAuto = ModeItem(Loc.T("Automatic"), (int)FanMode.Automatic);
            _miMax = ModeItem(Loc.T("Maximum"), (int)FanMode.Maximum);
            _miSilent = ModeItem(Loc.T("Silent"), (int)FanMode.Silent);
            _miCustom = ModeItem(Loc.T("Custom"), (int)FanMode.Custom);
            menu.Items.Add(_miAuto);
            menu.Items.Add(_miMax);
            menu.Items.Add(_miSilent);
            menu.Items.Add(_miCustom);
            menu.Items.Add(new ToolStripSeparator());

            var auto = new ToolStripMenuItem("Start with Windows");
            // The tick is a live read of the HKCU Run key, never a cached flag: regedit,
            // a moved exe or a sibling install can change it while we sit in the tray.
            auto.Checked = AutoStartIsEnabled();
            auto.Click += delegate { ToggleAutoStart(auto); };
            menu.Items.Add(auto);
            menu.Items.Add(Loc.T("Fan control exclusivity…"), null, delegate { ExclusivityDialog.Open(_win); });

            // Re-sync the tick every time the menu pops up, same reason as above.
            menu.Opening += delegate
            {
                auto.Checked = AutoStartIsEnabled();
                SyncModes();
            };

            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add(Loc.T("Exit"), null, delegate
            {
                _tray.Visible = false;
                _win.RealClose = true;
                _win.Close();
                _tray.Dispose();
                // OnExplicitShutdown means closing the last window does NOT stop the
                // dispatcher — without this the process lingers invisibly (still holding
                // the single-instance mutex, so the next launch just signals a ghost).
                if (_app != null) _app.Shutdown();
            });
            _tray.ContextMenuStrip = menu;

            // A single left click is what everyone tries first, so it restores the window.
            // NotifyIcon raises mouse events for the right button too (that is how the
            // context menu opens), hence the Left filter — without it a right-click would
            // pop the window underneath the menu. DoubleClick stays; Show() on an
            // already-visible window is harmless.
            _tray.MouseDown += delegate(object s, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left) Show();
            };
            _tray.DoubleClick += delegate { Show(); };
        }

        static void Show()
        {
            _win.Show();
            _win.WindowState = WindowState.Normal;
            _win.Activate();
            _tray.Visible = false;
        }

        // ---------------- fan mode entries ----------------

        static ToolStripMenuItem ModeItem(string label, int mode)
        {
            var it = new ToolStripMenuItem(label);
            it.Click += delegate { ApplyMode(mode); };
            return it;
        }

        static void ApplyMode(int mode)
        {
            try { _win.PickMode(mode); }
            catch (Exception ex) { Trace.W("tray mode set failed: " + ex.Message); }
            SyncModes();
        }

        /// <summary>Ticks the mode the firmware reports, not the one last clicked: the stock
        /// Control Center, a hotkey or a reboot can change it while CoolDeck sits in the tray.
        /// No tick at all is the honest answer for a mode the menu does not list (MaxQ, IQST).</summary>
        static void SyncModes()
        {
            if (_miAuto == null || _win == null) return;
            int cur;
            try { cur = _win.ModeNow(); }
            catch { cur = -1; }
            _miAuto.Checked = cur == (int)FanMode.Automatic;
            _miMax.Checked = cur == (int)FanMode.Maximum;
            _miSilent.Checked = cur == (int)FanMode.Silent;
            _miCustom.Checked = cur == (int)FanMode.Custom;
        }

        // ---------------- Start with Windows (HKCU Run key) ----------------

        // Per-user Run key: writable without elevation (the HKLM one is not), and the
        // value name is what Task Manager's Startup tab shows as the entry's identity.
        const string AutoRunKey = "Software\\Microsoft\\Windows\\CurrentVersion\\Run";
        const string AutoRunValue = "CoolDeck";

        static string ExePath()
        {
            try { return System.Reflection.Assembly.GetEntryAssembly().Location; }
            catch { return null; }
        }

        /// <summary>True when the Run key points at the exe we are actually running —
        /// either the current silent form ("exe" --tray) or the legacy bare-exe form.
        /// A legacy entry is upgraded in place so the next boot is silent; that is the
        /// whole point of writing the flag, and leaving old installs loud would make
        /// the feature look broken for everyone who enabled autostart before it.</summary>
        static bool AutoStartIsEnabled()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(AutoRunKey, false))
                {
                    if (key == null) return false;
                    object v = key.GetValue(AutoRunValue);
                    string s = v == null ? null : Convert.ToString(v);
                    if (string.IsNullOrEmpty(s)) return false;
                    string exe = ExePath();
                    if (string.IsNullOrEmpty(exe)) return false;
                    if (string.Equals(s.Trim(), "\"" + exe + "\" --tray", StringComparison.OrdinalIgnoreCase))
                        return true;
                    string stored = s.Trim().Trim('"');
                    bool legacy = string.Equals(stored, exe, StringComparison.OrdinalIgnoreCase);
                    if (legacy)
                    {
                        // Best effort: even if the rewrite fails the entry still boots us.
                        string err = SetAutoStart(true);
                        Trace.W(err == null
                            ? "autostart entry upgraded to silent form"
                            : "autostart upgrade failed, keeping legacy entry: " + err);
                        return true;
                    }
                    Trace.W("autostart entry exists but points elsewhere: " + s);
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Registry access can fail under lockdown policies; the menu must not lie.
                Trace.W("autostart read failed: " + ex.Message);
                return false;
            }
        }

        /// <summary>Write or delete the Run value. Returns null on success, the error
        /// text otherwise — registry trouble is never allowed to take the app down.</summary>
        static string SetAutoStart(bool on)
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(AutoRunKey, true))
                {
                    if (key == null) return "could not open HKCU\\" + AutoRunKey + " for writing";
                    if (on)
                    {
                        string exe = ExePath();
                        if (string.IsNullOrEmpty(exe)) return "the path of the running exe is unknown";
                        // Quotes matter: without them "C:\My Tools\CoolDeck.exe" dies on the space.
                        // --tray keeps the boot launch silent: window never shown, only the
                        // tray icon. A manual double-click (no flag) still opens the window.
                        key.SetValue(AutoRunValue, "\"" + exe + "\" --tray");
                    }
                    else
                    {
                        key.DeleteValue(AutoRunValue, false); // false = tolerate "not present"
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        static void ToggleAutoStart(ToolStripMenuItem item)
        {
            bool want = !item.Checked;
            string err = SetAutoStart(want);
            if (err != null)
            {
                Trace.W("autostart toggle failed: " + err);
                System.Windows.MessageBox.Show(_win,
                    "Could not change the CoolDeck startup entry: " + err,
                    "CoolDeck");
                // Re-read instead of guessing — the write may have half-applied.
                item.Checked = AutoStartIsEnabled();
                return;
            }
            item.Checked = want;
            Trace.W("autostart " + (want ? "enabled" : "disabled"));
        }

        /// <summary>Tray icon: reuse the executable's own embedded icon so the tray, the
        /// taskbar and Explorer all show the same mark. Falls back to a generated glyph only
        /// if the resource is missing.</summary>
        static Icon TrayIcon()
        {
            try
            {
                string exe = System.Reflection.Assembly.GetEntryAssembly().Location;
                var extracted = Icon.ExtractAssociatedIcon(exe);
                if (extracted != null)
                {
                    // The tray wants a 16px image; ExtractAssociatedIcon gives 32px, which
                    // NotifyIcon scales badly. Re-render it at 16 for a crisp result.
                    using (var bmp = new Bitmap(16, 16))
                    using (var g = Graphics.FromImage(bmp))
                    {
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        g.DrawIcon(extracted, new Rectangle(0, 0, 16, 16));
                        IntPtr h = bmp.GetHicon();
                        try { using (var tmp = Icon.FromHandle(h)) return (Icon)tmp.Clone(); }
                        finally { DestroyIcon(h); }
                    }
                }
            }
            catch { }
            return IconFromText("CD", Color.FromArgb(0x22, 0xD3, 0xEE));
        }

        // A tiny generated icon: a cyan rounded square with "CD" in it. Avoids shipping
        // a binary .ico through the csc-only toolchain.
        static Icon IconFromText(string text, Color accent)
        {
            using (var bmp = new Bitmap(32, 32))
            using (var g = Graphics.FromImage(bmp))
            using (var f = new Font("Segoe UI", 11, System.Drawing.FontStyle.Bold))
            using (var fill = new SolidBrush(accent))
            using (var bg = new SolidBrush(Color.FromArgb(0x11, 0x14, 0x1C)))
            {
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                g.FillRectangle(bg, 0, 0, 32, 32);
                var sz = g.MeasureString(text, f);
                g.DrawString(text, f, fill, (32 - sz.Width) / 2f, (32 - sz.Height) / 2f);
                IntPtr handle = bmp.GetHicon();
                try { using (var h = Icon.FromHandle(handle)) return (Icon)h.Clone(); }
                finally { DestroyIcon(handle); }
            }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool DestroyIcon(IntPtr handle);
    }

    /// <summary>Headless interface: `CoolDeck.exe --status`, `--mode N`, `--offset N`.</summary>
    static class Cli
    {
        public static bool Run(string[] a)
        {
            string cmd = a[0].ToLowerInvariant();
            if (cmd != "--status" && cmd != "--mode" && cmd != "--offset" && cmd != "--curve" && cmd != "--help") return false;

            var ctl = new Controller();
            if (ctl.Init() == null)
            {
                Console.Error.WriteLine("InsydeDCHU.dll not found — install the CLEVO Control Center.");
                return true;
            }

            if (cmd == "--help")
            {
                Console.WriteLine("CoolDeck — DEVIL RAYS / Clevo DCHU fan control");
                Console.WriteLine("  --status           print live telemetry as JSON");
                Console.WriteLine("  --mode <0..9>      set fan mode");
                Console.WriteLine("  --offset <0..100>  set automatic-curve speed offset (%)");
                Console.WriteLine("  --curve <fan> <t0> <d0> <t1> <d1> <t2> <d2>");
                Console.WriteLine("                     upload a 4-point curve to the EC and switch to Custom");
                Console.WriteLine("                     (only the two middle points are settable)");
                Console.WriteLine("  --tray             GUI mode, but start hidden in the tray (for autostart).");
                Console.WriteLine("                     The controller still starts and the saved profile");
                Console.WriteLine("                     is still re-applied; only the window stays hidden.");
                Console.WriteLine();
                Console.WriteLine("modes: 0 Automatic  1 Maximum  2 Noiseless  3 Silent");
                Console.WriteLine("       5 MaxQ       6 Custom   8 NoiselessEx  9 IFSC/IQST");
                return true;
            }

            if (cmd == "--mode")
            {
                int m;
                if (a.Length < 2 || !int.TryParse(a[1], out m)) { Console.Error.WriteLine("usage: --mode <0..9>"); return true; }
                ctl.SetMode((FanMode)m);
                Console.WriteLine("mode -> " + m);
                return true;
            }

            if (cmd == "--offset")
            {
                int p;
                if (a.Length < 2 || !int.TryParse(a[1], out p)) { Console.Error.WriteLine("usage: --offset <0..100>"); return true; }
                ctl.SetOffset(p);
                Console.WriteLine("offset -> " + p + " %");
                return true;
            }

            if (cmd == "--curve")
            {
                if (a.Length < 8)
                {
                    Console.Error.WriteLine("usage: --curve <fan 0..3> <t0> <d0> <t1> <d1> <t2> <d2>");
                    return true;
                }
                int fan, t0, d0, t1, d1, t2, d2;
                if (!int.TryParse(a[1], out fan) || !int.TryParse(a[2], out t0) ||
                    !int.TryParse(a[3], out d0) || !int.TryParse(a[4], out t1) ||
                    !int.TryParse(a[5], out d1) || !int.TryParse(a[6], out t2) ||
                    !int.TryParse(a[7], out d2))
                {
                    Console.Error.WriteLine("usage: --curve <fan 0..3> <t0> <d0> <t1> <d1> <t2> <d2>");
                    return true;
                }
                var pts = new FanPoint[4];
                pts[0] = new FanPoint(t0, d0);
                pts[1] = new FanPoint(t1, d1);
                pts[2] = new FanPoint(t2, d2);
                pts[3] = new FanPoint(100, 100);
                bool ok = ctl.ApplyCurve(fan, pts);
                Console.WriteLine(ok
                    ? "curve uploaded to the EC and applied: (" + t0 + "C," + d0 + "%) (" + t1 + "C," + d1 + "%) (" + t2 + "C," + d2 + "%) (100C,100%)"
                    : "curve upload failed");
                if (ok) Console.WriteLine("note: only the two middle points are settable — the EC owns 40C and 100C");
                return true;
            }

            var s = ctl.Read();
            var ci = CultureInfo.InvariantCulture;
            Console.WriteLine("{");
            Console.WriteLine("  \"board\": \"" + Esc(ctl.Board) + "\",");
            Console.WriteLine("  \"ec\": \"" + Esc(ctl.EcChip + " " + ctl.EcVersion) + "\",");
            Console.WriteLine("  \"dll\": \"" + Esc(ctl.DllPath) + "\",");
            Console.WriteLine("  \"mode\": " + (int)s.Mode + ",");
            Console.WriteLine("  \"offsetPct\": " + s.Offset + ",");
            Console.WriteLine("  \"fans\": [");
            for (int i = 0; i < 4; i++)
            {
                var f = s.Fans[i];
                Console.WriteLine("    { \"name\": \"" + Esc(f.Name) + "\", \"present\": " +
                    (f.Present ? "true" : "false") + ", \"rpm\": " + f.Rpm +
                    ", \"dutyPct\": " + f.DutyPct + ", \"tempC\": " + f.TempC + " }" +
                    (i < 3 ? "," : ""));
            }
            Console.WriteLine("  ],");
            Console.WriteLine("  \"curves\": [");
            for (int i = 0; i < 4; i++)
            {
                string pts = "";
                foreach (var p in s.Curves[i]) pts += "[" + p.Temp + "," + p.Duty + "],";
                Console.WriteLine("    [" + pts.TrimEnd(',') + "]" + (i < 3 ? "," : ""));
            }
            Console.WriteLine("  ]");
            Console.WriteLine("}");
            return true;
        }

        static string Esc(string s)
        {
            return s == null ? "" : s.Replace("\\", "\\\\").Replace("\"", "\\\"");
        }
    }

    /// <summary>
    /// Explains what CoolDeck can and cannot do about being the only fan controller.
    ///
    /// This is deliberately honest rather than helpful-looking. The vendor hotkey service
    /// (DCHUService) runs as LocalSystem with start type Automatic, so a non-elevated process
    /// cannot stop or disable it — CoolDeck does not try, and does not pretend otherwise.
    /// What IS achievable without elevation is pointing the user at the startup entries for the
    /// vendor UWP apps, which are the ones that actually race CoolDeck by writing curves.
    /// Nothing here ever kills another process.
    /// </summary>
    public static class ExclusivityDialog
    {
        // Names of the vendor fan stack, matched case-insensitively against process names.
        static readonly string[] VendorNames =
        {
            "FanSpeedSetting", "ControlCenter", "CC", "FnKey",
            "DCHUService", "HKClipSvc", "Flexikey", "LedKeyboard", "HotkeyService"
        };

        public static void Open(Window owner)
        {
            Trace.W("exclusivity dialog opened");
            var w = new Window
            {
                Title = "Fan control exclusivity",
                Width = 560, Height = 520,
                WindowStartupLocation = owner != null
                    ? WindowStartupLocation.CenterOwner : WindowStartupLocation.CenterScreen,
                Owner = owner,
                ShowInTaskbar = false,
                ResizeMode = ResizeMode.CanResize,
                Background = Theme.Bg0Brush,
                Foreground = Theme.TxtBrush,
                FontFamily = new System.Windows.Media.FontFamily(Theme.Font),
                FontSize = 12
            };

            var root = new Grid { Margin = new Thickness(18) };
            root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });   // heading
            root.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }); // body
            root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });   // buttons
            w.Content = root;

            root.Children.Add(new TextBlock
            {
                Text = Loc.T("Who is controlling the fans?"),
                FontFamily = new System.Windows.Media.FontFamily(Theme.FontDisplay),
                FontSize = 15, Foreground = Theme.TxtBrush,
                Margin = new Thickness(0, 0, 0, 10)
            });
            Grid.SetRow(root.Children[0], 0);

            var body = new StackPanel { Margin = new Thickness(0, 0, 0, 0) };
            var scroller = new ScrollViewer
            {
                // Primitives not imported at file top (WinForms already owns too many
                // short names) — qualify.
                VerticalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Auto,
                Content = body
            };
            root.Children.Add(scroller);
            Grid.SetRow(scroller, 1);

            body.Children.Add(Para(
                "CoolDeck writes fan curves and modes straight through the vendor InsydeDCHU " +
                "protocol. It does not install a service, a driver or a startup task of its own, " +
                "and it changes nothing until you press a button."));

            body.Children.Add(Para(
                "The embedded controller is the real authority. Whatever CoolDeck uploads can be " +
                "overwritten at any time by another program writing the same registers, and the EC " +
                "itself also re-derives its curve table while the fan mode is Automatic. That is why " +
                "saving a curve switches the mode to Custom first."));

            body.Children.Add(Para(
                "What CoolDeck can do about it: tell you which vendor fan programs are currently " +
                "running, and get you to the Windows startup settings so you can stop them launching. " +
                "You can also just close them."));

            var warn = new Border
            {
                // Uses only brushes that exist in Theme.cs today (CardHi + Warm reads as
                // a warning box); Theme.AccentSoft/Btn were referenced but are not members.
                Background = Theme.CardHiBrush,
                BorderBrush = Theme.WarmBrush,
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(8),
                Padding = new Thickness(12, 9, 12, 10),
                Margin = new Thickness(0, 4, 0, 12)
            };
            warn.Child = new TextBlock
            {
                Text = Loc.T(
                       "What CoolDeck will NOT do: the vendor hotkey service (DCHUService) runs as " +
                       "LocalSystem and starts automatically. A normal unelevated program cannot stop " +
                       "or disable it, and CoolDeck will not pretend to. Disabling it needs " +
                       "administrator rights and is left to you. Leaving that service running is " +
                       "harmless on its own — it is the curve-writing apps that conflict."),
                FontSize = 11.5, Foreground = Theme.TxtDimBrush,
                TextWrapping = System.Windows.TextWrapping.Wrap, LineHeight = 16
            };
            body.Children.Add(warn);

            body.Children.Add(new TextBlock
            {
                Text = Loc.T("Vendor fan processes running right now"),
                FontFamily = new System.Windows.Media.FontFamily(Theme.FontDisplay),
                FontSize = 12, Foreground = Theme.TxtBrush,
                Margin = new Thickness(0, 2, 0, 6)
            });

            var list = new TextBox
            {
                IsReadOnly = true,
                TextWrapping = System.Windows.TextWrapping.Wrap,
                FontFamily = new System.Windows.Media.FontFamily("Consolas"),
                FontSize = 11.5,
                Background = Theme.B(Theme.Card),
                Foreground = Theme.TxtDimBrush,
                BorderBrush = Theme.B(Theme.Line),
                BorderThickness = new Thickness(1),
                Padding = new Thickness(10, 8, 10, 8),
                MinHeight = 92,
                Text = Scan()
            };
            body.Children.Add(list);

            var btns = new StackPanel
            {
                Orientation = System.Windows.Controls.Orientation.Horizontal,
                Margin = new Thickness(0, 12, 0, 0)
            };
            root.Children.Add(btns);
            Grid.SetRow(btns, 2);

            // Spec name for the refresh control; the initial scan already ran while the
            // dialog was being built, this button is for re-checking after closing apps.
            btns.Children.Add(DialogButton(Loc.T("Show vendor fan apps"), delegate
            {
                list.Text = Scan();
                Trace.W("exclusivity dialog: vendor scan rerun");
            }));
            btns.Children.Add(DialogButton(Loc.T("Open Startup settings"), delegate
            {
                // ms-settings: needs the shell to resolve the protocol handler.
                try
                {
                    var psi = new System.Diagnostics.ProcessStartInfo("ms-settings:startup")
                    { UseShellExecute = true };
                    System.Diagnostics.Process.Start(psi);
                }
                catch (Exception ex) { list.Text = Loc.T("could not open Startup settings: ") + ex.Message; }
            }));
            var close = DialogButton(Loc.T("Close"), delegate { w.Close(); });
            close.IsCancel = true;
            btns.Children.Add(close);

            // The dialog must actually appear — the tray menu invokes Open() and expects
            // a modal window. ShowDialog blocks on the dispatcher like every modal does.
            w.Closed += delegate { Trace.W("exclusivity dialog closed"); };
            w.ShowDialog();
        }

        static string Scan()
        {
            try
            {
                string[] lines = new string[64];
                int n = 0;
                foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcesses())
                {
                    string name = null;
                    try { name = p.ProcessName; } catch { }
                    if (string.IsNullOrEmpty(name)) continue;
                    for (int i = 0; i < VendorNames.Length; i++)
                    {
                        // "CC" is a substring of many unrelated names, so it matches only
                        // exactly; the longer tokens match case-insensitively anywhere in
                        // the name (catches ControlCenter3, HotkeyServiceWindows, ...).
                        bool hit = VendorNames[i] == "CC"
                            ? string.Equals(name, "CC", StringComparison.OrdinalIgnoreCase)
                            : name.IndexOf(VendorNames[i], StringComparison.OrdinalIgnoreCase) >= 0;
                        if (!hit) continue;
                        if (n < lines.Length)
                            lines[n++] = string.Format(CultureInfo.InvariantCulture,
                                "  {0,-22} pid {1}", name, p.Id);
                        break;
                    }
                }
                if (n == 0)
                    // Honest: an empty list of NAME MATCHES is not proof nobody else can
                    // write the EC — other software could reach the same registers.
                    return "  none detected — no running process matched the vendor fan " +
                           "name list below.";
                var sb = new System.Text.StringBuilder();
                for (int i = 0; i < n; i++) sb.AppendLine(lines[i]);
                sb.Append("\n  Present here does not mean they are fighting you: only the ones that\n" +
                          "  write fan curves do. Close them from Task Manager if curves keep changing.");
                return sb.ToString();
            }
            catch (Exception ex)
            {
                return "  scan failed: " + ex.Message;
            }
        }

        /// <summary>Body paragraph. Translating inside the helper keeps the call sites readable —
        /// they are long multi-line concatenations that would otherwise need wrapping twice.</summary>
        static TextBlock Para(string text)
        {
            return new TextBlock
            {
                Text = Loc.T(text), FontSize = 11.5, Foreground = Theme.TxtDimBrush,
                TextWrapping = System.Windows.TextWrapping.Wrap, LineHeight = 16,
                Margin = new Thickness(0, 0, 0, 10)
            };
        }

        static Button DialogButton(string text, Action act)
        {
            var b = new Button
            {
                Content = text, Height = 28, Padding = new Thickness(14, 0, 14, 0),
                Margin = new Thickness(0, 0, 8, 0), Cursor = System.Windows.Input.Cursors.Hand,
                Background = Theme.CardHiBrush, BorderBrush = Theme.LineBrush,
                BorderThickness = new Thickness(1), Foreground = Theme.TxtDimBrush, FontSize = 11
            };
            b.Click += delegate { act(); };
            return b;
        }
    }
}
