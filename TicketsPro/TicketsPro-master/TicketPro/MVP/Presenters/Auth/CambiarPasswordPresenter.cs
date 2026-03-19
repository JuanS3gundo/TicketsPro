using Services.BLL;
using System;
using System.Drawing;
using TicketPro.MVP.Views;
namespace TicketPro.MVP.Presenters
{
    public class CambiarPasswordPresenter
    {
        private readonly ICambiarPasswordView _view;
        private readonly RecuperacionBLL _recuperacionBLL;
        private readonly string _emailUsuario;
        public CambiarPasswordPresenter(ICambiarPasswordView view, RecuperacionBLL recuperacionBLL, string email)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _recuperacionBLL = recuperacionBLL ?? throw new ArgumentNullException(nameof(recuperacionBLL));
            _emailUsuario = string.IsNullOrWhiteSpace(email) ? throw new ArgumentException(nameof(email)) : email;
        }
        public void GuardarPassword()
        {
            try
            {
                string nueva = _view.NuevaPassword;
                string confirmar = _view.ConfirmarPassword;
                if (string.IsNullOrWhiteSpace(nueva) || string.IsNullOrWhiteSpace(confirmar))
                {
                    _view.MostrarAdvertencia(LanguageBLL.Translate("CambiarPassword_Error_CamposVacios"), LanguageBLL.Translate("CambiarPassword_Error_CamposVacios_Titulo"));
                    return;
                }
                if (nueva != confirmar)
                {
                    _view.MostrarError(LanguageBLL.Translate("CambiarPassword_Error_PasswordsNoCoinciden"), LanguageBLL.Translate("CambiarPassword_Error_PasswordsNoCoinciden_Titulo"));
                    return;
                }
                bool ok = _recuperacionBLL.CambiarPassword(_emailUsuario, nueva);
                if (ok)
                {
                    _view.MostrarExito(LanguageBLL.Translate("CambiarPassword_Exito_Actualizada"), LanguageBLL.Translate("CambiarPassword_Exito_Actualizada_Titulo"));
                    _view.NavegarALogin();
                }
                else
                {
                    _view.MostrarEstado(LanguageBLL.Translate("CambiarPassword_Error_NoActualizada"), Color.LightCoral);
                }
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                _view.MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                _view.MostrarEstado(LanguageBLL.Translate("CambiarPassword_Error_General"), Color.LightCoral);
                _view.MostrarError(string.Format(LanguageBLL.Translate("CambiarPassword_Error_General_Detalle"), ex.Message), LanguageBLL.Translate("Error"));
            }
        }
    }
}
