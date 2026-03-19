using Services.BLL;
using Services.DomainModel;
using Services.Services;
using System;
using System.Linq;
using TicketPro.MVP.Views;
namespace TicketPro.MVP.Presenters
{
    public class LogInPresenter
    {
        private readonly ILogInView _view;
        private readonly LoginBLL _loginBLL;
        private readonly UsuarioBLL _usuarioBLL;
        public LogInPresenter(ILogInView view, LoginBLL loginBLL, UsuarioBLL usuarioBLL)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _loginBLL = loginBLL ?? throw new ArgumentNullException(nameof(loginBLL));
            _usuarioBLL = usuarioBLL ?? throw new ArgumentNullException(nameof(usuarioBLL));
        }
        public void IniciarSesion()
        {
            _view.HabilitarBotonLogin(false);
            try
            {
                var usuario = _loginBLL.IniciarSesion(_view.Username, _view.Password);
                if (usuario == null)
                {
                    _view.MostrarError(LanguageBLL.Translate("LOGIN_Error_InvalidCredentials"), LanguageBLL.Translate("LOGIN_Title"));
                    return;
                }
                SessionService.SetUsuario(usuario);
                var familiasUsuario = _usuarioBLL.ObtenerFamiliasDelUsuario(usuario.IdUsuario);
                if (familiasUsuario == null || familiasUsuario.Count == 0)
                {
                    _view.MostrarError(LanguageBLL.Translate("LOGIN_Error_NoFamily"), LanguageBLL.Translate("LOGIN_Title"));
                    return;
                }
                var familia = familiasUsuario.First();
                _view.MostrarMensaje($"{LanguageBLL.Translate("MAIN_Header_Welcome")} {usuario.UserName}!", LanguageBLL.Translate("LOGIN_Title"));
                if (familia.NombreFamilia == "Cliente")
                {
                    _view.NavegarACliente(usuario.IdUsuario);
                }
                else 
                {
                    _view.NavegarAAdministradorTecnico(usuario.IdUsuario);
                }
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                _view.MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                _view.MostrarError($"{LanguageBLL.Translate("Error_General")}: {ex.Message}", LanguageBLL.Translate("Login_Error_InicioSesion_Titulo"));
            }
            finally
            {
                _view.HabilitarBotonLogin(true);
            }
        }
    }
}
