using Microsoft.Extensions.DependencyInjection;
using Services.BLL;
using Services.Services;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TicketPro.Logins;
using TicketPro.VistaCliente;
using TicketPro.MVP.Views;
using TicketPro.MVP.Presenters;
namespace TicketPro
{
    public partial class LogIn : BaseForm, ILogInView
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly LogInPresenter _presenter;
        public string Username => txtUser.Text;
        public string Password => txtPass.Text;
        public void HabilitarBotonLogin(bool habilitado)
        {
            BtnLogin.Enabled = habilitado;
        }
        public void MostrarError(string mensaje, string titulo = "Error")
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void MostrarMensaje(string mensaje, string titulo = "Informacion")
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void NavegarACliente(Guid idUsuario)
        {
            this.Hide();
            new FormCliente(idUsuario).Show();
        }
        public void NavegarAAdministradorTecnico(Guid idUsuario)
        {
            this.Hide();
            new VistaMainForm(idUsuario, _serviceProvider).Show();
        }
        public LogIn(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _presenter = new LogInPresenter(
                this,
                serviceProvider.GetRequiredService<LoginBLL>(),
                new UsuarioBLL() 
            );
            
            this.Tag = "LOGIN_WindowTitle";
            lblAppTitle.Tag = "LOGIN_AppTitle";
            lblAppSubtitle.Tag = "LOGIN_AppSubtitle";
            labelTitle.Tag = "LOGIN_Title";
            labelUsername.Tag = "LOGIN_LabelUsername";
            labelPassword.Tag = "LOGIN_LabelPassword";
            labelForgot.Tag = "LOGIN_LabelForgot";
            BtnLogin.Tag = "LOGIN_ButtonLogin";
            lblOrSeparator.Tag = "LOGIN_OrSeparator";
            BtnRegister.Tag = "LOGIN_ButtonCreateAccount";
            cmbIdioma.Items.Clear();
            cmbIdioma.Items.Add("🌐 Espanol");
            cmbIdioma.Items.Add("🌐 English");
            cmbIdioma.SelectedIndex =
                (LanguageBLL.LastLanguage == "en-US") ? 1 : 0;
            cmbIdioma.SelectedIndexChanged += CmbIdioma_SelectedIndexChanged;
            pnlLeft.MouseDown += PnlDrag_MouseDown;
            pnlLeft.MouseMove += PnlDrag_MouseMove;
            pnlLeft.MouseUp += PnlDrag_MouseUp;
            pnlTopBar.MouseDown += PnlDrag_MouseDown;
            pnlTopBar.MouseMove += PnlDrag_MouseMove;
            pnlTopBar.MouseUp += PnlDrag_MouseUp;
            txtPass.KeyPress += TxtPass_KeyPress;
            this.RefreshLanguage();
        }
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private void PnlDrag_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }
        private void PnlDrag_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (dragging)
            {
                Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(diff));
            }
        }
        private void PnlDrag_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            dragging = false;
        }
        private void TxtPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                BtnLogin_Click(this, EventArgs.Empty);
                e.Handled = true;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            _presenter.IniciarSesion();
        }
        private void BtnRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            _serviceProvider.GetRequiredService<Register>().Show();
        }
        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            new RecuperarPassword(_serviceProvider).Show();
        }
        private void LogIn_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void CmbIdioma_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbIdioma.SelectedItem == null)
                return;
            string selected = cmbIdioma.SelectedItem.ToString();
            if (selected.Contains("Espanol"))
                LanguageBLL.SetLanguage("es-ES");
            else
                LanguageBLL.SetLanguage("en-US");
            this.RefreshLanguage();
        }
    }
}
