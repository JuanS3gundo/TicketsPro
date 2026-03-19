using System;
using BLL.Interfaces;
using Services.BLL;
using Services.Services;
using TicketPro.MVP.Views;
namespace TicketPro.MVP.Presenters
{
    public class SolucionTicketPresenter
    {
        private readonly ISolucionTicketView _view;
        private readonly ISolucionTicketBLL _solucionService;
        public SolucionTicketPresenter(ISolucionTicketView view, ISolucionTicketBLL solucionService)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _solucionService = solucionService ?? throw new ArgumentNullException(nameof(solucionService));
        }
        public void GuardarSolucion()
        {
            if (string.IsNullOrWhiteSpace(_view.SolucionData))
            {
                _view.MostrarMensajeAdvertencia(LanguageBLL.Translate("TicketSolucion_Msg_Vacio"), LanguageBLL.Translate("Error_Validacion"));
                return;
            }
            try
            {
                _solucionService.AgregarSolucion(_view.TicketId, _view.SolucionData.Trim());
                _view.MostrarMensajeExito(LanguageBLL.Translate("TicketSolucion_Msg_Exito"), LanguageBLL.Translate("Info"));
                _view.CerrarFormularioConExito();
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                _view.MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                _view.MostrarError(LanguageBLL.Translate("Ticket_Solucion_Error_Guardar"), ex);
            }
        }
        public void Cancelar()
        {
            _view.CancelarYCerrar();
        }
    }
}
