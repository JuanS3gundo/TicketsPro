using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace TicketPro.UI
{
    public class ModernNavButton : Button
    {
        private bool _isActive;
        private Color _bg = Color.FromArgb(17, 24, 39);       
        private Color _bgHover = Color.FromArgb(31, 41, 55);  
        private Color _bgActive = Color.FromArgb(59, 130, 246); 
        private Color _fg = Color.White;
        [Browsable(true)]
        [Category("Appearance")]
        public bool IsActive
        {
            get => _isActive;
            set { _isActive = value; Invalidate(); }
        }
        public ModernNavButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            BackColor = _bg;
            ForeColor = _fg;
            Font = new Font("Segoe UI", 10.5f, FontStyle.Regular);
            TextAlign = ContentAlignment.MiddleLeft;
            Padding = new Padding(14, 0, 10, 0);
            Height = 44;
            Cursor = Cursors.Hand;
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (!IsActive) BackColor = _bgHover;
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (!IsActive) BackColor = _bg;
        }
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            BackColor = IsActive ? _bgActive : _bg;
            ForeColor = IsActive ? Color.White : _fg;
        }
        public void Activate()
        {
            IsActive = true;
            BackColor = _bgActive;
            ForeColor = Color.White;
        }
        public void Deactivate()
        {
            IsActive = false;
            BackColor = _bg;
            ForeColor = _fg;
        }
    }
}
