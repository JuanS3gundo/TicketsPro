using Services.BLL;
using Services.DomainModel;
using System;
using System.Linq;
using TicketPro.MVP.Views;
namespace TicketPro.MVP.Presenters
{
    public class RegisterPresenter
    {
        private readonly IRegisterView _view;
        private readonly LoginBLL _loginBLL;
        public RegisterPresenter(IRegisterView view, LoginBLL loginBLL)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _loginBLL = loginBLL ?? throw new ArgumentNullException(nameof(loginBLL));
        }
        public void RegistrarUsuario()
        {
            try
            {
                var userName = _view.UserName;
                var pass = _view.Password;
                var email = _view.Email;
                if (string.IsNullOrWhiteSpace(userName) ||
                    string.IsNullOrWhiteSpace(pass) ||
                    string.IsNullOrWhiteSpace(email))
                {
                    _view.MostrarAdvertencia(LanguageBLL.Translate("Register_Error_CamposIncompletos"), LanguageBLL.Translate("Register_Error_CamposIncompletos_Titulo"));
                    return;
                }
                if (!PasswordOk(pass))
                {
                    _view.MostrarAdvertencia(LanguageBLL.Translate("Register_Error_PasswordInsegura"), LanguageBLL.Translate("Register_Error_PasswordInsegura_Titulo"));
                    return;
                }
                if (!email.Contains("@") || !email.Contains("."))
                {
                    _view.MostrarError(LanguageBLL.Translate("Register_Error_EmailInvalido"), LanguageBLL.Translate("Register_Error_EmailInvalido_Titulo"));
                    return;
                }
                var nuevoUsuario = new Usuario
                {
                    UserName = userName,
                    Password = pass,
                    Email = email
                };
                _loginBLL.RegistrarUsuario(nuevoUsuario);
                _view.MostrarExito(LanguageBLL.Translate("Register_Exito_UsuarioCreado"), LanguageBLL.Translate("Register_Exito_UsuarioCreado_Titulo"));
                _view.NavegarALogin();
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                _view.MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                _view.MostrarError($"{LanguageBLL.Translate("Register_Error_RegistroFallido")}: {ex.Message}", LanguageBLL.Translate("Register_Error_RegistroFallido"));
            }
        }
        private bool PasswordOk(string p)
        {
            if (string.IsNullOrWhiteSpace(p) || p.Length < 8) return false;
            bool mayus = p.Any(char.IsUpper);
            bool minus = p.Any(char.IsLower);
            bool num = p.Any(char.IsDigit);
            return mayus && minus && num;
        }
    }
}
