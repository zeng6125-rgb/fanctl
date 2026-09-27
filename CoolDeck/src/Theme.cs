// CoolDeck — visual language. Two palettes (dark / light) resolved once at startup.
//
// WHY A RESTART IS REQUIRED TO SWITCH
// The whole UI is built in code and every control captures brush references at
// CONSTRUCTION time (MainWindow assigns Theme.*Brush directly onto properties; the
// hand-drawn controls read them inside OnRender). Mutating the brushes in place would
// therefore leave already-constructed controls painted with the old palette — a judgement
// pass put that risk at 0.68. So the palette is chosen once, before any window exists, and
// switching writes the preference and relaunches. This also keeps the diff small: no
// DynamicResource plumbing across three files.
//
// The palette is resolved in the static constructor, i.e. on first touch of any Theme
// member. That happens during MainWindow construction, which is well before any brush is
// handed to a control — so App.cs does not need to know about theming at all.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace CoolDeck
{
    public static class Theme
    {
        // ---------------- mutable palette slots ----------------
        // Not `readonly` on purpose: Apply() fills these in once, at startup.

        public static Color Bg0, Bg1, Card, CardHi, Line;
        public static Color Txt, TxtDim, TxtFaint;
        public static Color Accent, Accent2, Cool, Warm, Hot, Track;

        // Semantic tokens. These exist so MainWindow.cs / Controls.cs stopped hard-coding
        // 28 different hex literals, which would not have adapted to a light palette.
        public static Color Bar;          // title / status bar background
        public static Color Chip;         // pill + tab background
        public static Color ChipLine;     // pill border
        public static Color Btn;          // secondary button background
        public static Color BtnLine;      // secondary button border
        public static Color TabSel;       // selected tab background
        public static Color HoverTint;    // generic hover wash
        public static Color AccentSoft;   // accent-tinted button background
        public static Color AccentSoftLine;
        public static Color AccentLine;   // strong accent border (selected pill)
        public static Color OnAccent;     // text drawn ON an accent fill
        public static Color Danger;       // close-button glyph
        public static Color DangerHover;  // close-button hover background
        public static Color TxtInert;     // accepted-but-inert mode label
        public static Color Locked;       // non-draggable curve marker
        public static Color Dot;          // unselected curve point fill
        public static Color GridH, GridV; // chart gridlines

        public static SolidColorBrush
            Bg0Brush, Bg1Brush, CardBrush, CardHiBrush, LineBrush,
            TxtBrush, TxtDimBrush, TxtFaintBrush,
            AccentBrush, Accent2Brush, CoolBrush, WarmBrush, HotBrush, TrackBrush,
            BarBrush, ChipBrush, ChipLineBrush, BtnBrush, BtnLineBrush, TabSelBrush,
            HoverTintBrush, AccentSoftBrush, AccentSoftLineBrush, AccentLineBrush,
            OnAccentBrush, DangerBrush, DangerHoverBrush, TxtInertBrush, LockedBrush,
            DotBrush, GridHBrush, GridVBrush;

        public const string Font = "Segoe UI";
        public const string FontDisplay = "Segoe UI Semibold";

        public static bool IsLight { get; private set; }

        public static Color C(int r, int g, int b) { return Color.FromRgb((byte)r, (byte)g, (byte)b); }

                // Frozen brushes are immutable, so they can be shared. B() was called from inside
        // OnRender loops (11 tick pens per gauge, per frame) and allocated every time; the
        // cache turns that into a dictionary hit. Nothing can mutate these because they are
        // frozen, so sharing is safe.
        static readonly Dictionary<Color, SolidColorBrush> _brushCache =
            new Dictionary<Color, SolidColorBrush>();

        public static SolidColorBrush B(Color c)
        {
            SolidColorBrush b;
            if (_brushCache.TryGetValue(c, out b)) return b;
            b = new SolidColorBrush(c);
            b.Freeze();
            if (_brushCache.Count < 256) _brushCache[c] = b;
            return b;
        }

        static Theme()
        {
            Apply(LoadPreference());
        }

        /// <summary>Installs one of the two palettes. Called exactly once, from the static
        /// constructor, before any control exists.</summary>
        public static void Apply(bool light)
        {
            IsLight = light;

            Accent = C(0x22, 0xD3, 0xEE);
            Accent2 = C(0x81, 0x8C, 0xF8);

            if (light)
            {
                // Light palette. Mid-tone accents rather than the neon cyans: #22D3EE on
                // white is unreadable, so Accent keeps its hue but drops to a cyan-600.
                Bg0 = C(0xF4, 0xF6, 0xFA);
                Bg1 = C(0xED, 0xF0, 0xF6);
                Card = C(0xFF, 0xFF, 0xFF);
                CardHi = C(0xF2, 0xF5, 0xFA);
                Line = C(0xD8, 0xDE, 0xE9);
                Txt = C(0x10, 0x15, 0x1F);
                TxtDim = C(0x4A, 0x54, 0x68);
                TxtFaint = C(0x7C, 0x87, 0x9C);
                Cool = C(0x05, 0x96, 0x69);
                Warm = C(0xB4, 0x53, 0x09);
                Hot = C(0xDC, 0x26, 0x26);
                Track = C(0xE2, 0xE8, 0xF2);

                Bar = C(0xFA, 0xFB, 0xFD);
                Chip = C(0xEE, 0xF2, 0xF8);
                ChipLine = C(0xD3, 0xDA, 0xE6);
                Btn = C(0xF1, 0xF4, 0xF9);
                BtnLine = C(0xD8, 0xDE, 0xE9);
                TabSel = C(0xDC, 0xEE, 0xF5);
                HoverTint = C(0xE3, 0xE9, 0xF2);
                AccentSoft = C(0xDF, 0xF1, 0xF7);
                AccentSoftLine = C(0x7C, 0xC4, 0xDC);
                Danger = C(0xB9, 0x1C, 0x1C);
                DangerHover = C(0xF6, 0xDE, 0xDE);
                TxtInert = C(0xA8, 0xB2, 0xC2);
                Locked = C(0xB0, 0xBA, 0xC9);
                Dot = C(0xFF, 0xFF, 0xFF);
                GridH = C(0xE6, 0xEB, 0xF2);
                GridV = C(0xEC, 0xF0, 0xF6);
            }
            else
            {
                // Dark palette — the original values, unchanged.
                Bg0 = C(0x0B, 0x0D, 0x12);
                Bg1 = C(0x11, 0x14, 0x1C);
                Card = C(0x16, 0x1A, 0x24);
                CardHi = C(0x1D, 0x22, 0x2E);
                Line = C(0x27, 0x2D, 0x3B);
                Txt = C(0xE8, 0xEC, 0xF4);
                TxtDim = C(0x8A, 0x94, 0xA8);
                TxtFaint = C(0x56, 0x5F, 0x73);
                Cool = C(0x34, 0xD3, 0x99);
                Warm = C(0xFB, 0xBF, 0x24);
                Hot = C(0xF8, 0x71, 0x71);
                Track = C(0x22, 0x27, 0x33);

                Bar = C(0x0D, 0x10, 0x16);
                Chip = C(0x1B, 0x20, 0x2B);
                ChipLine = C(0x25, 0x2B, 0x38);
                Btn = C(0x1C, 0x21, 0x2C);
                BtnLine = C(0x28, 0x2E, 0x3C);
                TabSel = C(0x22, 0x3D, 0x47);
                HoverTint = C(0x22, 0x28, 0x35);
                AccentSoft = C(0x14, 0x2A, 0x33);
                AccentSoftLine = C(0x27, 0x5A, 0x68);
                Danger = C(0xC4, 0x6A, 0x6A);
                DangerHover = C(0x5A, 0x1E, 0x24);
                TxtInert = C(0x55, 0x5C, 0x6B);
                Locked = C(0x4A, 0x53, 0x66);
                Dot = C(0x0E, 0x12, 0x18);
                GridH = C(0x1E, 0x23, 0x2E);
                GridV = C(0x1B, 0x20, 0x2A);
            }

            // OnAccent must contrast with the accent FILL, which is bright in both palettes.
            OnAccent = C(0x05, 0x10, 0x14);
            AccentLine = light ? C(0x0E, 0x74, 0x90) : C(0x3A, 0xE0, 0xF0);

            Bg0Brush = B(Bg0); Bg1Brush = B(Bg1); CardBrush = B(Card);
            CardHiBrush = B(CardHi); LineBrush = B(Line);
            TxtBrush = B(Txt); TxtDimBrush = B(TxtDim); TxtFaintBrush = B(TxtFaint);
            AccentBrush = B(Accent); Accent2Brush = B(Accent2);
            CoolBrush = B(Cool); WarmBrush = B(Warm); HotBrush = B(Hot);
            TrackBrush = B(Track);
            BarBrush = B(Bar); ChipBrush = B(Chip); ChipLineBrush = B(ChipLine);
            BtnBrush = B(Btn); BtnLineBrush = B(BtnLine); TabSelBrush = B(TabSel);
            HoverTintBrush = B(HoverTint); AccentSoftBrush = B(AccentSoft);
            AccentSoftLineBrush = B(AccentSoftLine); AccentLineBrush = B(AccentLine);
            OnAccentBrush = B(OnAccent); DangerBrush = B(Danger);
            DangerHoverBrush = B(DangerHover); TxtInertBrush = B(TxtInert);
            LockedBrush = B(Locked); DotBrush = B(Dot);
            GridHBrush = B(GridH); GridVBrush = B(GridV);
        }

        public static Color TempColor(int c)
        {
            if (c < 60) return Cool;
            if (c < 80) return Warm;
            return Hot;
        }

        public static Color DutyColor(int pct)
        {
            if (pct < 50) return Cool;
            if (pct < 80) return Warm;
            return Hot;
        }

        /// <summary>Cached Typeface. Constructing one resolves font families and is far too
        /// expensive to do per label per frame — the curve chart alone drew ~13 labels, each
        /// with its own new Typeface.</summary>
        static readonly Dictionary<string, Typeface> _faces = new Dictionary<string, Typeface>();

        public static Typeface Face(string family)
        {
            Typeface f;
            if (_faces.TryGetValue(family, out f)) return f;
            // Typeface is not a Freezable, so no Freeze() here. The single-argument ctor
            // already means normal style/weight/stretch.
            f = new Typeface(family);
            _faces[family] = f;
            return f;
        }
        public static LinearGradientBrush AccentGradient()
        {
            var g = new LinearGradientBrush(Accent, Accent2, new Point(0, 0), new Point(1, 1));
            g.Freeze();
            return g;
        }

        /// <summary>Alpha-blended accent wash used by the curve area fill. Stronger in light
        /// mode because a 25%-alpha cyan over white barely registers.</summary>
        public static byte AreaFillAlpha { get { return IsLight ? (byte)0x2E : (byte)0x40; } }

        // ---------------- preference persistence ----------------
        // Kept out of the registry so the app stays side-by-side friendly, and out of the
        // exe folder because that may be read-only under Program Files.

        static string PrefPath
        {
            get
            {
                string dir = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CoolDeck");
                return Path.Combine(dir, "theme.txt");
            }
        }

        static bool LoadPreference()
        {
            try
            {
                string p = PrefPath;
                if (!File.Exists(p)) return false;           // dark is the default
                string s = File.ReadAllText(p).Trim().ToLowerInvariant();
                return s == "light";
            }
            catch { return false; }
        }

        /// <summary>Persists the choice. Returns false if the write failed.</summary>
        public static bool SavePreference(bool light)
        {
            try
            {
                string p = PrefPath;
                Directory.CreateDirectory(Path.GetDirectoryName(p));
                File.WriteAllText(p, light ? "light" : "dark");
                return true;
            }
            catch { return false; }
        }

        public static string PreferencePath { get { try { return PrefPath; } catch { return "unavailable"; } } }
    }
}
