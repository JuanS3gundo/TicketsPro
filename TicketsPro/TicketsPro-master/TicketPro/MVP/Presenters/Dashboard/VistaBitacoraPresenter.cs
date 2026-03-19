using System;
using System.Collections.Generic;
using Services.BLL;
using Services.Services;
using TicketPro.MVP.Views;
namespace TicketPro.MVP.Presenters
{
    public class VistaBitacoraPresenter
    {
        private readonly IVistaBitacoraView _view;
        public VistaBitacoraPresenter(IVistaBitacoraView view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
        }
        public void Iniciar()
        {
            _view.CargarNiveles(new[] { "", "INFO", "WARNING", "ERROR" });
            LimpiarFiltros();
        }
        public void CargarDatos()
        {
            try
            {
                DateTime desde = _view.FechaDesde.Date;
                DateTime hasta = _view.FechaHasta.Date.AddDays(1).AddSeconds(-1);
                if (desde > hasta.Date)
                {
                    _view.MostrarMensajeValidacion(LanguageBLL.Translate("Msg_Fecha_Invalida"), LanguageBLL.Translate("Error_Validacion"));
                    return;
                }
                var bitacora = BitacoraService.ObtenerBitacoraPorFiltros(
                    desde, 
                    hasta, 
                    _view.UsuarioFiltro, 
                    _view.NivelFiltro, 
                    _view.AccionFiltro, 
                    _view.LimiteRegistros
                );
                _view.DatosBitacora = bitacora;
                _view.ConfigurarEstilosYColumnasGrid();
                _view.ColorizarFilasGrid();
                int count = 0;
                if (bitacora is System.Collections.ICollection collection)
                {
                    count = collection.Count;
                }
                _view.ActualizarContadorInfo(count, _view.LimiteRegistros);
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                _view.MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                _view.MostrarError("Error", ex);
            }
        }
        public void LimpiarFiltros()
        {
            _view.FechaDesde = DateTime.Now.AddDays(-7).Date;
            _view.FechaHasta = DateTime.Now.Date;
            _view.UsuarioFiltro = null;
            _view.NivelFiltro = null;
            _view.AccionFiltro = null;
            CargarDatos();
        }
    }
}
