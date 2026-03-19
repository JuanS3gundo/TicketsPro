using Services.BLL;
using Services.Implementations;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
public class BaseForm : Form
{
    protected static readonly LoggerService Logger = LoggerService.Instance;
    private bool _dragging = false;
    private Point _dragCursorPoint;
    private Point _dragFormPoint;
    private Control _dragPanel;
    private const int WM_NCHITTEST = 0x84;
    private const int HTCLIENT = 1;
    private const int HTLEFT = 10;
    private const int HTRIGHT = 11;
    private const int HTTOP = 12;
    private const int HTTOPLEFT = 13;
    private const int HTTOPRIGHT = 14;
    private const int HTBOTTOM = 15;
    private const int HTBOTTOMLEFT = 16;
    private const int HTBOTTOMRIGHT = 17;
    private const int RESIZE_BORDER_WIDTH = 8; 
    public BaseForm()
    {
        this.Load += BaseForm_InitializeDragging;
        this.Load += (s, e) => RefreshLanguage();
        this.AutoScaleMode = AutoScaleMode.Font;
        this.AutoScaleDimensions = new SizeF(6F, 13F);
    }
    private void BaseForm_InitializeDragging(object sender, System.EventArgs e)
    {
        _dragPanel = FindDragPanel(this);
        if (_dragPanel != null)
        {
            _dragPanel.MouseDown += DragPanel_MouseDown;
            _dragPanel.MouseMove += DragPanel_MouseMove;
            _dragPanel.MouseUp += DragPanel_MouseUp;
            _dragPanel.Cursor = Cursors.SizeAll;
        }
        else if (this.FormBorderStyle == FormBorderStyle.None)
        {
            this.MouseDown += DragPanel_MouseDown;
            this.MouseMove += DragPanel_MouseMove;
            this.MouseUp += DragPanel_MouseUp;
        }
        MakeResponsive();
    }
    protected virtual void MakeResponsive()
    {
        if (this.MinimumSize == Size.Empty)
        {
            this.MinimumSize = new Size(
                this.Width > 400 ? 400 : this.Width,
                this.Height > 300 ? 300 : this.Height
            );
        }
        ApplyResponsiveToControls(this);
    }
    private void ApplyResponsiveToControls(Control parent)
    {
        foreach (Control ctrl in parent.Controls)
        {
            bool hasDefaultAnchor = ctrl.Anchor == (AnchorStyles.Top | AnchorStyles.Left);
            if ((ctrl is TextBox || ctrl is ComboBox || ctrl is NumericUpDown) && hasDefaultAnchor)
            {
                if (ctrl.Parent != null && ctrl.Parent.ClientSize.Width > 0 &&
                    ctrl.Width > ctrl.Parent.ClientSize.Width * 0.7)
                {
                    ctrl.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                }
            }
            else if ((ctrl is DataGridView || (ctrl is ListBox && ctrl.Dock == DockStyle.None)) && hasDefaultAnchor)
            {
                if (ctrl.Parent != null)
                {
                    ctrl.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                }
            }
            else if (ctrl is Button && hasDefaultAnchor)
            {
                var btn = ctrl as Button;
                if (btn.Parent != null && btn.Parent.ClientSize.Width > 0 && btn.Parent.ClientSize.Height > 0)
                {
                    AnchorStyles newAnchor = AnchorStyles.Top | AnchorStyles.Left;
                    if (btn.Left > btn.Parent.ClientSize.Width / 2)
                    {
                        newAnchor = AnchorStyles.Right | AnchorStyles.Top;
                    }
                    if (btn.Top > btn.Parent.ClientSize.Height * 0.7)
                    {
                        newAnchor = (newAnchor & AnchorStyles.Right) == AnchorStyles.Right
                            ? AnchorStyles.Right | AnchorStyles.Bottom
                            : AnchorStyles.Left | AnchorStyles.Bottom;
                    }
                    btn.Anchor = newAnchor;
                }
            }
            if (ctrl.HasChildren && !(ctrl is TextBox) && !(ctrl is ComboBox))
            {
                ApplyResponsiveToControls(ctrl);
            }
        }
    }
    private Control FindDragPanel(Control parent)
    {
        foreach (Control ctrl in parent.Controls)
        {
            string name = ctrl.Name?.ToLower() ?? "";
            if (ctrl is Panel && (
                name.Contains("top") ||
                name.Contains("header") ||
                name.Contains("titulo") ||
                name.Contains("title") ||
                name.Contains("cabecera")))
            {
                return ctrl;
            }
        }
        return null;
    }
    private void DragPanel_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            _dragging = true;
            _dragCursorPoint = Cursor.Position;
            _dragFormPoint = this.Location;
        }
    }
    private void DragPanel_MouseMove(object sender, MouseEventArgs e)
    {
        if (_dragging)
        {
            Point diff = Point.Subtract(Cursor.Position, new Size(_dragCursorPoint));
            this.Location = Point.Add(_dragFormPoint, new Size(diff));
        }
    }
    private void DragPanel_MouseUp(object sender, MouseEventArgs e)
    {
        _dragging = false;
    }
    public void RefreshLanguage()
    {
        if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            return;
        TraducirControles(this);
    }
    private void BaseForm_Load(object sender, System.EventArgs e)
    {
        if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            return;
        RefreshLanguage();
    }
    private void TraducirControles(Control parent)
    {
        if (parent is TextBox || parent is ComboBox || parent is DateTimePicker)
            goto Recorrer;
        string key = parent.Tag?.ToString(); 
        if (string.IsNullOrWhiteSpace(key))
            key = parent.Text;
        if (string.IsNullOrWhiteSpace(key) || key.Contains(" "))
            goto Recorrer;
        string translated = LanguageBLL.Translate(key);
        if (translated != "[" + key + "]")
            parent.Text = translated;
        if (parent is MenuStrip menu)
        {
            foreach (ToolStripMenuItem item in menu.Items)
                TraducirMenuItem(item);
            return;
        }
    Recorrer:
        foreach (Control ctrl in parent.Controls)
            TraducirControles(ctrl);
    }
    private void TraducirMenuItem(ToolStripMenuItem item)
    {
        string key = item.Text;
        if (!string.IsNullOrWhiteSpace(key) && !key.Contains(" "))
        {
            string translated = LanguageBLL.Translate(key);
            if (translated != "[" + key + "]")
                item.Text = translated;
        }
        foreach (ToolStripMenuItem sub in item.DropDownItems)
            TraducirMenuItem(sub);
    }
    protected override void WndProc(ref Message m)
    {
        base.WndProc(ref m);
        if (m.Msg == WM_NCHITTEST && this.FormBorderStyle == FormBorderStyle.None)
        {
            Point pos = new Point(m.LParam.ToInt32());
            pos = this.PointToClient(pos);
            if (pos.X <= RESIZE_BORDER_WIDTH && pos.Y <= RESIZE_BORDER_WIDTH)
            {
                m.Result = (IntPtr)HTTOPLEFT;
            }
            else if (pos.X >= this.ClientSize.Width - RESIZE_BORDER_WIDTH && pos.Y <= RESIZE_BORDER_WIDTH)
            {
                m.Result = (IntPtr)HTTOPRIGHT;
            }
            else if (pos.X <= RESIZE_BORDER_WIDTH && pos.Y >= this.ClientSize.Height - RESIZE_BORDER_WIDTH)
            {
                m.Result = (IntPtr)HTBOTTOMLEFT;
            }
            else if (pos.X >= this.ClientSize.Width - RESIZE_BORDER_WIDTH && pos.Y >= this.ClientSize.Height - RESIZE_BORDER_WIDTH)
            {
                m.Result = (IntPtr)HTBOTTOMRIGHT;
            }
            else if (pos.X <= RESIZE_BORDER_WIDTH)
            {
                m.Result = (IntPtr)HTLEFT;
            }
            else if (pos.X >= this.ClientSize.Width - RESIZE_BORDER_WIDTH)
            {
                m.Result = (IntPtr)HTRIGHT;
            }
            else if (pos.Y <= RESIZE_BORDER_WIDTH)
            {
                m.Result = (IntPtr)HTTOP;
            }
            else if (pos.Y >= this.ClientSize.Height - RESIZE_BORDER_WIDTH)
            {
                m.Result = (IntPtr)HTBOTTOM;
            }
        }
    }
    protected void MostrarError(string contexto, Exception ex)
    {
        try { Logger.LogError(contexto, ex); } catch { }
        string mensajeAmigable = ObtenerMensajeAmigable(contexto);
        MessageBox.Show(
            mensajeAmigable,
            "Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
    }
    protected void MostrarError(string mensajeUsuario, string contextoLog, Exception ex)
    {
        try { Logger.LogError(contextoLog, ex); } catch { }
        MessageBox.Show(
            mensajeUsuario,
            "Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
    }
    private string ObtenerMensajeAmigable(string contexto)
    {
        if (string.IsNullOrWhiteSpace(contexto))
            return "Ocurrio un error inesperado. Por favor intente nuevamente o contacte al administrador.";
        string c = contexto.ToLower();
        if (c.Contains("cargar") || c.Contains("obtener") || c.Contains("buscar"))
            return $"No se pudieron cargar los datos. Por favor intente nuevamente.";
        if (c.Contains("guardar") || c.Contains("crear") || c.Contains("agregar") || c.Contains("asignar"))
            return $"No se pudo completar la operacion. Verifique los datos e intente nuevamente.";
        if (c.Contains("eliminar") || c.Contains("quitar"))
            return $"No se pudo eliminar el elemento. Es posible que tenga registros asociados.";
        if (c.Contains("login") || c.Contains("sesion") || c.Contains("acceso"))
            return "Error de autenticacion. Por favor intente nuevamente.";
        return $"Ocurrio un error al intentar realizar la operacion. Por favor intente nuevamente o contacte al administrador.";
    }
}
