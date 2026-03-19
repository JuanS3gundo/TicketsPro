using Services.BLL;
using Services.Services;
using System;
using TicketPro.MVP.Views;
namespace TicketPro.MVP.Presenters
{
    public class FormHistorialEquipoPresenter
    {
        private readonly IFormHistorialEquipoView _view;
        public FormHistorialEquipoPresenter(IFormHistorialEquipoView view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
        }
        public void Iniciar()
        {
            try
            {
                _view.EstablecerSubtitulo($"{LanguageBLL.Translate("Historial_Subtitulo")} {_view.EquipoId}");
                CargarHistorial();
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                _view.MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                _view.MostrarError($"Error al iniciar el historial: {ex.Message}");
            }
        }
        private void CargarHistorial()
        {
            try
            {
                var historial = BitacoraService.ObtenerBitacoraPorEquipoId(_view.EquipoId);
                _view.MostrarHistorial(historial);
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                _view.MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                _view.MostrarError($"Error al cargar historial: {ex.Message}");
            }
        }
    }
}
