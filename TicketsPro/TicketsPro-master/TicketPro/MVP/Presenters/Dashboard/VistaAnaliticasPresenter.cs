using System;
using System.Collections.Generic;
using BLL.Implementations;
using Entity.DTOs;
using TicketPro.MVP.Views;
namespace TicketPro.MVP.Presenters
{
    public class VistaAnaliticasPresenter
    {
        private readonly IVistaAnaliticasView _view;
        private readonly AnaliticasService _analiticasService;
        public VistaAnaliticasPresenter(IVistaAnaliticasView view, AnaliticasService analiticasService)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _analiticasService = analiticasService ?? throw new ArgumentNullException(nameof(analiticasService));
        }
        public void CargarYMostrarAnaliticas()
        {
            try
            {
                var datosCrudos = _analiticasService.ObtenerDatosCrudos();
                _view.LimpiarGraficos();
                if (datosCrudos == null || datosCrudos.Count == 0)
                {
                    _view.MostrarMensajeSinDatos();
                    return;
                }
                var datosEstado = _analiticasService.ProcesarTicketsPorEstado(datosCrudos);
                _view.CargarTicketsPorEstado(datosEstado);
                var datosAHT = _analiticasService.ProcesarTiempoSolucionPorTecnico(datosCrudos);
                _view.CargarTiempoSolucionPorTecnico(datosAHT);
                var datosCategoria = _analiticasService.ProcesarTicketsPorCategoria(datosCrudos);
                _view.CargarTicketsPorCategoria(datosCategoria);
                var datosTendencia = _analiticasService.ProcesarTendencia(datosCrudos);
                _view.CargarTendencia(datosTendencia);
                var datosSLA = _analiticasService.ProcesarCumplimientoSLA(datosCrudos);
                _view.CargarCumplimientoSLA(datosSLA);
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                _view.MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                _view.MostrarError("Error al cargar los datos de Analiticas", ex);
            }
        }
    }
}
