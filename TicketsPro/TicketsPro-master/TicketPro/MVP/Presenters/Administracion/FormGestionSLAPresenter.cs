using System;
using BLL.Interfaces;
using Entity.Domain;
using Services.BLL;
using TicketPro.MVP.Views;
namespace TicketPro.MVP.Presenters
{
    public class FormGestionSLAPresenter
    {
        private readonly IFormGestionSLAView _view;
        private readonly ISLABLL _slaBLL;
        private readonly IPrioridadBLL _prioridadBLL;
        private PoliticaSLA _politicaActual;
        private bool _esNuevo;
        public FormGestionSLAPresenter(IFormGestionSLAView view, ISLABLL slaBLL, IPrioridadBLL prioridadBLL)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _slaBLL = slaBLL ?? throw new ArgumentNullException(nameof(slaBLL));
            _prioridadBLL = prioridadBLL ?? throw new ArgumentNullException(nameof(prioridadBLL));
        }
        public void Iniciar()
        {
            CargarPrioridades();
            CargarPoliticas();
            _view.LimpiarFormulario();
            _view.HabilitarFormulario(false);
        }
        private void CargarPrioridades()
        {
            try
            {
                var prioridades = _prioridadBLL.ObtenerTodas();
                _view.MostrarPrioridades(prioridades);
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                _view.MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                _view.MostrarError("Error al cargar prioridades", ex);
            }
        }
        public void CargarPoliticas()
        {
            try
            {
                var politicas = _slaBLL.ObtenerTodas();
                _view.MostrarPoliticas(politicas, politicas.Count);
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                _view.MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                _view.MostrarError("Error al cargar politicas", ex);
            }
        }
        public void SeleccionarPolitica(PoliticaSLA politica)
        {
            if (politica == null) return;
            _politicaActual = politica;
            _esNuevo = false;
            _view.CargarDatosFormulario(politica);
            _view.HabilitarFormulario(true);
            _view.ConfigurarParaEdicion();
        }
        public void PrepararNuevo()
        {
            _esNuevo = true;
            _politicaActual = null;
            _view.LimpiarFormulario();
            _view.HabilitarFormulario(true);
            _view.ConfigurarParaNuevaPolitica();
            _view.EstablecerFocoNuevo();
        }
        public void Guardar()
        {
            if (string.IsNullOrWhiteSpace(_view.Nombre))
            {
                _view.MostrarMensajeValidacion(LanguageBLL.Translate("SLA_Nombre_Obligatorio"));
                _view.EstablecerFocoNuevo();
                return;
            }
            if (!_view.PrioridadId.HasValue || _view.PrioridadId.Value == Guid.Empty)
            {
                _view.MostrarMensajeValidacion(LanguageBLL.Translate("SLA_Prioridad_Obligatoria"));
                _view.EstablecerFocoPrioridad();
                return;
            }
            if (_view.HorasAtencion <= 0 || _view.HorasResolucion <= 0)
            {
                _view.MostrarMensajeValidacion(LanguageBLL.Translate("SLA_Horas_Mayores_Cero"));
                return;
            }
            if (_view.HorasAtencion >= _view.HorasResolucion)
            {
                _view.MostrarMensajeValidacion(LanguageBLL.Translate("SLA_HorasAtencion_Menor_HorasResolucion"));
                return;
            }
            
            var prioridad = _prioridadBLL.ObtenerPorId(_view.PrioridadId.Value);
            var politica = new PoliticaSLA
            {
                Id = _esNuevo ? Guid.NewGuid() : _politicaActual.Id,
                Nombre = _view.Nombre.Trim(),
                Prioridad = prioridad,
                HorasAtencion = _view.HorasAtencion,
                HorasResolucion = _view.HorasResolucion,
                SoloHorasLaborales = _view.SoloHorasLaborales
            };
            string mensaje;
            bool exito;
            try
            {
                if (_esNuevo)
                {
                    exito = _slaBLL.CrearPoliticaSLA(politica, out mensaje);
                }
                else
                {
                    exito = _slaBLL.ActualizarPoliticaSLA(politica, out mensaje);
                }
                if (exito)
                {
                    _view.MostrarMensajeExito(mensaje);
                    CargarPoliticas();
                    _view.LimpiarFormulario();
                    _view.HabilitarFormulario(false);
                }
                else
                {
                    _view.MostrarError(mensaje);
                }
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
        public void Cancelar()
        {
            _view.LimpiarFormulario();
            _view.HabilitarFormulario(false);
        }
        public void Eliminar()
        {
            if (_politicaActual == null) return;
            if (!_view.PedirConfirmacionEliminacion(_politicaActual.Nombre))
                return;
            try
            {
                string mensaje;
                bool exito = _slaBLL.EliminarPoliticaSLA(_politicaActual.Id, out mensaje);
                if (exito)
                {
                    _view.MostrarMensajeExito(mensaje);
                    CargarPoliticas();
                    _view.LimpiarFormulario();
                    _view.HabilitarFormulario(false);
                }
                else
                {
                    _view.MostrarError(mensaje);
                }
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
    }
}
