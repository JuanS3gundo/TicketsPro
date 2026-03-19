using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
namespace TicketPro.UI
{
    public class ModernTopButton : Button
    {
        public ModernTopButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            BackColor = Color.FromArgb(243, 244, 246); 
            ForeColor = Color.FromArgb(17, 24, 39);
            Font = new Font("Segoe UI Semibold", 10f, FontStyle.Bold);
            Height = 34;
            Width = 120;
            Cursor = Cursors.Hand;
        }
        protected override void OnPaint(PaintEventArgs pevent)
        {
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            int radius = 8;
            using (GraphicsPath path = RoundedPath(rect, radius))
            using (SolidBrush b = new SolidBrush(BackColor))
            using (Pen p = new Pen(Color.FromArgb(229, 231, 235)))
            {
                pevent.Graphics.FillPath(b, path);
                pevent.Graphics.DrawPath(p, path);
            }
            TextRenderer.DrawText(pevent.Graphics, Text, Font, rect, ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
        private static GraphicsPath RoundedPath(Rectangle r, int radius)
        {
            int d = radius * 2;
            GraphicsPath p = new GraphicsPath();
            p.AddArc(r.X, r.Y, d, d, 180, 90);
            p.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            p.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            p.CloseFigure();
            return p;
        }
    }
}
