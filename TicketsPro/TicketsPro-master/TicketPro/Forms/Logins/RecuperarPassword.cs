using Services.BLL;
using System;
using System.Drawing;
using System.Windows.Forms;
using Services.Services;
using Microsoft.Extensions.DependencyInjection;
using TicketPro.MVP.Views;
using TicketPro.MVP.Presenters;
using Services.Implementations;
namespace TicketPro.Logins
{
    public partial class RecuperarPassword : BaseForm, IRecuperarPasswordView
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly RecuperarPasswordPresenter _presenter;
        public string UserName => txtUser.Text.Trim();
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
        public void NavegarAValidarCodigo(string email)
        {
            var formValidar = new ValidarCodigo(email, _serviceProvider);
            formValidar.Show();
            this.Close();
        }
        public void CerrarPantalla()
        {
            this.Close();
        }
        public RecuperarPassword(IServiceProvider serviceProvider = null)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _presenter = new RecuperarPasswordPresenter(
                this,
                new RecuperacionBLL(),
                new UsuarioBLL()
            );
            
            this.Tag = "Recuperar_WindowTitle";
            lblTitulo.Tag = "Recuperar_LabelTitulo";
            lblSubtitulo.Tag = "Recuperar_LabelSubtitulo";
            lblUser.Tag = "Recuperar_LabelUser";
            lblEstado.Tag = "Recuperar_LabelEstado";
            btnEnviarCodigo.Tag = "Recuperar_ButtonEnviar";
            btnVolverLogin.Tag = "Recuperar_ButtonVolver";
            this.RefreshLanguage();
        }
        private void btnVolverLogin_Click(object sender, EventArgs e)
        {
            this.Close();
            if (_serviceProvider != null)
            {
                var login = _serviceProvider.GetRequiredService<LogIn>();
                login.Show();
            }
            else
            {
                MessageBox.Show(LanguageBLL.Translate("LOGIN_Error_NoClosingApp"),
                    LanguageBLL.Translate("Advertencia"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }
        }
        private void btnEnviarCodigo_Click_1(object sender, EventArgs e)
        {
            _presenter.EnviarCodigo();
        }
    }
}
