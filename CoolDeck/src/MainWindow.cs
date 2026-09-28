// CoolDeck — main window. Pure C# WPF (no XAML).

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace CoolDeck
{
    /// <summary>Tiny host so a hand-built DrawingVisual can sit in the visual tree.</summary>
    public class DrawingVisualHost : FrameworkElement
    {
        Visual _visual;
        public Visual Visual
        {
            get { return _visual; }
            set { _visual = value; AddVisualChild(value); AddLogicalChild(value); }
        }
        protected override int VisualChildrenCount { get { return _visual == null ? 0 : 1; } }
        protected override Visual GetVisualChild(int index) { return _visual; }
    }

    public partial class MainWindow : Window
    {
        readonly Controller _ctl = new Controller();
        readonly DispatcherTimer _timer = new DispatcherTimer();
        readonly DispatcherTimer _saveTimer = new DispatcherTimer();

        FanGauge _gCpu, _gGpu;
        PillRow _modes;
        CurveEditor _curve;
        TextBlock _curveFanLabel;
        readonly List<Border> _curveTabs = new List<Border>();
        int _curveFan;

        OffsetSlider _offset;
        TextBlock _offsetVal;
        TextBlock _dllInfo, _status;

        readonly List<TextBlock> _sensorDuty = new List<TextBlock>();
        readonly List<TextBlock> _sensorTemp = new List<TextBlock>();
        readonly List<TextBlock> _sensorRpm = new List<TextBlock>();

        bool _suppressModeEvent;
        bool _curveDirty;
        Button _saveCurve;
        TextBlock _curveHint;
        readonly List<Button> _presetBtns = new List<Button>();
        readonly List<int[][]> _presetTables = new List<int[][]>();
        int _presetSel = -1;
        TextBlock _fanSlots;
        bool _showAllFans;
        bool _idlePoll;
        Profile _prof;
        CheckBox _autoApply;
        ScrollViewer _bodyScroll;
        int _maxRpm = 6000;

        /// <summary>Set by the tray "Exit" item so the close handler really exits.</summary>
        public bool RealClose { get; set; }

        // ---------------- tray-facing API ----------------
        // The tray menu reads the live EC mode instead of a cached one: while the window is
        // hidden its poll timer is stopped, so anything remembered from the last tick could be
        // minutes out of date — and that is precisely when the tray is the only thing visible.

        /// <summary>Current EC fan mode, or -1 when the controller is not usable.</summary>
        public int ModeNow()
        {
            try { return _ctl.Ready ? _ctl.ReadMode() : -1; }
            catch { return -1; }
        }

        /// <summary>Writes a fan mode straight from the tray menu.</summary>
        public void PickMode(int mode)
        {
            if (!_ctl.Ready) return;
            if (mode == (int)FanMode.Custom) { ApplySavedCurve(); return; }
            var m = (FanMode)mode;
            _ctl.SetMode(m);
            SaveModeIntent(m);
            Trace.W("tray mode -> " + mode);
            if (IsVisible) Tick();
        }

        /// <summary>Re-uploads the curves stored in the profile and switches the EC to Custom.
        /// This is what the tray's 自定义 entry means: the named Custom mode alone only stops the
        /// EC re-deriving its table, it does not put the user's curve back into it.</summary>
        public void ApplySavedCurve()
        {
            if (!_ctl.Ready) return;
            if (_prof == null) _prof = Profile.Load();
            if (_prof == null) { _status.Text = Loc.T("no saved curve yet"); return; }
            _ctl.SetMode(FanMode.Custom);   // even if no fan is confirmed present yet
            for (int f = 0; f <= 2; f++)
            {
                // Only channels this chassis actually has: an absent fan's EC slot still holds
                // leftovers, and every extra DCHU write widens the stub-answer window.
                if (!_ctl.FanSeen(f)) continue;
                _ctl.ApplyCurve(f, _prof.Fans[f].ToPoints());
                if (_curveFan == f) { _presetSel = _presetBtns.Count - 1; _curveDirty = false; }
            }
            Trace.W("tray: applied saved curve, mode -> Custom");
            if (IsVisible) { MarkDirty(); Tick(); }
        }

        static readonly FanMode[] ModeList = {
            FanMode.Automatic, FanMode.Silent, FanMode.Noiseless, FanMode.Maximum,
            FanMode.MaxQ, FanMode.Custom, FanMode.IFSC, FanMode.NoiselessEx
        };
        static readonly string[] ModeLabels = {
            Loc.T("Automatic"), Loc.T("Silent"), Loc.T("Noiseless"), Loc.T("Maximum"), Loc.T("MaxQ"), Loc.T("Custom"), Loc.T("IQST"), Loc.T("NoiselessEx")
        };
        // Modes that are accepted by the EC but do nothing measurable on AC power on this
        // firmware — surfaced greyed so the user is not sent hunting for a quiet mode that
        // is not there. (Verified: duty is unchanged vs Automatic at the same temperature.)
        static readonly bool[] ModeInert = { false, true, true, false, false, false, false, false };
        // t0,d0,t1,d1,t2,d2,t3,d3. The first and last pairs are each fan's EC-owned endpoints,
        // so they differ per fan: CPU is 40..100, GPU is 48..95. Getting these wrong made the
        // presets describe a domain the GPU fan cannot hold.
        static readonly int[][] DefaultCurves = {
            new[] { 40, 35, 60, 55, 80, 83, 100, 100 },
            new[] { 48, 35, 60, 51, 80, 83, 95, 100 },
            new[] { 40, 35, 60, 55, 80, 83, 100, 100 },
            new[] { 40, 35, 60, 55, 80, 83, 100, 100 }
        };
        static readonly int[][] QuietCurve = {
            new[] { 40, 25, 75, 25, 90, 45, 100, 100 },
            new[] { 48, 25, 75, 25, 90, 45, 95, 100 },
            new[] { 40, 25, 75, 25, 90, 45, 100, 100 },
            new[] { 40, 25, 75, 25, 90, 45, 100, 100 }
        };
        static readonly int[][] BalancedCurve = {
            new[] { 40, 30, 70, 40, 85, 70, 100, 100 },
            new[] { 48, 30, 70, 40, 85, 70, 95, 100 },
            new[] { 40, 30, 70, 40, 85, 70, 100, 100 },
            new[] { 40, 30, 70, 40, 85, 70, 100, 100 }
        };
        static readonly int[][] CoolCurve = {
            new[] { 40, 45, 55, 60, 75, 95, 100, 100 },
            new[] { 48, 45, 55, 60, 75, 95, 95, 100 },
            new[] { 40, 45, 55, 60, 75, 95, 100, 100 },
            new[] { 40, 45, 55, 60, 75, 95, 100, 100 }
        };

        public MainWindow()
        {
            Trace.W("MainWindow ctor");
            Width = 940; Height = 726;
            MinWidth = 820; MinHeight = 600;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            WindowStyle = WindowStyle.None;
            Background = Theme.Bg0Brush;
            ResizeMode = ResizeMode.CanResize;
            FontFamily = new FontFamily(Theme.Font);
            Foreground = Theme.TxtBrush;

            // Take the frame over from WPF. With a bare WindowStyle=None + CanResize the DWM
            // still paints a ~7px #F3F3F3 resize strip across the top of the window, which
            // reads as a flickering white line on a dark chrome. WindowChrome with a zero
            // glass frame puts the client area over the whole window and handles the resize
            // borders itself. (.NET 4.0 API: the attach method is SetWindowChrome.)
            var chrome = new System.Windows.Shell.WindowChrome
            {
                CaptionHeight = 38,
                ResizeBorderThickness = new Thickness(6),
                GlassFrameThickness = new Thickness(0),
                // Told DWM how much rounding to expect. A bare WindowStyle=None window gets
                // square corners; see RoundCorners() for the DWM call that actually rounds them.
                CornerRadius = new CornerRadius(10),
                UseAeroCaptionButtons = false
            };
            System.Windows.Shell.WindowChrome.SetWindowChrome(this, chrome);

            // DWM only rounds a window once its HWND exists, so this cannot be done in the
            // constructor alongside the chrome setup.
            SourceInitialized += delegate { RoundCorners(); };

            var root = new Grid();
            root.RowDefinitions.Add(new RowDefinition { Height = new GridLength(38) });   // titlebar
            root.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            root.RowDefinitions.Add(new RowDefinition { Height = new GridLength(24) });   // statusbar
            Content = root;

            root.Children.Add(TitleBar());
            Trace.W("titlebar ok");
            root.Children.Add(Body());
            Trace.W("body ok");
            root.Children.Add(StatusBar());
            Trace.W("statusbar ok");
            Grid.SetRow(root.Children[0], 0);
            Grid.SetRow(root.Children[1], 1);
            Grid.SetRow(root.Children[2], 2);

            Loaded += delegate { Start(); };

            // Polling while nobody can see the window is pure waste: CPU, allocations and — the
            // part that actually matters here — continuous DCHU traffic, which is what makes the
            // EC answer with its stub body. Stop when hidden or minimised, resync on the way back.
            IsVisibleChanged += delegate { UpdatePolling(); };
            StateChanged += delegate { UpdatePolling(); };
            // An unfocused monitor does not need the same refresh rate as one being read.
            Activated += delegate { _idlePoll = false; UpdatePolling(); };
            Deactivated += delegate { _idlePoll = true; UpdatePolling(); };
            // ScrollViewer auto-scrolls to whatever child last took focus, which on startup
            // left the gauges cut off at the top. Pin it back to the top once laid out.
            Loaded += delegate
            {
                Dispatcher.BeginInvoke((Action)delegate
                {
                    if (_bodyScroll != null) _bodyScroll.ScrollToTop();
                }, System.Windows.Threading.DispatcherPriority.Background);
            };
            // Close (X) sends the window to the tray rather than killing fan control; the
            // tray menu has an explicit Exit that sets RealClose. App.cs owns the tray
            // visibility so the two handlers don't fight over it.
            Closing += delegate(object sender, System.ComponentModel.CancelEventArgs e)
            {
                if (!RealClose)
                {
                    e.Cancel = true;
                    Hide();
                    // The timer is already stopped by UpdatePolling (IsVisibleChanged fires
                    // on Hide). Page the window out too: a tray-sitting monitor has no
                    // business holding 80+ MB resident while doing literally nothing.
                    Native.TrimWorkingSet();
                }
                else
                {
                    _timer.Stop();
                }
            };
        }

        bool _started;

        /// <summary>Boot/autostart entry point: do everything Loaded would do (controller
        /// init, profile restore) without ever showing the window. Loaded still fires on
        /// the first manual Show and calls Start() again — the _started guard makes that
        /// a no-op instead of a double timer subscription.</summary>
        public void SilentStart()
        {
            Start();
            UpdatePolling();   // hidden => timer stays stopped; tray menu reads live EC
            // Construction + first paint fault in tens of MB that a hidden window never
            // needs resident. Wait out the 3 s boot-restore first, then trim only if the
            // user never opened the window in the meantime.
            var trim = new DispatcherTimer { Interval = TimeSpan.FromSeconds(12) };
            trim.Tick += delegate
            {
                trim.Stop();
                if (!IsVisible) Native.TrimWorkingSet();
            };
            trim.Start();
        }

        // ---------------- chrome ----------------

        FrameworkElement TitleBar()
        {
            // Explicit 1px bottom rule: the title bar and the body are both near-black, so
            // without a separator the 38px strip reads as dead space.
            var g = new Grid { Background = Theme.B(Theme.Bar) };
            g.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            g.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1) });
            g.Children.Add(new Border { Background = Theme.LineBrush });
            Grid.SetRow(g.Children[0], 1);

            g.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            g.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            g.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            // Logo: the real fan mark, not a bare gradient square. A plain colour block read
            // as a placeholder. Drawn with a small DrawingVisual so it stays crisp and needs
            // no image asset.
            var logo = new Border
            {
                Width = 20, Height = 20, CornerRadius = new CornerRadius(6),
                Margin = new Thickness(14, 0, 9, 0),
                Background = Theme.AccentGradient(),
                VerticalAlignment = VerticalAlignment.Center,
                Child = FanMark()
            };
            var title = new TextBlock
            {
                Text = Loc.T("CoolDeck"),
                FontFamily = new FontFamily(Theme.FontDisplay),
                FontSize = 13, Foreground = Theme.TxtBrush,
                VerticalAlignment = VerticalAlignment.Center
            };
            var sub = new TextBlock
            {
                Text = Loc.T("fan control"),
                FontSize = 11, Foreground = Theme.TxtFaintBrush,
                Margin = new Thickness(8, 0, 0, 0), VerticalAlignment = VerticalAlignment.Center
            };
            var left = new StackPanel { Orientation = Orientation.Horizontal };
            left.Children.Add(logo); left.Children.Add(title); left.Children.Add(sub);
            g.Children.Add(left);

            var btns = new StackPanel { Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Center };
            // "Aa" rather than a sun/moon glyph: this font stack has already bitten once with
            // icon characters rendering as tofu, and Aa reads unambiguously as "appearance".
            var themeBtn = ChromeBtn("Aa", delegate { SwitchTheme(); });
            themeBtn.ToolTip = Theme.IsLight ? "Switch to dark" : "Switch to light";
            btns.Children.Add(themeBtn);
            btns.Children.Add(ChromeBtn("\u2013", delegate { WindowState = WindowState.Minimized; }));
            btns.Children.Add(ChromeBtn("\u00d7", delegate { Close(); }, true));
            g.Children.Add(btns);
            Grid.SetColumn(btns, 2);

            g.MouseLeftButtonDown += delegate { try { DragMove(); } catch { } };
            return g;
        }

        // ---------------- rounded window corners ----------------

        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int value, int size);

        const int DWMWA_WINDOW_CORNER_PREFERENCE = 33;   // Windows 11 only
        const int DWMWCP_ROUND = 2;

        /// <summary>
        /// Ask DWM for real rounded window corners. WindowStyle=None on its own yields a
        /// square window; the chrome CornerRadius only tells WPF how to inset the client area,
        /// it does not round anything. Unsupported builds simply return a failure code.
        /// </summary>
        void RoundCorners()
        {
            try
            {
                var helper = new System.Windows.Interop.WindowInteropHelper(this);
                IntPtr hwnd = helper.Handle;
                if (hwnd == IntPtr.Zero) return;
                int pref = DWMWCP_ROUND;
                DwmSetWindowAttribute(hwnd, DWMWA_WINDOW_CORNER_PREFERENCE, ref pref, 4);
            }
            catch { }
        }

        void SwitchTheme()
        {
            bool target = !Theme.IsLight;
            if (!Theme.SavePreference(target))
            {
                _status.Text = Loc.T("could not write the theme preference to ") + Theme.PreferencePath;
                return;
            }
            // Palette is resolved once at startup (see Theme's static ctor), so applying it
            // means a fresh process.
            Relaunch();
            RealClose = true;
            var app = System.Windows.Application.Current;
            if (app != null) app.Shutdown(); else Environment.Exit(0);
        }

        /// <summary>Start a fresh copy of this exe after a short delay, then let the caller
        /// exit. The delay is required: the single-instance mutex means a process launched
        /// while we are still alive would simply signal us and quit instead of starting.</summary>
        static void Relaunch()
        {
            try
            {
                string exe = System.Reflection.Assembly.GetEntryAssembly().Location;
                var psi = new System.Diagnostics.ProcessStartInfo("cmd.exe",
                    "/c ping -n 2 127.0.0.1 >nul & start \"\" \"" + exe + "\"")
                {
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                System.Diagnostics.Process.Start(psi);
            }
            catch { }
        }

        /// <summary>The fan mark used in the title bar: five blades around a hub, drawn in the
        /// window background colour so it reads as a cut-out of the gradient tile.</summary>
        static FrameworkElement FanMark()
        {
            var dv = new DrawingVisual();
            using (var dc = dv.RenderOpen())
            {
                var dark = Theme.B(Theme.Bg0);
                var accent = Theme.B(Theme.Accent);
                var centre = new Point(10, 10);
                // Blade geometry matters at 20px: fat, close-in circles merge into one blob.
                // Smaller blades pushed further out keep the gaps visible so it reads as a fan.
                for (int i = 0; i < 5; i++)
                {
                    double a = (i * 72.0 - 90.0) * Math.PI / 180.0;
                    var at = new Point(centre.X + Math.Cos(a) * 5.0, centre.Y + Math.Sin(a) * 5.0);
                    dc.DrawEllipse(dark, null, at, 3.5, 3.5);
                }
                dc.DrawEllipse(dark, null, centre, 3.2, 3.2);
                dc.DrawEllipse(accent, null, centre, 1.7, 1.7);
            }
            var host = new DrawingVisualHost { Visual = dv };
            host.IsHitTestVisible = false;
            return host;
        }

        Button ChromeBtn(string glyph, Action act, bool danger = false)
        {
            var b = new Button
            {
                Content = glyph, Width = 44, Height = 32,
                Background = Brushes.Transparent, BorderThickness = new Thickness(0),
                Foreground = danger ? Theme.B(Theme.Danger) : Theme.TxtDimBrush,
                // NOTE: no Segoe MDL2 Assets here — that font only carries private-use-area
                // icon codepoints, so plain text glyphs render as tofu boxes. Use the UI font.
                FontFamily = new FontFamily(Theme.Font),
                FontSize = 13, Cursor = Cursors.Hand,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                Style = FlatButtonStyle()
            };
            // Inside a WindowChrome caption the DWM owns the whole strip: mouse input over it
            // is consumed for dragging and never reaches WPF children. Without this the
            // minimise/close buttons look right but are completely dead to clicks.
            System.Windows.Shell.WindowChrome.SetIsHitTestVisibleInChrome(b, true);
            b.Click += delegate { act(); };
            // Subtle hover: a faint tint instead of the previous flat grey slab, and a
            // stronger red wash for close (the convention users expect).
            b.MouseEnter += delegate
            {
                b.Background = Theme.B(danger ? Theme.DangerHover : Theme.HoverTint);
                // White glyph suits the dark red hover, but the light palette's hover is a
                // pale pink where white is invisible — there the danger colour is legible.
                b.Foreground = danger
                    ? (Theme.IsLight ? Theme.DangerBrush : (Brush)Theme.B(Theme.C(0xFF, 0xFF, 0xFF)))
                    : Theme.TxtBrush;
            };
            b.MouseLeave += delegate
            {
                b.Background = Brushes.Transparent;
                b.Foreground = danger ? Theme.B(Theme.Danger) : Theme.TxtDimBrush;
            };
            return b;
        }

        // ---------------- body ----------------

        FrameworkElement Body()
        {
            var g = new Grid { Margin = new Thickness(16) };
            g.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            g.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(252) });

            var leftCol = new StackPanel();
            leftCol.Children.Add(GaugeRow());
            leftCol.Children.Add(ModeCard());
            leftCol.Children.Add(CurveCard());
            g.Children.Add(leftCol);

            var rightCol = new StackPanel { Margin = new Thickness(14, 0, 0, 0) };
            rightCol.Children.Add(OffsetCard());
            rightCol.Children.Add(SensorCard());
            rightCol.Children.Add(ActionCard());
            g.Children.Add(rightCol);
            Grid.SetColumn(rightCol, 1);

            // The two columns are taller than the default window on smaller displays, which
            // used to clip the curve card's button row (Apply/Default) clean off the bottom.
            // Scroll instead of clipping so every control stays reachable at any size.
            var sc = new ScrollViewer
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
                Padding = new Thickness(0, 0, 2, 0),
                Content = g
            };
            _bodyScroll = sc;
            return sc;
        }

        FrameworkElement GaugeRow()
        {
            var g = new Grid { Height = 176 };
            g.ColumnDefinitions.Add(new ColumnDefinition());
            g.ColumnDefinitions.Add(new ColumnDefinition());

            _gCpu = new FanGauge { Margin = new Thickness(0, 0, 7, 0) };
            _gGpu = new FanGauge { Margin = new Thickness(7, 0, 0, 0) };
            foreach (var gg in new[] { _gCpu, _gGpu })
            {
                var card = Card(gg);
                g.Children.Add(card);
            }
            Grid.SetColumn(_gCpu.Parent as FrameworkElement, 0);
            Grid.SetColumn(_gGpu.Parent as FrameworkElement, 1);
            return g;
        }

        FrameworkElement Card(FrameworkElement inner)
        {
            var b = new Border
            {
                Background = Theme.CardBrush,
                BorderBrush = Theme.LineBrush, BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(12),
                Padding = new Thickness(10),
                Child = inner
            };
            return b;
        }

        FrameworkElement ModeCard()
        {
            var sp = new StackPanel { Margin = new Thickness(0, 9, 0, 0) };
            sp.Children.Add(SectionHeader(Loc.T("Fan Mode"), Loc.T("applies immediately to the embedded controller")));

            var body = new Border
            {
                Background = Theme.CardBrush, BorderBrush = Theme.LineBrush,
                BorderThickness = new Thickness(1), CornerRadius = new CornerRadius(12),
                Padding = new Thickness(12, 10, 12, 12), Margin = new Thickness(0, 6, 0, 0)
            };
            _modes = new PillRow { Height = 28 };
            _modes.SetItems(ModeLabels, ModeInert);
            _modes.Changed += OnModePicked;

            var mnote = new TextBlock
            {
                Text = Loc.T("Silent and Noiseless do nothing on AC power here \u2014 for quiet use Offset 0% or the Quiet preset."),
                FontSize = 9.5, Foreground = Theme.TxtFaintBrush,
                Margin = new Thickness(0, 6, 0, 0), TextWrapping = TextWrapping.Wrap
            };
            var wrap = new StackPanel();
            wrap.Children.Add(_modes);
            wrap.Children.Add(mnote);
            body.Child = wrap;
            sp.Children.Add(body);
            return sp;
        }

        FrameworkElement CurveCard()
        {
            var sp = new StackPanel { Margin = new Thickness(0, 9, 0, 0) };

            var head = new Grid();
            head.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            head.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            head.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            head.Children.Add(SectionHeader(Loc.T("Fan Curve"), Loc.T("read live from the EC — drag points to edit")));
            _curveFanLabel = new TextBlock
            {
                Text = Loc.T("CPU Fan"), Foreground = Theme.AccentBrush, FontSize = 11,
                VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(0, 0, 10, 0)
            };
            head.Children.Add(_curveFanLabel);
            Grid.SetColumn(_curveFanLabel, 2);
            sp.Children.Add(head);

            var body = new Border
            {
                Background = Theme.CardBrush, BorderBrush = Theme.LineBrush,
                BorderThickness = new Thickness(1), CornerRadius = new CornerRadius(12),
                Padding = new Thickness(12), Margin = new Thickness(0, 6, 0, 0)
            };

            var inner = new StackPanel();
            var tabRow = new Grid { Margin = new Thickness(0, 0, 0, 8) };
            tabRow.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            tabRow.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            var tabs = new StackPanel { Orientation = Orientation.Horizontal };
            string[] names = { Loc.T("CPU Fan"), Loc.T("GPU Fan"), Loc.T("GPU Fan 2"), Loc.T("Case Fan") };
            for (int i = 0; i < 4; i++)
            {
                int idx = i;
                var tb = new TextBlock
                {
                    Text = names[i], FontSize = 11, Foreground = Theme.TxtDimBrush,
                    Padding = new Thickness(10, 4, 10, 4), Cursor = Cursors.Hand
                };
                var bd = new Border
                {
                    Child = tb, CornerRadius = new CornerRadius(6),
                    Background = Theme.B(Theme.Chip),
                    Margin = new Thickness(0, 0, 6, 0)
                };
                bd.MouseLeftButtonUp += delegate { SelectCurveFan(idx); };
                _curveTabs.Add(bd);
                tabs.Children.Add(bd);
            }
            tabRow.Children.Add(tabs);

            // "GPU Fan 2" and "Case Fan" are EC slots this chassis does not populate, so by
            // default only fans we have actually observed are shown. The escape hatch keeps
            // the EC's full table reachable — hiding must never mean losing access.
            _fanSlots = new TextBlock
            {
                Text = Loc.T("show all EC slots"), FontSize = 9.5, Foreground = Theme.TxtFaintBrush,
                VerticalAlignment = VerticalAlignment.Center, Cursor = Cursors.Hand,
                Margin = new Thickness(0, 0, 2, 0)
            };
            // Plain faint text reads as a label, not a control. Hover feedback says clickable.
            _fanSlots.MouseEnter += delegate { _fanSlots.Foreground = Theme.AccentBrush; };
            _fanSlots.MouseLeave += delegate
            {
                _fanSlots.Foreground = _showAllFans ? Theme.AccentBrush : Theme.TxtFaintBrush;
            };
            _fanSlots.MouseLeftButtonUp += delegate
            {
                _showAllFans = !_showAllFans;
                ApplyFanVisibility();
            };
            tabRow.Children.Add(_fanSlots);
            Grid.SetColumn(_fanSlots, 1);
            inner.Children.Add(tabRow);

            _curve = new CurveEditor { Height = 150, ClipToBounds = true };
            _curve.Edited += delegate { _curveDirty = true; _presetSel = -1; MarkDirty(); };
            inner.Children.Add(_curve);

            var row = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 8, 0, 0) };
            row.Children.Add(GhostButton(Loc.T("Default"), delegate
            {
                _curve.ResetTo(DefaultCurves[_curveFan]);
                _curveDirty = true;
                MarkDirty();
            }));
            _saveCurve = SaveButton(Loc.T("Save curve to fans"), delegate { ApplyCurve(); });
            row.Children.Add(_saveCurve);
            _curveHint = new TextBlock
            {
                Text = Loc.T("drag the two middle points, then Save. The EC owns 40\u00b0C and 100\u00b0C."),
                FontSize = 9.5, Foreground = Theme.TxtFaintBrush,
                VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(10, 0, 0, 0),
                TextWrapping = TextWrapping.Wrap
            };
            row.Children.Add(_curveHint);
            inner.Children.Add(row);

            // Presets. This is the real quiet lever on this firmware: the named "silent"
            // modes are no-ops on AC power, and the offset only ever raises the duty.
            var presets = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 8, 0, 0) };
            AddPreset(presets, Loc.T("Quiet"), QuietCurve);
            AddPreset(presets, Loc.T("Balanced"), BalancedCurve);
            AddPreset(presets, Loc.T("Stock"), DefaultCurves);
            AddPreset(presets, Loc.T("Cool"), CoolCurve);
            // The user's own saved curve, live from %APPDATA%\CoolDeck\profile.txt.
            AddPreset(presets, Loc.T("User"), null);
            inner.Children.Add(presets);
            var pnote = new TextBlock
            {
                Text = Loc.T("Quiet keeps the fans near idle until 75 \u00b0C \u2014 watch your temperatures after applying."),
                FontSize = 9.5, Foreground = Theme.TxtFaintBrush,
                Margin = new Thickness(0, 6, 0, 0), TextWrapping = TextWrapping.Wrap
            };
            inner.Children.Add(pnote);

            body.Child = inner;
            sp.Children.Add(body);
            return sp;
        }

        FrameworkElement OffsetCard()
        {
            var sp = new StackPanel();
            sp.Children.Add(SectionHeader(Loc.T("Speed Offset"), Loc.T("a boost added on top of the automatic curve")));
            var body = new Border
            {
                Background = Theme.CardBrush, BorderBrush = Theme.LineBrush,
                BorderThickness = new Thickness(1), CornerRadius = new CornerRadius(12),
                Padding = new Thickness(12, 10, 12, 12), Margin = new Thickness(0, 6, 0, 0)
            };
            var inner = new StackPanel();

            var top = new Grid();
            top.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            top.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            _offsetVal = new TextBlock
            {
                Text = Loc.T("0 %"), FontFamily = new FontFamily(Theme.FontDisplay), FontSize = 20,
                Foreground = Theme.AccentBrush, HorizontalAlignment = HorizontalAlignment.Right
            };
            top.Children.Add(_offsetVal);
            inner.Children.Add(top);

            _offset = new OffsetSlider
            {
                Minimum = 0, Maximum = 100, Value = 0, Step = 5, Height = 22,
                Margin = new Thickness(0, 4, 0, 0)
            };
            _offset.ValueChanged += delegate
            {
                int v = (int)Math.Round(_offset.Value);
                _offsetVal.Text = v + " %";
            };
            // One commit path for mouse and keyboard alike. The old keyboard handler called
            // SaveProfile() (re-capture live EC state); SaveOffsetIntent() is the correct one —
            // it persists what the user asked for instead of absorbing firmware noise.
            _offset.ValuePicked += delegate { _ctl.SetOffset((int)Math.Round(_offset.Value)); SaveOffsetIntent((int)Math.Round(_offset.Value)); };
            inner.Children.Add(_offset);

            // Presets. Offset is one-directional on this firmware: it only ever raises the
            // duty the automatic curve asks for (0% = untouched, ~40% already saturates).
            // So the quiet end of the scale is 0, not 100. "Boost" doubles as the loud
            // preset, so no separate Max button (it used to overflow the card).
            // UniformGrid so the three buttons share the card width exactly instead of
            // overflowing it — the card is only ~228px of usable space.
            var presets = new UniformGrid
            {
                Rows = 1, Columns = 3,
                Margin = new Thickness(0, 8, 0, 0)
            };
            presets.Children.Add(MiniButton(Loc.T("Quiet"), delegate { SetOffsetUi(0); }, true));
            presets.Children.Add(MiniButton(Loc.T("Boost +25"), delegate { SetOffsetUi(25); }, true));
            presets.Children.Add(MiniButton(Loc.T("Max +100"), delegate { SetOffsetUi(100); }, true));
            inner.Children.Add(presets);

            var hint = new TextBlock
            {
                Text = Loc.T(
                       "Raises the duty the automatic curve asks for. 0 % = untouched; " +
                       "above ~40 % pins the fans at full speed."),
                FontSize = 9.5, Foreground = Theme.TxtFaintBrush,
                Margin = new Thickness(0, 6, 0, 0), TextWrapping = TextWrapping.Wrap
            };
            inner.Children.Add(hint);

            body.Child = inner;
            sp.Children.Add(body);
            return sp;
        }

        void SetOffsetUi(int v)
        {
            _offset.Value = v;
            _ctl.SetOffset(v);
            SaveOffsetIntent(v);
        }

        /// <summary>Primary action button — accent gradient, turns amber-tinted while there
        /// are unapplied curve edits so it is obvious something still needs saving.</summary>
        Button SaveButton(string text, Action act)
        {
            var b = MakeButton(text, act, Theme.AccentGradient(), Brushes.Transparent,
                               Theme.B(Theme.OnAccent), 148, true);
            b.Margin = new Thickness(0, 0, 8, 0);
            return b;
        }

        void MarkDirty()
        {
            if (_saveCurve == null) return;
            _saveCurve.Content = Loc.T(_curveDirty ? "Save curve to fans  \u2022" : "Save curve to fans");
            if (_curveHint != null)
            {
                // The window is quoted from the fan actually being edited — GPU is 48..95,
                // CPU is 40..100, and a hard-coded message would be wrong on one of them.
                // Whole sentences with {0}/{1} rather than fragments glued together: a
                // translation may need the numbers in a different order.
                _curveHint.Text = _curveDirty
                    ? Loc.F("unsaved changes \u2014 press Save to upload them. This fan's window is {0}\u2013{1} \u00b0C; the EC owns both ends.",
                            _curve.TempLo, _curve.TempHi)
                    : Loc.F("drag the two middle points, then Save. This fan's window is {0}\u2013{1} \u00b0C; the EC owns both ends.",
                            _curve.TempLo, _curve.TempHi);
            }
            UpdatePresetSelection();
        }

        Button MiniButton(string text, Action act, bool stretch = false)
        {
            var b = MakeButton(text, act, Theme.B(Theme.Chip), Theme.LineBrush,
                              Theme.TxtDimBrush, 0, false);
            if (stretch)
            {
                // inside a UniformGrid: fill the cell, drop the inter-button margin
                b.Margin = new Thickness(0, 0, 6, 0);
                b.Padding = new Thickness(6, 0, 6, 0);
                b.HorizontalAlignment = HorizontalAlignment.Stretch;
            }
            return b;
        }

        FrameworkElement SensorCard()
        {
            var sp = new StackPanel { Margin = new Thickness(0, 9, 0, 0) };
            sp.Children.Add(SectionHeader(Loc.T("Sensors"), Loc.T("live EC telemetry")));
            var body = new Border
            {
                Background = Theme.CardBrush, BorderBrush = Theme.LineBrush,
                BorderThickness = new Thickness(1), CornerRadius = new CornerRadius(12),
                Padding = new Thickness(12, 8, 12, 10), Margin = new Thickness(0, 6, 0, 0)
            };
            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            for (int i = 0; i < 4; i++) grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(26) });

            var hdr = new Grid { Margin = new Thickness(0, 2, 0, 4) };
            hdr.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            hdr.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(58) });
            hdr.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(46) });
            hdr.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(66) });
            hdr.Children.Add(Micro("FAN"));
            hdr.Children.Add(Micro("RPM", true));
            hdr.Children.Add(Micro("DUTY", true));
            hdr.Children.Add(Micro("TEMP", true));
            Grid.SetColumn(hdr.Children[1], 1); Grid.SetColumn(hdr.Children[2], 2); Grid.SetColumn(hdr.Children[3], 3);
            grid.Children.Add(hdr);

            string[] names = { Loc.T("CPU"), Loc.T("GPU"), Loc.T("GPU 2"), Loc.T("Case") };
            for (int i = 0; i < 4; i++)
            {
                var row = new Grid();
                row.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                row.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(58) });
                row.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(46) });
                row.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(66) });

                var nm = new TextBlock { Text = names[i], FontSize = 11, Foreground = Theme.TxtDimBrush, VerticalAlignment = VerticalAlignment.Center };
                var rpm = new TextBlock { Text = Loc.T("--"), FontSize = 11, Foreground = Theme.TxtBrush, HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Center };
                var duty = new TextBlock { Text = Loc.T("--"), FontSize = 11, Foreground = Theme.TxtBrush, HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Center };
                var temp = new TextBlock { Text = Loc.T("--"), FontSize = 11, Foreground = Theme.TxtBrush, HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Center };

                row.Children.Add(nm); row.Children.Add(rpm); row.Children.Add(duty); row.Children.Add(temp);
                Grid.SetColumn(rpm, 1); Grid.SetColumn(duty, 2); Grid.SetColumn(temp, 3);
                if (i > 0)
                {
                    // Hairline between rows so the four fans read as a table, not four
                    // floating lines. Overlays the row's top edge; the 26 px row leaves
                    // the centred text clear of it.
                    var sep = new Border
                    {
                        Height = 1, Background = Theme.LineBrush,
                        VerticalAlignment = VerticalAlignment.Top,
                        SnapsToDevicePixels = true
                    };
                    Grid.SetColumnSpan(sep, 4);
                    row.Children.Add(sep);
                }
                _sensorRpm.Add(rpm); _sensorDuty.Add(duty); _sensorTemp.Add(temp);
                grid.Children.Add(row);
                Grid.SetRow(row, i + 1);
            }
            body.Child = grid;
            sp.Children.Add(body);
            return sp;
        }

        /// <summary>
        /// A CheckBox drawn to match the buttons. The stock WPF template is the same trap the
        /// buttons fell into: it ignores Background/BorderBrush and paints system chrome, which
        /// looks like a foreign control dropped into the card. This is a rounded box that fills
        /// with the accent gradient when ticked, plus the label.
        /// </summary>
        static Style CheckBoxStyle()
        {
            var s = new Style(typeof(CheckBox));
            var t = new ControlTemplate(typeof(CheckBox));

            var outer = new FrameworkElementFactory(typeof(StackPanel));
            outer.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);

            var box = new FrameworkElementFactory(typeof(Border), "box");
            box.SetValue(Border.WidthProperty, 18.0);
            box.SetValue(Border.HeightProperty, 18.0);
            box.SetValue(Border.CornerRadiusProperty, new CornerRadius(5));
            box.SetValue(Border.BackgroundProperty, Theme.B(Theme.Btn));
            box.SetValue(Border.BorderBrushProperty, Theme.B(Theme.BtnLine));
            box.SetValue(Border.BorderThicknessProperty, new Thickness(1.5));
            box.SetValue(Border.VerticalAlignmentProperty, VerticalAlignment.Center);

            // U+2713 is a real Segoe UI glyph. The earlier tofu incident was caused by asking
            // Segoe MDL2 Assets (a private-use-area icon font) to draw ordinary characters.
            var chk = new FrameworkElementFactory(typeof(TextBlock), "chk");
            chk.SetValue(TextBlock.TextProperty, "\u2713");
            chk.SetValue(TextBlock.FontSizeProperty, 12.0);
            chk.SetValue(TextBlock.FontWeightProperty, FontWeights.Bold);
            chk.SetValue(TextBlock.ForegroundProperty, Theme.OnAccentBrush);
            chk.SetValue(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center);
            chk.SetValue(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center);
            chk.SetValue(UIElement.VisibilityProperty, Visibility.Collapsed);
            box.AppendChild(chk);
            outer.AppendChild(box);

            var lbl = new FrameworkElementFactory(typeof(ContentPresenter));
            lbl.SetValue(ContentPresenter.MarginProperty, new Thickness(9, 0, 0, 0));
            lbl.SetValue(ContentPresenter.VerticalAlignmentProperty, VerticalAlignment.Center);
            outer.AppendChild(lbl);

            t.VisualTree = outer;

            // Hover first so the checked trigger, added after it, wins when both apply.
            var hover = new Trigger { Property = UIElement.IsMouseOverProperty, Value = true };
            hover.Setters.Add(new Setter(Border.BorderBrushProperty,
                Theme.B(Theme.AccentSoftLine), "box"));
            t.Triggers.Add(hover);

            var on = new Trigger { Property = System.Windows.Controls.Primitives.ToggleButton.IsCheckedProperty, Value = true };
            on.Setters.Add(new Setter(Border.BackgroundProperty, Theme.AccentGradient(), "box"));
            on.Setters.Add(new Setter(Border.BorderBrushProperty, Theme.B(Theme.SelLine), "box"));
            on.Setters.Add(new Setter(UIElement.VisibilityProperty, Visibility.Visible, "chk"));
            t.Triggers.Add(on);

            s.Setters.Add(new Setter(Control.TemplateProperty, t));
            s.Setters.Add(new Setter(Control.BackgroundProperty, Brushes.Transparent));
            s.Setters.Add(new Setter(Control.BorderThicknessProperty, new Thickness(0)));
            s.Seal();
            return s;
        }

        FrameworkElement ActionCard()
        {
            var sp = new StackPanel { Margin = new Thickness(0, 9, 0, 0) };
            sp.Children.Add(SectionHeader(Loc.T("Actions"), Loc.T("")));
            var body = new Border
            {
                Background = Theme.CardBrush, BorderBrush = Theme.LineBrush,
                BorderThickness = new Thickness(1), CornerRadius = new CornerRadius(12),
                Padding = new Thickness(12), Margin = new Thickness(0, 6, 0, 0)
            };
            var st = new StackPanel();

            // The persistence switch. Without this the curve only lives in the EC, and a boot
            // that comes back in Automatic silently voids it.
            _autoApply = new CheckBox
            {
                Content = Loc.T("Re-apply my curve at startup"),
                IsChecked = _prof == null || _prof.AutoApply,
                Foreground = Theme.TxtBrush,
                FontSize = 11.5,
                Margin = new Thickness(2, 0, 0, 14),
                Cursor = Cursors.Hand,
                Padding = new Thickness(0),
                Style = CheckBoxStyle()
            };
            _autoApply.Checked += delegate { if (_prof != null) { _prof.AutoApply = true; _prof.Save(); } };
            _autoApply.Unchecked += delegate { if (_prof != null) { _prof.AutoApply = false; _prof.Save(); } };
            st.Children.Add(_autoApply);

            // GhostButton carries a right margin for horizontal rows; in a vertical stack that
            // leaves the buttons touching each other. Give them real vertical gaps.
            var dust = GhostButton(Loc.T("Run Anti-Dust"), delegate { _ctl.AntiDust(); });
            dust.Margin = new Thickness(0, 0, 0, 8);
            dust.HorizontalAlignment = HorizontalAlignment.Stretch;
            st.Children.Add(dust);

            var refresh = GhostButton(Loc.T("Refresh now"), delegate { Tick(); });
            refresh.Margin = new Thickness(0, 0, 0, 0);
            refresh.HorizontalAlignment = HorizontalAlignment.Stretch;
            st.Children.Add(refresh);

            // The full path wrapped mid-word and crowded the buttons; %APPDATA% form fits one
            // line and is what a user would actually type into Explorer.
            var pnote = new TextBlock
            {
                Text = Loc.T("profile  ") + ShortPath(Profile.FilePath),
                FontSize = 9.5, Foreground = Theme.TxtFaintBrush,
                Margin = new Thickness(2, 12, 0, 0)
            };
            st.Children.Add(pnote);

            body.Child = st;
            sp.Children.Add(body);
            return sp;
        }

        TextBlock Micro(string s, bool right = false)
        {
            return new TextBlock
            {
                Text = Loc.T(s), FontSize = 9, Foreground = Theme.TxtFaintBrush,
                HorizontalAlignment = right ? HorizontalAlignment.Right : HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };
        }

        // ---------------- button factory ----------------
        // One height for every action button so the rows line up, and real horizontal padding
        // so the label never touches the border. (The earlier version hard-coded the
        // ContentPresenter margin to 0 inside the template, which silently discarded Padding
        // and left the text jammed against the edge.)

        const double BtnH = 30;

        Button MakeButton(string label, Action act, Brush bg, Brush border, Brush fg,
                          double minWidth, bool bold)
        {
            var b = new Button
            {
                Content = label,
                Height = BtnH,
                MinWidth = minWidth,
                Padding = new Thickness(16, 0, 16, 0),
                Margin = new Thickness(0, 0, 8, 0),
                Background = bg,
                BorderBrush = border,
                BorderThickness = new Thickness(1),
                Foreground = fg,
                FontSize = 11,
                FontWeight = bold ? FontWeights.SemiBold : FontWeights.Normal,
                Cursor = Cursors.Hand,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                // kill the default WPF chrome so our colours are the only thing visible
                Style = FlatButtonStyle()
            };
            b.Tag = new Skin { Bg = bg, Border = border };
            b.MouseEnter += delegate
            {
                var k = SkinOf(b);
                b.Background = Lighten(k.Bg, 0.18);
                b.BorderBrush = Lighten(k.Border, 0.30);
            };
            b.MouseLeave += delegate
            {
                var k = SkinOf(b);
                b.Background = k.Bg;
                b.BorderBrush = k.Border;
            };
            b.Click += delegate { act(); };
            return b;
        }

        /// <summary>The colours a hand-made button should return to after a hover. Held on the
        /// button instead of captured in the mouse handlers, because the preset chips get
        /// recoloured whenever the selected mode changes — a captured base would paint them back
        /// to unselected on the first mouse-over.</summary>
        class Skin
        {
            public Brush Bg;
            public Brush Border;
        }

        static Skin SkinOf(Button b)
        {
            var s = b.Tag as Skin;
            if (s == null)
            {
                s = new Skin { Bg = b.Background, Border = b.BorderBrush };
                b.Tag = s;
            }
            return s;
        }

        static void SetSkin(Button b, Brush bg, Brush border)
        {
            var s = SkinOf(b);
            s.Bg = bg;
            s.Border = border;
            b.Background = bg;
            b.BorderBrush = border;
        }

        /// <summary>Removes the Aero/Classic button template so Background/BorderBrush apply
        /// directly and the hover state is ours, not the system's blue glow.</summary>
        static Style FlatButtonStyle()
        {
            var s = new Style(typeof(Button));
            var t = new ControlTemplate(typeof(Button));
            var bd = new FrameworkElementFactory(typeof(Border));
            bd.SetValue(Border.CornerRadiusProperty, new CornerRadius(6));
            // Same black-fringe issue as the mode pills: 1 px border without pixel
            // snapping under software rendering. SnapsToDevicePixels is inheritable,
            // but the template Border is the element that actually draws the edge.
            bd.SetValue(Border.SnapsToDevicePixelsProperty, true);
            bd.SetBinding(Border.BackgroundProperty,
                new Binding("Background") { RelativeSource = RelativeSource.TemplatedParent });
            bd.SetBinding(Border.BorderBrushProperty,
                new Binding("BorderBrush") { RelativeSource = RelativeSource.TemplatedParent });
            bd.SetBinding(Border.BorderThicknessProperty,
                new Binding("BorderThickness") { RelativeSource = RelativeSource.TemplatedParent });
            var cp = new FrameworkElementFactory(typeof(ContentPresenter));
            cp.SetValue(ContentPresenter.HorizontalAlignmentProperty, HorizontalAlignment.Center);
            cp.SetValue(ContentPresenter.VerticalAlignmentProperty, VerticalAlignment.Center);
            // Bind Padding through to the ContentPresenter. A custom ControlTemplate does NOT
            // inherit Button.Padding automatically — with a hard-coded Margin of 0 the label
            // sat flush against the border and the button looked cramped.
            cp.SetBinding(ContentPresenter.MarginProperty,
                new Binding("Padding") { RelativeSource = RelativeSource.TemplatedParent });
            bd.AppendChild(cp);
            t.VisualTree = bd;
            s.Setters.Add(new Setter(Control.TemplateProperty, t));
            s.Seal();
            return s;
        }

        static Brush Lighten(Brush src, double amount)
        {
            var sc = src as SolidColorBrush;
            if (sc == null) return src;
            var c = sc.Color;
            // NOTE: no local function here — csc 4.0 (C# 5) cannot parse them.
            return Theme.B(Color.FromArgb(c.A,
                LightenByte(c.R, amount), LightenByte(c.G, amount), LightenByte(c.B, amount)));
        }

        static byte LightenByte(byte v, double amount)
        {
            return (byte)Math.Min(255, v + (255 - v) * amount);
        }

        Button GhostButton(string label, Action act, bool accent = false)
        {
            return MakeButton(label, act,
                accent ? Theme.B(Theme.AccentSoft) : Theme.B(Theme.Btn),
                accent ? Theme.B(Theme.AccentSoftLine) : Theme.B(Theme.BtnLine),
                accent ? Theme.AccentBrush : Theme.TxtDimBrush, 0, false);
        }

        FrameworkElement SectionHeader(string title, string sub)
        {
            var sp = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(2, 0, 0, 0) };
            sp.Children.Add(new TextBlock
            {
                Text = title, FontFamily = new FontFamily(Theme.FontDisplay),
                FontSize = 13, Foreground = Theme.TxtBrush, VerticalAlignment = VerticalAlignment.Center
            });
            if (!string.IsNullOrEmpty(sub))
                sp.Children.Add(new TextBlock
                {
                    Text = sub, FontSize = 10, Foreground = Theme.TxtFaintBrush,
                    Margin = new Thickness(10, 0, 0, 0), VerticalAlignment = VerticalAlignment.Center
                });
            return sp;
        }

        FrameworkElement StatusBar()
        {
            var g = new Grid { Background = Theme.B(Theme.Bar) };
            g.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            g.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            g.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            _dllInfo = new TextBlock
            {
                Text = Loc.T("loading…"), FontSize = 10, Foreground = Theme.TxtFaintBrush,
                VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(14, 0, 0, 0)
            };
            _status = new TextBlock
            {
                Text = Loc.T(""), FontSize = 10, Foreground = Theme.TxtDimBrush,
                VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new Thickness(0, 0, 14, 0)
            };
            g.Children.Add(_dllInfo);
            g.Children.Add(_status);
            Grid.SetColumn(_status, 2);
            return g;
        }

        // ---------------- logic ----------------

        void Start()
        {
            if (_started) return;   // SilentStart() ran at boot; Loaded fires on first Show
            _started = true;
            string path = _ctl.Init();
            if (path == null)
            {
                _dllInfo.Text = Loc.T("InsydeDCHU.dll not found — install the CLEVO Control Center");
                _status.Text = Loc.T("driver unavailable");
                return;
            }
            string tag = Native.HasEx ? "app build" : "service build";
            _dllInfo.Text = string.Format("{0} · {1} {2} · {3} · {4}",
                Shorten(path), _ctl.EcChip, _ctl.EcVersion, _ctl.Board, tag);

            SelectCurveFan(0);
            _timer.Interval = TimeSpan.FromMilliseconds(900);
            _timer.Tick += delegate { Tick(); };
            _timer.Start();
            Tick();

            // Re-assert the saved profile, but only after the EC has settled. Right after boot
            // (and after any heavy DCHU traffic) cmd 12 answers with a plausible-looking stub
            // body for several seconds; applying against that would compare against fiction.
            var boot = new DispatcherTimer { Interval = TimeSpan.FromSeconds(3) };
            boot.Tick += delegate
            {
                boot.Stop();
                RestoreProfile();
            };
            boot.Start();
            Trace.W("start ok");
        }

        /// <summary>
        /// Load the saved profile, and if the user asked for it, put the EC back into that
        /// state. This is what makes a custom curve permanent: the EC's own mode can come back
        /// as Automatic after a boot, and in Automatic the uploaded table is simply ignored —
        /// so persistence needs somebody to re-assert both the curve and the mode.
        /// </summary>
        void RestoreProfile()
        {
            _prof = Profile.Load();
            if (_prof == null)
            {
                // First run: adopt whatever is live so the user's existing setup becomes the
                // profile instead of us inventing one and overwriting their work.
                var s0 = _ctl.Read();
                if (s0.Error != null) return;
                _prof = Profile.Capture(s0);
                _prof.Save();
                Trace.W("profile created from live EC state");
                return;
            }

            if (!_prof.AutoApply) { Trace.W("profile present, auto-apply off"); return; }

            var s = _ctl.Read();
            if (s.Error != null || s.Stale) { RestoreLater(); return; }
            if (_prof.Matches(s)) { Trace.W("profile already applied, nothing to do"); return; }

            int[] all = _prof.DifferingFans(s);
            // Skip channels this chassis does not physically have. Their EC slots still hold
            // leftovers, so a naive comparison reports them as differing and every boot would
            // issue a pointless write — and every extra DCHU write widens the window where the
            // EC answers with its stub body.
            var keep = new System.Collections.Generic.List<int>();
            for (int i = 0; i < all.Length; i++)
                if (all[i] <= 2 && _ctl.FanSeen(all[i])) keep.Add(all[i]);
            int[] diff = keep.ToArray();
            if (diff.Length == 0 && (int)s.Mode == _prof.Mode && s.Offset == _prof.Offset)
            {
                Trace.W("profile matches all present fans, nothing to do");
                return;
            }
            Trace.W("re-applying profile: fans [" + string.Join(",", Array.ConvertAll(diff, delegate(int i) { return i.ToString(); })) + "] mode=" + _prof.Mode);
            ApplyProfileTo(_prof, diff);
        }

        void RestoreLater()
        {
            var t = new DispatcherTimer { Interval = TimeSpan.FromSeconds(4) };
            t.Tick += delegate { t.Stop(); RestoreProfile(); };
            t.Start();
        }

        void ApplyProfileTo(Profile p, int[] fans)
        {
            // Mode first: a curve uploaded while the EC is in Automatic has no effect.
            if ((int)_ctl.ReadMode() != p.Mode) _ctl.SetMode((FanMode)p.Mode);
            for (int i = 0; i < fans.Length; i++)
            {
                int f = fans[i];
                if (f < 0 || f > 2) continue;          // cmd 14 only covers three channels
                _ctl.ApplyCurve(f, p.Fans[f].ToPoints());
            }
            if (_ctl.ReadOffset() != p.Offset) _ctl.SetOffset(p.Offset);
        }

        /// <summary>
        /// Persist what the user ASKED FOR, never what the EC happens to report. SaveProfile()
        /// re-reads the EC, and the EC's cmd13 mirror drifts (a 33 % point came back as 41 % and
        /// got absorbed into the profile) — so a read-back save silently rewrites the user's
        /// saved curve with firmware noise. These three write the intent instead.
        /// </summary>
        void SaveCurveIntent(int fan, FanPoint[] pts)
        {
            if (_prof == null) _prof = new Profile();
            _prof.Fans[fan].Set(pts);
            _prof.Mode = (int)FanMode.Custom;      // ApplyCurve switches to Custom by design
            if (!_prof.Save()) Trace.W("profile save FAILED");
        }

        void SaveModeIntent(FanMode m)
        {
            if (_prof == null) _prof = new Profile();
            _prof.Mode = (int)m;
            if (!_prof.Save()) Trace.W("profile save FAILED");
        }

        void SaveOffsetIntent(int pct)
        {
            if (_prof == null) _prof = new Profile();
            _prof.Offset = pct;
            if (!_prof.Save()) Trace.W("profile save FAILED");
        }
        /// <summary>Refresh the stored profile from the current EC state. Called after every
        /// deliberate user change so "what the user last chose" is always what comes back.</summary>
        void SaveProfile()
        {
            var s = _ctl.Read();
            if (s.Error != null || s.Stale) return;
            if (_prof == null) _prof = new Profile();
            _prof.Mode = (int)s.Mode;
            _prof.Offset = s.Offset;
            for (int i = 0; i < 4; i++) _prof.Fans[i].Set(s.Curves[i]);
            bool keep = _autoApply != null ? _autoApply.IsChecked == true : _prof.AutoApply;
            _prof.AutoApply = keep;
            if (!_prof.Save()) Trace.W("profile save FAILED");
        }

        /// <summary>Show an AppData path in the %APPDATA%\... form Explorer accepts, so it fits
        /// one line instead of wrapping mid-word.</summary>
        static string ShortPath(string p)
        {
            if (string.IsNullOrEmpty(p)) return "(unavailable)";
            try
            {
                string ad = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                if (!string.IsNullOrEmpty(ad) && p.StartsWith(ad, StringComparison.OrdinalIgnoreCase))
                    return "%APPDATA%" + p.Substring(ad.Length);
            }
            catch { }
            return p;
        }

        static string Shorten(string p)
        {
            try { return Path.GetFileName(p); } catch { return p; }
        }

        void UpdatePolling()
        {
            if (_timer == null) return;
            bool shouldRun = IsVisible && WindowState != WindowState.Minimized;
            if (!shouldRun)
            {
                if (_timer.IsEnabled) _timer.Stop();
                return;
            }
            // Focused: snappy. Unfocused: 2.5x slower, which is still well inside what a
            // thermal display needs but cuts the steady-state work noticeably.
            var want = TimeSpan.FromMilliseconds(_idlePoll ? 2250 : 900);
            if (_timer.Interval != want) _timer.Interval = want;
            if (!_timer.IsEnabled)
            {
                _timer.Start();
                Tick();                       // catch up immediately, do not wait out the interval
            }
        }
        void Tick()
        {
            var s = _ctl.Read();
            if (s.Error != null) { _status.Text = s.Error; return; }

            int cpuRpm = s.Fans[0].Rpm, gpuRpm = s.Fans[1].Rpm;
            _gCpu.Value = cpuRpm; _gCpu.Max = _maxRpm;
            _gGpu.Value = gpuRpm; _gGpu.Max = _maxRpm;
            _gCpu.Caption = "RPM"; _gGpu.Caption = "RPM";
            _gCpu.Sub = cpuRpm > 0 ? Loc.F("{0} % duty", s.Fans[0].DutyPct) : Loc.T("not present");
            _gGpu.Sub = gpuRpm > 0 ? Loc.F("{0} % duty", s.Fans[1].DutyPct) : Loc.T("not present");
            _gCpu.Tertiary = s.Fans[0].TempC + " °C";
            _gGpu.Tertiary = s.Fans[1].TempC + " °C";
            _gCpu.ArcColor = Theme.DutyColor(s.Fans[0].DutyPct);
            _gGpu.ArcColor = Theme.DutyColor(s.Fans[1].DutyPct);
            _gCpu.Active = _gGpu.Active = true;

            int mx = Math.Max(cpuRpm, gpuRpm);
            if (mx > _maxRpm * 0.92) _maxRpm = (int)(Math.Ceiling(mx / 500.0) * 500);

            _suppressModeEvent = true;
            int mi = Array.IndexOf(ModeList, s.Mode);
            if (mi >= 0) _modes.Select(mi);
            _suppressModeEvent = false;

            _offset.Value = s.Offset;
            _offsetVal.Text = s.Offset + " %";

            for (int i = 0; i < 4; i++)
            {
                var f = s.Fans[i];
                _sensorRpm[i].Text = f.Present ? f.Rpm.ToString("N0", CultureInfo.InvariantCulture) : "--";
                _sensorDuty[i].Text = f.Present ? f.DutyPct + " %" : "--";
                _sensorTemp[i].Text = f.Present ? f.TempC + " °C" : "--";
                _sensorTemp[i].Foreground = f.Present ? Theme.B(Theme.TempColor(f.TempC)) : Theme.TxtFaintBrush;
                _sensorDuty[i].Foreground = f.Present ? Theme.B(Theme.DutyColor(f.DutyPct)) : Theme.TxtFaintBrush;
            }

            // Reload the editor from the EC only when the user is neither mid-drag nor has
            // unapplied edits. Two separate guards are needed: IsDragging alone protects the
            // drag itself, but the moment the button is released the next poll would see
            // "EC != editor" and wipe the edit before the user ever reaches Apply.
            if (!_curve.IsDragging && !_curveDirty)
            {
                var now = s.Curves[_curveFan];
                if (!Same(now, _curve.Snapshot())) _curve.Load(now);
            }

            UpdatePresetSelection();
            ApplyFanVisibility();

            // The interval used to be hard-coded into this string, so it kept claiming 0.9 s
            // even after focus-throttling dropped it to 2.25 s. Report what is actually set.
            _status.Text = Loc.F("updated {0:HH:mm:ss} · poll {1:N1} s · {2}",
                s.At, _timer.Interval.TotalSeconds,
                Theme.IsLight ? Loc.T("light") : Loc.T("dark"));
        }

        static bool Same(FanPoint[] a, FanPoint[] b)
        {
            if (a == null || b == null || a.Length != 4 || b.Length != 4) return false;
            for (int i = 0; i < 4; i++)
                if (a[i].Temp != b[i].Temp || a[i].Duty != b[i].Duty) return false;
            return true;
        }

        void OnModePicked(int idx)
        {
            if (_suppressModeEvent) return;
            _ctl.SetMode(ModeList[idx]);
            SaveModeIntent(ModeList[idx]);
        }

        void SelectCurveFan(int idx)
        {
            _curveFan = idx;
            _curveDirty = false;   // switching tabs re-reads from the EC on purpose
            _presetSel = -1;       // a remembered chip belongs to the fan it was chosen on
            for (int i = 0; i < _curveTabs.Count; i++)
            {
                bool on = i == idx;
                _curveTabs[i].Background = on ? Theme.B(Theme.TabSel) : Theme.B(Theme.Chip);
                ((TextBlock)_curveTabs[i].Child).Foreground = on ? Theme.AccentBrush : Theme.TxtDimBrush;
            }
            _curveFanLabel.Text = new[] { Loc.T("CPU Fan"), Loc.T("GPU Fan"), Loc.T("GPU Fan 2"), Loc.T("Case Fan") }[idx];
            _curve.Load(_ctl.ReadCurves()[idx]);
            MarkDirty();   // the hint quotes the new fan's temperature window
        }

        /// <summary>
        /// Show a curve tab only for fans this chassis actually reports. The EC exposes four
        /// channels on every Clevo design this binary ships on; the DR521 populates two, so
        /// "GPU Fan 2" and "Case Fan" are empty slots rather than broken fans.
        /// </summary>
        void ApplyFanVisibility()
        {
            for (int i = 0; i < _curveTabs.Count; i++)
            {
                bool show = _showAllFans || _ctl.FanSeen(i);
                _curveTabs[i].Visibility = show ? Visibility.Visible : Visibility.Collapsed;
            }
            if (_fanSlots != null)
            {
                _fanSlots.Text = Loc.T(_showAllFans ? "hide empty EC slots" : "show all EC slots");
                _fanSlots.Foreground = _showAllFans ? Theme.AccentBrush : Theme.TxtFaintBrush;
            }
            // never leave the editor pointing at a tab that just got hidden
            if (!_showAllFans && _curveFan != 0 && !_ctl.FanSeen(_curveFan)) SelectCurveFan(0);
        }

        void ResetPreset(int[][] table)
        {
            // Fills the editor only — stays dirty until the user presses Save, otherwise the
            // next poll would see "EC != editor" and silently revert the preset.
            _curve.ResetTo(table[_curveFan]);
            _curveDirty = true;
            MarkDirty();
        }

        /// <summary>One chip in the preset row. A null table means "my own saved curve": the
        /// numbers are not baked into the binary, they come from the profile, so the 用户 chip
        /// follows whatever the user last pressed Save curve to fans with.</summary>
        void AddPreset(Panel host, string label, int[][] table)
        {
            int idx = _presetBtns.Count;
            var b = MiniButton(label, delegate
            {
                FanPoint[] pts = PresetPoints(table);
                if (pts == null)
                {
                    _status.Text = Loc.T("no saved curve yet");
                    return;
                }
                _curve.Load(pts);
                _presetSel = idx;
                _curveDirty = true;    // nothing reaches the EC until Save
                MarkDirty();
            });
            b.ToolTip = Loc.T(table == null
                ? "click to load your saved curve; Save curve to fans stores the current one here"
                : label);
            _presetBtns.Add(b);
            _presetTables.Add(table);
            host.Children.Add(b);
        }

        /// <summary>The curve a chip stands for on the fan currently being edited, or null when
        /// there is nothing to load (the 用户 chip before anything has ever been saved).</summary>
        FanPoint[] PresetPoints(int[][] table)
        {
            if (table != null)
            {
                int[] flat = table[_curveFan];
                if (flat == null || flat.Length != 8) return null;
                var r = new FanPoint[4];
                for (int i = 0; i < 4; i++) r[i] = new FanPoint(flat[i * 2], flat[i * 2 + 1]);
                return r;
            }
            if (_prof == null) _prof = Profile.Load();
            if (_prof == null) return null;
            return _prof.Fans[_curveFan].ToPoints();
        }

        /// <summary>
        /// Lights the chip whose curve the editor is actually showing. Derived from the numbers
        /// rather than remembered from the last click, because the poll repopulates the editor
        /// from the EC and a remembered highlight would outlive its own meaning. The one piece of
        /// memory kept: when nothing matches any more — the EC's adaptive logic nudged a duty a
        /// few percent away from what was uploaded — the chip the user last chose or saved stays
        /// lit, because that is still what they asked for.
        /// </summary>
        void UpdatePresetSelection()
        {
            if (_presetBtns.Count == 0) return;
            FanPoint[] cur = _curve.Snapshot();
            int hit = -1;
            for (int i = 0; i < _presetBtns.Count; i++)
            {
                FanPoint[] p = PresetPoints(_presetTables[i]);
                if (p != null && Near(p, cur)) { hit = i; break; }
            }
            if (hit < 0 && _presetSel >= 0 && _presetSel < _presetBtns.Count) hit = _presetSel;
            for (int i = 0; i < _presetBtns.Count; i++) StyleChip(_presetBtns[i], i == hit);
        }

        /// <summary>Preset comparison with a 2 % duty slack: the EC round-trips duty through a
        /// 0..255 byte, so an exact match would flicker between two polls.</summary>
        static bool Near(FanPoint[] a, FanPoint[] b)
        {
            if (a == null || b == null || a.Length != 4 || b.Length != 4) return false;
            for (int i = 0; i < 4; i++)
                if (a[i].Temp != b[i].Temp || Math.Abs(a[i].Duty - b[i].Duty) > 2) return false;
            return true;
        }

        void StyleChip(Button b, bool on)
        {
            SetSkin(b, on ? Theme.AccentGradient() : (Brush)Theme.B(Theme.Chip),
                      on ? Theme.B(Theme.SelLine) : Theme.B(Theme.Line));
            b.Foreground = on ? Theme.B(Theme.OnAccent) : Theme.TxtDimBrush;
            b.FontWeight = on ? FontWeights.SemiBold : FontWeights.Normal;
        }

        void StageCurve()
        {
            bool ok = _ctl.StageCurve(_curveFan, _curve.Snapshot());
            _status.Text = ok
                ? "curve staged to settings store (persists across reboot)"
                : "curve staging failed";
        }

        void ApplyCurve()
        {
            FanPoint[] pts = _curve.Snapshot();
            bool ok = _ctl.ApplyCurve(_curveFan, pts);
            if (ok)
            {
                SaveCurveIntent(_curveFan, pts);
                // This fan's curve now IS the user curve, so pin the 用户 chip as the selected
                // one: without that the highlight would drop the moment the EC's adaptive logic
                // nudges the uploaded duty a percent or two off what the editor still shows.
                _presetSel = _presetBtns.Count - 1;
                _curveDirty = false;
                MarkDirty();
            }
            _status.Text = ok
                ? Loc.T("curve saved to the EC and to your profile — it will be re-applied at startup")
                : Loc.T("curve upload failed");
        }
    }
}
