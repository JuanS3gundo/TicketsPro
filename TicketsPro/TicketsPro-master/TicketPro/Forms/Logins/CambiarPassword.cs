using System;
using System.Drawing;
using System.Windows.Forms;
using BLL;
using Services.BLL;
using TicketPro.MVP.Views;
using TicketPro.MVP.Presenters;
namespace TicketPro.Logins
{
    public partial class CambiarPassword : BaseForm, ICambiarPasswordView
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly CambiarPasswordPresenter _presenter;
        public string NuevaPassword => txtNueva.Text.Trim();
        public string ConfirmarPassword => txtConfirmar.Text.Trim();
        public void MostrarEstado(string mensaje, Color color)
        {
            lblEstado.Text = mensaje;
            lblEstado.ForeColor = color;
        }
        public void MostrarError(string mensaje, string titulo = "Error")
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void MostrarAdvertencia(string mensaje, string titulo = "Advertencia")
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public void MostrarExito(string mensaje, string titulo = "exito")
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void NavegarALogin()
        {
            var login = new LogIn(_serviceProvider);
            login.Show();
            this.Close();
        }
        public void CerrarPantalla()
        {
            this.Close();
        }
        public CambiarPassword(string email, IServiceProvider serviceProvider = null)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _presenter = new CambiarPasswordPresenter(this, new RecuperacionBLL(), email);
            
            this.Tag = "CambiarPassword_WindowTitle";
            lblTitulo.Tag = "CambiarPassword_LabelTitulo";
            lblSubtitulo.Tag = "CambiarPassword_LabelSubtitulo";
            lblNueva.Tag = "CambiarPassword_LabelNueva";
            lblConfirmar.Tag = "CambiarPassword_LabelConfirmar";
            lblRequisitos.Tag = "CambiarPassword_LabelRequisitos";
            btnGuardar.Tag = "CambiarPassword_ButtonGuardar";
            btnVolverLogin.Tag = "CambiarPassword_ButtonVolver";
            this.RefreshLanguage();
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            _presenter.GuardarPassword();
        }
        private void btnVolverLogin_Click(object sender, EventArgs e)
        {
            var login = new LogIn(_serviceProvider);
            login.Show();
            this.Close();
        }
    }
}
