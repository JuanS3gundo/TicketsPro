using Microsoft.Extensions.DependencyInjection;
using Services.BLL;
using Services.DomainModel;
using System;
using System.Linq;
using System.Windows.Forms;
using TicketPro.MVP.Views;
using TicketPro.MVP.Presenters;
namespace TicketPro
{
    public partial class Register : BaseForm, IRegisterView
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly RegisterPresenter _presenter;
        public string UserName => txtUser.Text.Trim();
        public string Password => txtPass.Text.Trim();
        public string Email => txtEmail.Text.Trim();
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
            this.Hide();
            var loginForm = _serviceProvider.GetRequiredService<LogIn>();
            loginForm.StartPosition = FormStartPosition.CenterScreen;
            loginForm.Show();
        }
        public void CerrarPantalla()
        {
            this.Close();
        }
        public Register(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _presenter = new RegisterPresenter(this, serviceProvider.GetRequiredService<LoginBLL>());
            
            this.Tag = "Register_WindowTitle";
            lblTitulo.Tag = "Register_Titulo";
            lblSubtitulo.Tag = "Register_Subtitulo";
            lblEmail.Tag = "Register_LabelEmail";
            lblUser.Tag = "Register_LabelUser";
            lblPassword.Tag = "Register_LabelPassword";
            lblRequisitos.Tag = "Register_LabelRequisitos";
            BtnRegister.Tag = "Register_ButtonRegister";
            btnVolverLogin.Tag = "Register_ButtonVolver";
            this.RefreshLanguage();
        }
        private void BtnRegister_Click(object sender, EventArgs e)
        {
            _presenter.RegistrarUsuario();
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
    }
}
