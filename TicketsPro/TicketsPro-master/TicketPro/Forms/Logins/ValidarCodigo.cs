using System;
using System.Drawing;
using System.Windows.Forms;
using BLL;
using Services.BLL;
using TicketPro.MVP.Views;
using TicketPro.MVP.Presenters;
namespace TicketPro.Logins
{
    public partial class ValidarCodigo : BaseForm, IValidarCodigoView
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ValidarCodigoPresenter _presenter;
        public string Codigo => txtCodigo.Text.Trim();
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
        public void NavegarACambiarPassword(string email)
        {
            var formCambiar = new CambiarPassword(email, _serviceProvider);
            formCambiar.Show();
            this.Close();
        }
        public void CerrarPantalla()
        {
            this.Close();
        }
        public ValidarCodigo(string email, IServiceProvider serviceProvider = null)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _presenter = new ValidarCodigoPresenter(this, new RecuperacionBLL(), email);
            
            this.Tag = "ValidarCodigo_WindowTitle";
            lblTitulo.Tag = "ValidarCodigo_LabelTitulo";
            lblSubtitulo.Tag = "ValidarCodigo_LabelSubtitulo";
            lblCodigo.Tag = "ValidarCodigo_LabelCodigo";
            btnValidar.Tag = "ValidarCodigo_ButtonValidar";
            btnVolverLogin.Tag = "ValidarCodigo_ButtonVolver";
            this.RefreshLanguage();
        }
        private void btnValidar_Click(object sender, EventArgs e)
        {
            _presenter.ValidarCodigo();
        }
        private void btnVolverLogin_Click(object sender, EventArgs e)
        {
            var login = new LogIn(_serviceProvider);
            login.Show();
            this.Close();
        }
    }
}
