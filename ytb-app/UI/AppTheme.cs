using System.Drawing.Text;

namespace ytb_app.UI
{
    public static class AppColors
    {
        // Tailwind Slate colors
        public static readonly Color BgSlate100 = ColorTranslator.FromHtml("#F1F5F9");
        public static readonly Color BgWhite = Color.White;
        public static readonly Color TextSlate700 = ColorTranslator.FromHtml("#334155");
        public static readonly Color TextSlate500 = ColorTranslator.FromHtml("#64748B");
        
        // Accent Colors (Emerald/Blue)
        public static readonly Color AccentBlue = ColorTranslator.FromHtml("#3B82F6");
        public static readonly Color AccentBlueHover = ColorTranslator.FromHtml("#2563EB");
        public static readonly Color AccentEmerald = ColorTranslator.FromHtml("#10B981");
        
        // Borders
        public static readonly Color BorderSlate200 = ColorTranslator.FromHtml("#E2E8F0");
        public static readonly Color BorderSlate300 = ColorTranslator.FromHtml("#CBD5E1");
    }

    public static class AppFonts
    {
        private static PrivateFontCollection? _fonts;
        public static Font? Regular;
        public static Font? Bold;
        public static Font? Title;

        public static void Initialize(string fontPath)
        {
            if (!File.Exists(fontPath)) return;

            _fonts = new PrivateFontCollection();
            _fonts.AddFontFile(fontPath);

            var family = _fonts.Families[0];
            Regular = new Font(family, 10f, FontStyle.Regular);
            Title = new Font(family, 14f, FontStyle.Bold);
            Bold = new Font(family, 10f, FontStyle.Bold);
        }
    }

    public static class AppSpacing
    {
        public static int P4 = 16;
        public static int P6 = 24;
        public static int M4 = 16;
        public static int CardPadding = 20;
    }
}
