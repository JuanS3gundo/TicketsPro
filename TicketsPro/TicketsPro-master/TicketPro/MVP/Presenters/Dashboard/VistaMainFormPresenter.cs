using System;
using System.Collections.Generic;
using System.Linq;
using Entity.Domain;
using Services.Services;
using BLL.Interfaces;
using TicketPro.MVP.Views;
using Services.BLL;
using BLL;
namespace TicketPro.MVP.Presenters
{
    public class VistaMainFormPresenter
    {
        private readonly IVistaMainFormView _view;
        private readonly ITicketBLL _ticketBLL;
        private readonly IEstadoTicketBLL _estadoBLL;
        private readonly ICategoriaTicketBLL _categoriaBLL;
        private readonly IUbicacionBLL _ubicacionBLL;
        private readonly IPrioridadBLL _prioridadBLL;
        private List<Services.DomainModel.Usuario> _cacheTecnicos;
        public VistaMainFormPresenter(
            IVistaMainFormView view,
            ITicketBLL ticketBLL,
            IEstadoTicketBLL estadoBLL,
            ICategoriaTicketBLL categoriaBLL,
            IUbicacionBLL ubicacionBLL,
            IPrioridadBLL prioridadBLL)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _ticketBLL = ticketBLL ?? throw new ArgumentNullException(nameof(ticketBLL));
            _estadoBLL = estadoBLL ?? throw new ArgumentNullException(nameof(estadoBLL));
            _categoriaBLL = categoriaBLL ?? throw new ArgumentNullException(nameof(categoriaBLL));
            _ubicacionBLL = ubicacionBLL ?? throw new ArgumentNullException(nameof(ubicacionBLL));
            _prioridadBLL = prioridadBLL ?? throw new ArgumentNullException(nameof(prioridadBLL));
        }
        public void Iniciar()
        {
            AplicarPermisos();
            CargarListasFiltros();
        }
        private void AplicarPermisos()
        {
            _view.HabilitarMenu("GESTION_ACCESOS", SessionService.TienePermiso("GESTION_ACCESOS"));
            _view.HabilitarMenu("GESTION_INVENTARIO", SessionService.TienePermiso("GESTION_INVENTARIO"));
            _view.HabilitarMenu("GESTION_MAESTROS", SessionService.TienePermiso("GESTION_MAESTROS"));
            _view.HabilitarMenu("VISUALIZAR_BITACORA", SessionService.TienePermiso("VISUALIZAR_BITACORA"));
            _view.HabilitarMenu("GESTION_ANALITICAS", SessionService.TienePermiso("GESTION_ANALITICAS"));
            _view.HabilitarMenu("GESTION_BACKUP", SessionService.TienePermiso("GESTION_BACKUP"));
        }
        private void CargarListasFiltros()
        {
            try
            {
                var estados = _estadoBLL.ObtenerEstados();
                var categorias = _categoriaBLL.ObtenerCategorias();
                var ubicaciones = _ubicacionBLL.ObtenerUbicaciones();
                var prioridades = _prioridadBLL.ObtenerTodas();
                var listaPrioridades = new List<PrioridadTicket> { new PrioridadTicket { Id = Guid.Empty, Nombre = "Todas" } };
                listaPrioridades.AddRange(prioridades);
                _cacheTecnicos = UsuarioBLL.Instance.ObtenerUsuariosPorFamilia("Tecnico");
                var opcionesSLA = new List<object>
                {
                    new { Id = "TODOS",     Nombre = LanguageBLL.Translate("Todos") },
                    new { Id = "VENCIDOS",  Nombre = LanguageBLL.Translate("Filtro_SLA_Vencidos") },
                    new { Id = "POR_VENCER", Nombre = LanguageBLL.Translate("Por_Vencer") },
                    new { Id = "OK",        Nombre = LanguageBLL.Translate("SLA_OK") },
                    new { Id = "SIN_SLA",   Nombre = LanguageBLL.Translate("Filtro_SLA_Sin_SLA") }
                };
                _view.PoblarFiltros(estados, categorias, ubicaciones, _cacheTecnicos, listaPrioridades, opcionesSLA);
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                _view.MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                _view.MostrarError(LanguageBLL.Translate("Main_Error_InicializarFiltros"), ex);
            }
        }
        public void CargarTickets()
        {
            try
            {
                var ticketsEnBruto = _ticketBLL.BuscarTickets(
                    _view.FiltroTexto,
                    _view.FiltroId,
                    _view.FiltroEstadoId,
                    _view.FiltroCategoriaId,
                    _view.FiltroUbicacionId,
                    _view.FiltroTecnicoId
                );
                if (_view.FiltroPrioridadId.HasValue && _view.FiltroPrioridadId.Value != Guid.Empty)
                {
                    ticketsEnBruto = ticketsEnBruto.Where(t => t.Prioridad != null && t.Prioridad.Id == _view.FiltroPrioridadId.Value).ToList();
                }
                string filtroSLA = _view.FiltroSLA;
                if (!string.IsNullOrEmpty(filtroSLA) && filtroSLA != "TODOS")
                {
                    ticketsEnBruto = FiltrarPorSLA(ticketsEnBruto, filtroSLA);
                }
                _view.RenderizarTickets(ticketsEnBruto, _cacheTecnicos ?? new List<Services.DomainModel.Usuario>());
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                _view.MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                _view.MostrarError(LanguageBLL.Translate("Main_Error_CargarTickets"), ex);
            }
        }
        private List<Ticket> FiltrarPorSLA(List<Ticket> tickets, string tipoFiltroSLA)
        {
            if (tipoFiltroSLA == "VENCIDOS")
            {
                return tickets.Where(t =>
                    t.FechaVencimiento.HasValue &&
                    t.FechaVencimiento.Value < DateTime.Now &&
                    t.Estado?.Nombre != "Resuelto" &&
                    t.Estado?.Nombre != "Cerrado"
                ).ToList();
            }
            else if (tipoFiltroSLA == "POR_VENCER")
            {
                return tickets.Where(t =>
                {
                    if (!t.FechaVencimiento.HasValue || t.Estado?.Nombre == "Resuelto" || t.Estado?.Nombre == "Cerrado")
                        return false;
                    TimeSpan tiempoRestante = t.FechaVencimiento.Value - DateTime.Now;
                    if (tiempoRestante.TotalHours < 0) return false;
                    TimeSpan tiempoTotal = t.FechaVencimiento.Value - t.FechaApertura;
                    if (tiempoTotal.TotalHours == 0) return false;
                    double porcentaje = ((tiempoTotal - tiempoRestante).TotalHours / tiempoTotal.TotalHours) * 100;
                    return porcentaje >= 80;
                }).ToList();
            }
            else if (tipoFiltroSLA == "OK")
            {
                return tickets.Where(t =>
                {
                    if (!t.FechaVencimiento.HasValue || t.Estado?.Nombre == "Resuelto" || t.Estado?.Nombre == "Cerrado")
                        return false;
                    TimeSpan tiempoRestante = t.FechaVencimiento.Value - DateTime.Now;
                    if (tiempoRestante.TotalHours < 0) return false;
                    TimeSpan tiempoTotal = t.FechaVencimiento.Value - t.FechaApertura;
                    if (tiempoTotal.TotalHours == 0) return true;
                    double porcentaje = ((tiempoTotal - tiempoRestante).TotalHours / tiempoTotal.TotalHours) * 100;
                    return porcentaje < 80;
                }).ToList();
            }
            else if (tipoFiltroSLA == "SIN_SLA")
            {
                return tickets.Where(t => !t.FechaVencimiento.HasValue).ToList();
            }
            return tickets;
        }
        public void LimpiarFiltros()
        {
            _view.LimpiarFiltrosUI();
            CargarTickets();
        }
        public void AccionAccesoDenegado()
        {
            _view.MostrarAdvertenciaAccesoDenegado();
        }
    }
}
