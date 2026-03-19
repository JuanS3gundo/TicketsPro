using System.Drawing;
using System.Windows.Forms;
namespace TicketPro.Temas
{
    public static class Theme
    {
        public static readonly Color BackgroundDark = Color.FromArgb(35, 35, 50);
        public static readonly Color PanelDark = Color.FromArgb(45, 45, 60);
        public static readonly Color Primary = Color.FromArgb(120, 80, 200);
        public static readonly Color Secondary = Color.FromArgb(85, 85, 100);
        public static readonly Color AccentGreen = Color.FromArgb(80, 200, 120);
        public static readonly Color Danger = Color.FromArgb(200, 80, 80);
        public static readonly Color TextLight = Color.WhiteSmoke;
        public static readonly Color TextMuted = Color.FromArgb(200, 200, 200);
        public static void Apply(Form form)
        {
            form.BackColor = BackgroundDark;
            form.ForeColor = TextLight;
        }
        public static void ApplyForm(Form form) => Apply(form);
        public static void MakeCard(Panel panel)
        {
            panel.BackColor = PanelDark;
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Padding = new Padding(8);
            foreach (Control ctrl in panel.Controls)
            {
                if (ctrl is Label lbl)
                    lbl.ForeColor = TextLight;
                else if (ctrl is TextBox txt)
                {
                    txt.BackColor = Color.FromArgb(55, 55, 70);
                    txt.ForeColor = TextLight;
                }
                else if (ctrl is ComboBox cmb)
                {
                    cmb.BackColor = Color.FromArgb(55, 55, 70);
                    cmb.ForeColor = TextLight;
                    cmb.FlatStyle = FlatStyle.Flat;
                }
                else if (ctrl is ListBox lst)
                {
                    lst.BackColor = Color.FromArgb(55, 55, 70);
                    lst.ForeColor = TextLight;
                }
            }
        }
        public static void ApplyButtonPrimary(Button btn) => ButtonPrimary(btn);
        public static void ApplyButtonSecondary(Button btn) => ButtonNeutral(btn);
        public static void ApplyButtonDanger(Button btn) => ButtonDanger(btn);
        public static void ButtonPrimary(Button btn)
        {
            StyleButton(btn, Primary, Color.White);
        }
        public static void ButtonNeutral(Button btn)
        {
            StyleButton(btn, Secondary, TextLight);
        }
        public static void ButtonDanger(Button btn)
        {
            StyleButton(btn, Danger, Color.White);
        }
        private static void StyleButton(Button btn, Color bg, Color fg)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = bg;
            btn.ForeColor = fg;
            btn.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            btn.MouseEnter += (s, e) => btn.BackColor = ControlPaint.Light(bg, 0.15f);
            btn.MouseLeave += (s, e) => btn.BackColor = bg;
        }
        public static void ButtonSecondary(Button b) => ButtonNeutral(b);
        public static void ButtonOrange(Button b) => StyleButton(b, Color.Orange, Color.Black);
        public static void SubText(Label l) => l.ForeColor = TextMuted;
        public static void MakeResponsiveFlow(FlowLayoutPanel flow)
        {
            flow.WrapContents = true;
            flow.AutoScroll = true;
            flow.BackColor = BackgroundDark;
        }
    }
}
