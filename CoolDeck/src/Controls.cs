// CoolDeck — hand-drawn controls: arc gauge, pill selector, curve editor.
// All WPF, no XAML, no third-party dependencies.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CoolDeck
{
    /// <summary>Circular fan gauge: 270-degree sweep, gradient value arc, RPM in the middle.</summary>
    public class FanGauge : FrameworkElement
    {
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(FanGauge),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));
        public static readonly DependencyProperty MaxProperty =
            DependencyProperty.Register("Max", typeof(double), typeof(FanGauge),
                new FrameworkPropertyMetadata(6000.0, FrameworkPropertyMetadataOptions.AffectsRender));
        public static readonly DependencyProperty CaptionProperty =
            DependencyProperty.Register("Caption", typeof(string), typeof(FanGauge),
                new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.AffectsRender));
        public static readonly DependencyProperty SubProperty =
            DependencyProperty.Register("Sub", typeof(string), typeof(FanGauge),
                new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.AffectsRender));
        public static readonly DependencyProperty TertiaryProperty =
            DependencyProperty.Register("Tertiary", typeof(string), typeof(FanGauge),
                new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.AffectsRender));
        public static readonly DependencyProperty ArcColorProperty =
            DependencyProperty.Register("ArcColor", typeof(Color), typeof(FanGauge),
                new FrameworkPropertyMetadata(Theme.Accent, FrameworkPropertyMetadataOptions.AffectsRender));
        public static readonly DependencyProperty ActiveProperty =
            DependencyProperty.Register("Active", typeof(bool), typeof(FanGauge),
                new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsRender));

        public double Value { get { return (double)GetValue(ValueProperty); } set { SetValue(ValueProperty, value); } }
        public double Max { get { return (double)GetValue(MaxProperty); } set { SetValue(MaxProperty, value); } }
        public string Caption { get { return (string)GetValue(CaptionProperty); } set { SetValue(CaptionProperty, value); } }
        public string Sub { get { return (string)GetValue(SubProperty); } set { SetValue(SubProperty, value); } }
        public string Tertiary { get { return (string)GetValue(TertiaryProperty); } set { SetValue(TertiaryProperty, value); } }
        public Color ArcColor { get { return (Color)GetValue(ArcColorProperty); } set { SetValue(ArcColorProperty, value); } }
        public bool Active { get { return (bool)GetValue(ActiveProperty); } set { SetValue(ActiveProperty, value); } }

        const double StartDeg = 135.0;   // bottom-left
        const double SweepDeg = 270.0;   // clockwise to bottom-right

        // Pens are freezable and shareable, so build the fixed ones once. OnRender used to
        // allocate a new Pen (and a new Brush inside it) for the track, the value arc and all
        // eleven ticks — roughly 13 pens per gauge per frame.
        static Pen _trackPen, _trackPenIdle, _tickMajor, _tickMinor;

        static Pen TrackPen(bool active)
        {
            if (active)
            {
                if (_trackPen == null)
                {
                    _trackPen = new Pen(Theme.B(Theme.Track), 9);
                    _trackPen.StartLineCap = PenLineCap.Round;
                    _trackPen.EndLineCap = PenLineCap.Round;
                    _trackPen.Freeze();
                }
                return _trackPen;
            }
            if (_trackPenIdle == null)
            {
                _trackPenIdle = new Pen(Theme.B(Theme.Line), 9);
                _trackPenIdle.StartLineCap = PenLineCap.Round;
                _trackPenIdle.EndLineCap = PenLineCap.Round;
                _trackPenIdle.Freeze();
            }
            return _trackPenIdle;
        }

        static Pen TickMajorPen()
        {
            if (_tickMajor == null) _tickMajor = new Pen(Theme.B(Theme.TxtFaint), 1) { };
            return _tickMajor;
        }

        static Pen TickMinorPen()
        {
            if (_tickMinor == null) _tickMinor = new Pen(Theme.B(Theme.Line), 1) { };
            return _tickMinor;
        }

        protected override void OnRender(DrawingContext dc)
        {
            double w = ActualWidth, h = ActualHeight;
            if (w < 8 || h < 8) return;
            double r = Math.Min(w, h) / 2.0 - 6;
            var c = new Point(w / 2.0, h / 2.0);

            dc.DrawGeometry(null, TrackPen(Active), Arc(c, r, StartDeg, StartDeg + SweepDeg));

            double frac = Max <= 0 ? 0 : Math.Max(0, Math.Min(1, Value / Max));
            if (frac > 0.0005)
            {
                var lg = new LinearGradientBrush();
                lg.StartPoint = new Point(0, 0);
                lg.EndPoint = new Point(1, 1);
                lg.GradientStops.Add(new GradientStop(Active ? ArcColor : Theme.TxtFaint, 0.0));
                lg.GradientStops.Add(new GradientStop(Active ? Theme.Accent2 : Theme.TxtFaint, 1.0));
                lg.Freeze();
                var vp = new Pen(lg, 9) { StartLineCap = PenLineCap.Round, EndLineCap = PenLineCap.Round };
                dc.DrawGeometry(null, vp, Arc(c, r, StartDeg, StartDeg + SweepDeg * frac));
            }

            // ticks
            for (int i = 0; i <= 10; i++)
            {
                double a = (StartDeg + SweepDeg * i / 10.0) * Math.PI / 180.0;
                double r0 = r - 11, r1 = r - (i % 5 == 0 ? 17 : 14);
                dc.DrawLine(i % 5 == 0 ? TickMajorPen() : TickMinorPen(),
                    P(c, a, r0), P(c, a, r1));
            }

            string big = Value <= 0 ? "--" : ((long)Math.Round(Value)).ToString("N0", CultureInfo.InvariantCulture);
            var ft = Theme.Face(Theme.FontDisplay);
            DrawCentered(dc, big, ft, 30, Theme.TxtBrush, new Point(c.X, c.Y - 12));
            DrawCentered(dc, Caption, Theme.Face(Theme.Font), 10, Theme.TxtFaintBrush, new Point(c.X, c.Y + 12));

            if (!string.IsNullOrEmpty(Sub))
                DrawCentered(dc, Sub, Theme.Face(Theme.Font), 11, Theme.B(ArcColor), new Point(c.X, c.Y + 30));
            if (!string.IsNullOrEmpty(Tertiary))
                DrawCentered(dc, Tertiary, Theme.Face(Theme.Font), 10, Theme.TxtDimBrush, new Point(c.X, c.Y + 46));
        }

        static Point P(Point c, double a, double r)
        {
            return new Point(c.X + r * Math.Cos(a), c.Y + r * Math.Sin(a));
        }

        static Geometry Arc(Point c, double r, double a0, double a1)
        {
            var g = new StreamGeometry();
            using (var ctx = g.Open())
            {
                bool large = (a1 - a0) > 180.0;
                ctx.BeginFigure(P(c, a0 * Math.PI / 180.0, r), false, false);
                ctx.ArcTo(P(c, a1 * Math.PI / 180.0, r), new Size(r, r), 0, large,
                          SweepDirection.Clockwise, true, false);
            }
            g.Freeze();
            return g;
        }

        static void DrawCentered(DrawingContext dc, string s, Typeface tf, double size, Brush b, Point at)
        {
            if (string.IsNullOrEmpty(s)) return;
            var t = new FormattedText(s, CultureInfo.InvariantCulture, FlowDirection.LeftToRight,
                tf, size, b, 1.0);
            dc.DrawText(t, new Point(at.X - t.Width / 2.0, at.Y - t.Height / 2.0));
        }
    }

    /// <summary>Segmented pill selector used for the fan-mode row.</summary>
    public class PillRow : Panel
    {
        public event Action<int> Changed;
        public int Selected { get; private set; }
        readonly List<Border> _cells = new List<Border>();
        readonly List<TextBlock> _labels = new List<TextBlock>();
        readonly List<bool> _inert = new List<bool>();

        public void SetItems(IList<string> items)
        {
            SetItems(items, null);
        }

        /// <summary>items with an optional parallel "inert" mask: pills the firmware accepts
        /// but that do nothing measurable get dimmed so the user is not sent hunting.</summary>
        public void SetItems(IList<string> items, IList<bool> inert)
        {
            Children.Clear();
            _cells.Clear();
            _labels.Clear();
            _inert.Clear();
            for (int i = 0; i < items.Count; i++)
            {
                int idx = i;
                bool dim = inert != null && i < inert.Count && inert[i];
                _inert.Add(dim);
                var tb = new TextBlock
                {
                    Text = items[i],
                    FontFamily = new FontFamily(Theme.Font),
                    FontSize = 11.5,
                    Foreground = dim ? Theme.B(Theme.TxtInert) : Theme.TxtDimBrush,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    ToolTip = dim ? "accepted by the EC, but has no measurable effect on AC power" : null
                };
                var b = new Border
                {
                    CornerRadius = new CornerRadius(7),
                    Background = Theme.B(Theme.Chip),
                    BorderThickness = new Thickness(1),
                    BorderBrush = Theme.B(Theme.ChipLine),
                    Padding = new Thickness(4, 0, 4, 0),
                    Margin = new Thickness(0, 0, 6, 0),
                    Child = tb,
                    Cursor = Cursors.Hand
                };
                b.MouseLeftButtonUp += delegate { Select(idx); };
                _cells.Add(b);
                _labels.Add(tb);
                Children.Add(b);
            }
            Select(0);
        }

        public void Select(int i)
        {
            if (i < 0 || i >= _cells.Count) return;
            Selected = i;
            for (int k = 0; k < _cells.Count; k++)
            {
                bool on = k == i;
                _cells[k].Background = on ? Theme.AccentGradient() : (Brush)Theme.B(Theme.Chip);
                _cells[k].BorderBrush = on ? Theme.B(Theme.AccentLine) : Theme.B(Theme.ChipLine);
                _labels[k].Foreground = on ? Theme.B(Theme.OnAccent) :
                    (_inert[k] ? Theme.B(Theme.TxtInert) : Theme.TxtDimBrush);
                _labels[k].FontWeight = on ? FontWeights.SemiBold : FontWeights.Normal;
            }
            var h = Changed;
            if (h != null) h(i);
        }

        protected override Size MeasureOverride(Size avail)
        {
            double h = 0;
            foreach (UIElement e in Children) { e.Measure(avail); h = Math.Max(h, e.DesiredSize.Height); }
            return new Size(avail.Width, Math.Max(h, 26));
        }

        protected override Size ArrangeOverride(Size fin)
        {
            double x = 0;
            foreach (UIElement e in Children)
            {
                double w = e.DesiredSize.Width + 12;
                e.Arrange(new Rect(x, 0, w, fin.Height));
                x += w;
            }
            return fin;
        }
    }

    /// <summary>Temperature x duty curve editor with draggable points.</summary>
    public class CurveEditor : FrameworkElement
    {
        // PadB carries two rows of X labels (round ticks + the named domain bounds), so it
        // needs more room than the single row it was sized for.
        const double PadL = 38, PadR = 14, PadT = 12, PadB = 44;

        public FanPoint[] Points = new FanPoint[4];
        public int SelectedPoint = -1;
        public event Action Edited;

        static readonly FanPoint[] Fallback = {
            new FanPoint(40, 35), new FanPoint(60, 55), new FanPoint(80, 83), new FanPoint(100, 100)
        };

        public CurveEditor()
        {
            for (int i = 0; i < 4; i++) Points[i] = new FanPoint(Fallback[i].Temp, Fallback[i].Duty);
        }

        /// <summary>
        /// The editable temperature domain is NOT a constant. Each fan's curve is bounded by
        /// the two EC-owned endpoints: the CPU fan runs 40..100 C but the GPU fan runs
        /// 48..95 C. Plotting a fixed 40..100 axis on the GPU tab let the user drag points
        /// outside the range that fan can actually hold, and ApplyCurve then clamped them
        /// silently — the "I saved it and it changed" bug all over again.
        /// </summary>
        public int TempLo
        {
            get
            {
                int v = Points[0] == null ? 0 : Points[0].Temp;
                return v > 0 ? v : 40;
            }
        }

        public int TempHi
        {
            get
            {
                int v = Points[3] == null ? 0 : Points[3].Temp;
                return v > TempLo + 10 ? v : 100;
            }
        }

        /// <summary>Tick spacing chosen so the axis stays readable for narrow domains
        /// (GPU's 48..95 is only 47 degrees wide, where a 10-degree step gives 5 lines).</summary>
        int TickStep()
        {
            int span = TempHi - TempLo;
            if (span <= 30) return 5;
            if (span <= 70) return 10;
            return 20;
        }

        public void Load(FanPoint[] pts)
        {
            for (int i = 0; i < 4 && i < pts.Length; i++)
                Points[i] = new FanPoint(pts[i].Temp, pts[i].Duty);
            ClampAllToDomain();
            InvalidateVisual();
        }

        /// <summary>Keep the two draggable points inside the fan's own endpoints. A curve read
        /// back from the EC can have drifted (its adaptive logic rewrites the table), and
        /// without this the markers would render off the plot.</summary>
        void ClampAllToDomain()
        {
            int lo = TempLo, hi = TempHi;
            if (Points[1] != null) Points[1].Temp = Math.Max(lo + 1, Math.Min(hi - 1, Points[1].Temp));
            if (Points[2] != null) Points[2].Temp = Math.Max(lo + 1, Math.Min(hi - 1, Points[2].Temp));
            if (Points[1] != null && Points[2] != null && Points[2].Temp <= Points[1].Temp)
                Points[2].Temp = Math.Min(hi - 1, Points[1].Temp + 1);
        }

        public FanPoint[] Snapshot()
        {
            var r = new FanPoint[4];
            for (int i = 0; i < 4; i++) r[i] = new FanPoint(Points[i].Temp, Points[i].Duty);
            return r;
        }

        public void ResetTo(params int[] flat)
        {
            // flat = t0,d0,t1,d1,t2,d2,t3,d3
            for (int i = 0; i < 4; i++) Points[i] = new FanPoint(flat[i * 2], flat[i * 2 + 1]);
            ClampAllToDomain();
            InvalidateVisual();
        }

        protected override void OnRender(DrawingContext dc)
        {
            double w = ActualWidth, h = ActualHeight;
            if (w < 40 || h < 40) return;
            double pw = w - PadL - PadR, ph = h - PadT - PadB;

            var plot = new RectangleGeometry(new Rect(PadL, PadT, pw, ph));
            dc.PushClip(plot);

            // grid lines only — the axis labels are drawn AFTER Pop() below. They used to be
            // emitted here and were silently clipped away: the Y labels sit left of PadL and
            // the X labels below PadT+ph, both outside this clip rectangle.
            for (int t = 0; t <= 100; t += 20)
            {
                double y = Y(t);
                dc.DrawLine(new Pen(Theme.B(Theme.GridH), 1), new Point(PadL, y), new Point(PadL + pw, y));
            }
            for (int t = FirstTick(); t <= TempHi; t += TickStep())
            {
                double x = X(t);
                dc.DrawLine(new Pen(Theme.B(Theme.GridV), 1), new Point(x, PadT), new Point(x, PadT + ph));
            }
            // The domain endpoints get their own stronger rule so the editable window is
            // visible: everything outside these two lines cannot be set for this fan.
            var bound = new Pen(Theme.B(Theme.Line), 1.4);
            dc.DrawLine(bound, new Point(X(TempLo), PadT), new Point(X(TempLo), PadT + ph));
            dc.DrawLine(bound, new Point(X(TempHi), PadT), new Point(X(TempHi), PadT + ph));

            // curve
            var pts = new Point[4];
            for (int i = 0; i < 4; i++) pts[i] = new Point(X(Points[i].Temp), Y(Points[i].Duty));
            var fig = new StreamGeometry();
            using (var ctx = fig.Open())
            {
                ctx.BeginFigure(pts[0], false, false);
                for (int i = 1; i < 4; i++) ctx.LineTo(pts[i], true, false);
            }
            fig.Freeze();
            var grad = Theme.AccentGradient();
            dc.DrawGeometry(null, new Pen(grad, 2.2) { StartLineCap = PenLineCap.Round, EndLineCap = PenLineCap.Round }, fig);

            // area fill
            var fillGeo = new StreamGeometry();
            using (var ctx = fillGeo.Open())
            {
                ctx.BeginFigure(pts[0], true, true);
                for (int i = 1; i < 4; i++) ctx.LineTo(pts[i], true, false);
                ctx.LineTo(new Point(pts[3].X, PadT + ph), true, false);
                ctx.LineTo(new Point(pts[0].X, PadT + ph), true, false);
            }
            fillGeo.Freeze();
            byte fa = Theme.AreaFillAlpha;
            var fg = new LinearGradientBrush(Color.FromArgb(fa, Theme.Accent.R, Theme.Accent.G, Theme.Accent.B),
                                             Color.FromArgb(0x00, Theme.Accent.R, Theme.Accent.G, Theme.Accent.B),
                                             new Point(0, 0), new Point(0, 1));
            fg.Freeze();
            dc.DrawGeometry(fg, null, fillGeo);

            dc.Pop();

            // axis labels — outside the plot clip so they are actually visible
            for (int t = 0; t <= 100; t += 20)
            {
                double y = Y(t);
                DrawTxt(dc, t.ToString(CultureInfo.InvariantCulture), 9, Theme.TxtFaintBrush,
                        new Point(PadL - 5, y), 1);
            }
            // One label row, not two. The previous version drew the ticks AND then repeated the
            // two bounds on a second line, so on the CPU fan (40..100, step 10) "40" and "100"
            // appeared twice and collided with the axis caption.
            double ly = PadT + ph + 4;
            for (int t = FirstTick(); t <= TempHi; t += TickStep())
            {
                DrawTxt(dc, t.ToString(CultureInfo.InvariantCulture), 9, Theme.TxtFaintBrush,
                        new Point(X(t), ly), 0);
            }
            // Name a bound only where no tick already says it, so 48..95 (GPU) still shows its
            // odd endpoints while 40..100 (CPU) does not duplicate them.
            int step = TickStep();
            if (TempLo % step != 0)
                DrawTxt(dc, TempLo.ToString(CultureInfo.InvariantCulture), 9, Theme.TxtDimBrush,
                        new Point(X(TempLo), ly), 0);
            if (TempHi % step != 0)
                DrawTxt(dc, TempHi.ToString(CultureInfo.InvariantCulture), 9, Theme.TxtDimBrush,
                        new Point(X(TempHi), ly), 0);
            DrawVertTxt(dc, Loc.T("Duty %"), 9, Theme.TxtFaintBrush, new Point(6, PadT + ph / 2));
            DrawTxt(dc, Loc.T("Temperature \u00b0C"), 9, Theme.TxtFaintBrush,
                    new Point(PadL + pw / 2, ly + 13), 0);

            // Draggable points only. The EC owns points 0 and 3 (its floor temperature and
            // 100 C / 100 %) and they are provably unwritable, so drawing handles for them
            // invited the user to drag something that could never be honoured. The polyline
            // still runs to those anchors — only the markers are gone.
            for (int i = 1; i <= 2; i++)
            {
                var p = pts[i];
                bool sel = i == SelectedPoint;
                var fill = sel ? Theme.B(Theme.Accent) : Theme.B(Theme.Dot);
                double rr = sel ? 6.5 : 5;
                dc.DrawEllipse(fill, new Pen(Theme.B(Theme.Accent), sel ? 2.4 : 1.6), p, rr, rr);
                dc.DrawEllipse(null, new Pen(Theme.B(Theme.Txt), 1), p, 1.6, 1.6);
            }
        }

        double X(int temp)
        {
            int lo = TempLo, hi = TempHi;
            return PadL + (temp - lo) / (double)(hi - lo) * (ActualWidth - PadL - PadR);
        }
        double Y(int duty) { return PadT + (1.0 - duty / 100.0) * (ActualHeight - PadT - PadB); }

        /// <summary>Smallest on-step tick at or above the domain floor, so labels stay round
        /// numbers instead of inheriting the fan's odd 48 C start.</summary>
        int FirstTick()
        {
            int step = TickStep();
            int lo = TempLo;
            return ((lo + step - 1) / step) * step;
        }

        /// <summary>Inverse of X — pixel back to degrees, for dragging.</summary>
        int TempAt(double px)
        {
            int lo = TempLo, hi = TempHi;
            double f = (px - PadL) / (ActualWidth - PadL - PadR);
            return (int)Math.Round(lo + f * (hi - lo));
        }

        static void DrawTxt(DrawingContext dc, string s, double size, Brush b, Point at, bool rightAlign)
        {
            DrawTxt(dc, s, size, b, at, rightAlign ? 1 : 0);
        }

        /// <summary>align: 0 = centred on at.X, 1 = right-aligned to at.X, 2 = left-aligned from at.X</summary>
        static void DrawTxt(DrawingContext dc, string s, double size, Brush b, Point at, int align)
        {
            var t = new FormattedText(s, CultureInfo.InvariantCulture, FlowDirection.LeftToRight,
                Theme.Face(Theme.Font), size, b, 1.0);
            double x = align == 1 ? at.X - t.Width : (align == 2 ? at.X : at.X - t.Width / 2.0);
            dc.DrawText(t, new Point(x, at.Y - t.Height / 2.0));
        }

        /// <summary>Rotated text for the Y axis caption.</summary>
        static void DrawVertTxt(DrawingContext dc, string s, double size, Brush b, Point centre)
        {
            var t = new FormattedText(s, CultureInfo.InvariantCulture, FlowDirection.LeftToRight,
                Theme.Face(Theme.Font), size, b, 1.0);
            dc.PushTransform(new RotateTransform(-90, centre.X, centre.Y));
            dc.DrawText(t, new Point(centre.X - t.Width / 2.0, centre.Y - t.Height / 2.0));
            dc.Pop();
        }

        int Hit(Point m)
        {
            // Only the two MIDDLE points are draggable. Points 0 and 3 are owned by the EC
            // (its floor temperature and 100C/100%), so letting the user drag them produced a
            // curve that silently did not match what they had drawn — the exact "I saved it
            // and it changed" confusion. They are now drawn as fixed markers instead.
            for (int i = 2; i >= 1; i--)
            {
                var p = new Point(X(Points[i].Temp), Y(Points[i].Duty));
                if ((m - p).Length <= 11) return i;
            }
            return -1;
        }

        public bool IsDragging { get { return SelectedPoint >= 0 && IsMouseCaptured; } }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            SelectedPoint = Hit(e.GetPosition(this));
            if (SelectedPoint >= 0) { CaptureMouse(); InvalidateVisual(); }
            base.OnMouseLeftButtonDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            Cursor = Hit(e.GetPosition(this)) >= 0 ? Cursors.SizeAll : Cursors.Arrow;
            if (SelectedPoint >= 0 && IsMouseCaptured)
            {
                Point m = e.GetPosition(this);
                // Dragging is bounded by THIS fan's endpoints, not a global 40..100. The EC
                // accepts t1 anywhere, but a point outside the fan's own 48..95 (GPU) window
                // would be clamped on upload and the saved curve would not match the drawing.
                int t = TempAt(m.X);
                int d = (int)Math.Round((1.0 - (m.Y - PadT) / (ActualHeight - PadT - PadB)) * 100.0);
                int dLo = TempLo + 1, dHi = TempHi - 1;
                t = Math.Max(dLo, Math.Min(dHi, t));
                d = Math.Max(0, Math.Min(100, d));
                int lo = SelectedPoint <= 1 ? dLo : Points[SelectedPoint - 1].Temp + 1;
                int hi = SelectedPoint >= 2 ? dHi : Points[SelectedPoint + 1].Temp - 1;
                t = Math.Max(lo, Math.Min(hi, t));
                Points[SelectedPoint].Temp = t;
                Points[SelectedPoint].Duty = d;
                InvalidateVisual();
                var h = Edited;
                if (h != null) h();
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (IsMouseCaptured) ReleaseMouseCapture();
            base.OnMouseLeftButtonUp(e);
        }
    }
}
