using Services.BLL;
using System;
using System.Drawing;
using TicketPro.MVP.Views;
namespace TicketPro.MVP.Presenters
{
    public class ValidarCodigoPresenter
    {
        private readonly IValidarCodigoView _view;
        private readonly RecuperacionBLL _recuperacionBLL;
        private readonly string _emailUsuario;
        public ValidarCodigoPresenter(IValidarCodigoView view, RecuperacionBLL recuperacionBLL, string email)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _recuperacionBLL = recuperacionBLL ?? throw new ArgumentNullException(nameof(recuperacionBLL));
            _emailUsuario = string.IsNullOrWhiteSpace(email) ? throw new ArgumentException(nameof(email)) : email;
        }
        public void ValidarCodigo()
        {
            try
            {
                string codigo = _view.Codigo;
                if (string.IsNullOrWhiteSpace(codigo))
                {
                    _view.MostrarAdvertencia(LanguageBLL.Translate("ValidarCodigo_Error_CodigoVacio"), LanguageBLL.Translate("ValidarCodigo_Error_CodigoVacio_Titulo"));
                    return;
                }
                bool valido = _recuperacionBLL.ValidarCodigo(_emailUsuario, codigo);
                if (valido)
                {
                    _view.MostrarExito("Codigo validado correctamente.", "Exito");
                    _view.NavegarACambiarPassword(_emailUsuario);
                }
                else
                {
                    _view.MostrarEstado("Codigo incorrecto o expirado.", Color.LightCoral);
                }
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                _view.MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                _view.MostrarError($"Error al validar el codigo: {ex.Message}");
            }
        }
    }
}
