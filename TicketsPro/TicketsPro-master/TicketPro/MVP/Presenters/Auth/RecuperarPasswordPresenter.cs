using Services.BLL;
using System;
using TicketPro.MVP.Views;
using System.Drawing;
namespace TicketPro.MVP.Presenters
{
    public class RecuperarPasswordPresenter
    {
        private readonly IRecuperarPasswordView _view;
        private readonly RecuperacionBLL _recuperacionBLL;
        private readonly UsuarioBLL _usuarioBLL;
        public RecuperarPasswordPresenter(IRecuperarPasswordView view, RecuperacionBLL recuperacionBLL, UsuarioBLL usuarioBLL)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _recuperacionBLL = recuperacionBLL ?? throw new ArgumentNullException(nameof(recuperacionBLL));
            _usuarioBLL = usuarioBLL ?? throw new ArgumentNullException(nameof(usuarioBLL));
        }
        public void EnviarCodigo()
        {
            string userName = _view.UserName;
            if (string.IsNullOrWhiteSpace(userName))
            {
                _view.MostrarAdvertencia(LanguageBLL.Translate("Recuperacion_Msg_Ingresar_Usuario"), LanguageBLL.Translate("Advertencia"));
                return;
            }
            try
            {
                var usuario = _usuarioBLL.GetByUserName(userName);
                if (usuario == null)
                {
                    _view.MostrarError(LanguageBLL.Translate("Recuperacion_Msg_Usuario_No_Existe"), LanguageBLL.Translate("Error"));
                    return;
                }
                if (string.IsNullOrWhiteSpace(usuario.Email))
                {
                    _view.MostrarError(LanguageBLL.Translate("Recuperacion_Msg_Sin_Email"), LanguageBLL.Translate("Error"));
                    return;
                }
                _recuperacionBLL.EnviarCodigo(usuario.Email);
                _view.MostrarEstado($"{LanguageBLL.Translate("Recuperacion_Msg_Codigo_Enviado_A")}: {usuario.Email}", Color.LightGreen);
                _view.MostrarExito(LanguageBLL.Translate("Recuperacion_Msg_Codigo_Enviado"), LanguageBLL.Translate("Exito"));
                _view.NavegarAValidarCodigo(usuario.Email);
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                _view.MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                _view.MostrarEstado(LanguageBLL.Translate("Recuperacion_Msg_Error_Envio"), Color.LightCoral);
                _view.MostrarError(string.Format(LanguageBLL.Translate("RecuperarPassword_Error_EnvioCodigo"), ex.Message), LanguageBLL.Translate("RecuperarPassword_Error_EnvioCodigo_Titulo"));
            }
        }
    }
}
