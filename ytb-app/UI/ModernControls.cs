using System.Windows.Forms;
using System.Drawing;

namespace ytb_app.UI
{
    public static class ModernControls
    {
        public static void StylePrimaryButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = AppColors.AccentBlue;
            btn.ForeColor = Color.White;
            btn.Cursor = Cursors.Hand;
            btn.Height = 40; // The Master Reference Height
            if (AppFonts.Bold != null) btn.Font = AppFonts.Bold;
            btn.Padding = new Padding(10, 5, 10, 5);
            // No fixed width, allow docking/TableLayout to handle it
        }

        public static void StyleSecondaryButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = AppColors.BorderSlate300; // More visible border
            btn.BackColor = Color.White;
            btn.ForeColor = AppColors.TextSlate700;
            btn.Cursor = Cursors.Hand;
            btn.Height = 40; // Matches Primary Reference
            if (AppFonts.Regular != null) btn.Font = AppFonts.Regular;
            btn.Padding = new Padding(8, 0, 8, 0);
        }


        public static void StyleCard(Panel panel)
        {
            panel.BackColor = AppColors.BgWhite;
            panel.BorderStyle = BorderStyle.None;
            panel.Padding = new Padding(AppSpacing.CardPadding);
        }

        public static void StyleInputCard(Panel panel)
        {
            panel.BackColor = AppColors.BgWhite;
            panel.BorderStyle = BorderStyle.None;
            panel.Padding = new Padding(15, 10, 15, 10); // Reduced padding for inputs
        }


        public static void StyleTextBox(TextBox txt)
        {
            txt.BorderStyle = BorderStyle.None; // Set to none for modern wrapper style
            txt.BackColor = Color.White;
            txt.ForeColor = AppColors.TextSlate700;
            txt.Font = AppFonts.Regular ?? txt.Font;
        }

        public static void StyleModernInputContainer(Panel pnl)
        {
            pnl.BackColor = Color.White;
            pnl.BorderStyle = BorderStyle.None; // We'll paint the border
        }

        public static void PaintModernBorder(Panel pnl, Graphics g, bool focused)
        {
            Color borderColor = focused ? AppColors.AccentBlue : AppColors.BorderSlate300;
            using var pen = new Pen(borderColor, 1);
            g.DrawRectangle(pen, 0, 0, pnl.Width - 1, pnl.Height - 1);
        }


        public static void StyleLabel(Label lbl, bool isHeader = false)
        {
            lbl.ForeColor = isHeader ? AppColors.TextSlate700 : AppColors.TextSlate500;
            if (isHeader)
            {
                if (AppFonts.Title != null) lbl.Font = AppFonts.Title;
            }
            else
            {
                if (AppFonts.Regular != null) lbl.Font = AppFonts.Regular;
            }
        }
    }
}
