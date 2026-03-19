using BLL.Interfaces;
using BLL;
using DAL.Contracts.UnitOfWork;
using Entity.Domain;
using Services.DomainModel;
using Services.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using TicketPro.MVP.Presenters.Base;
using TicketPro.MVP.Views;
namespace TicketPro.MVP.Presenters
{
    public class CrearTicketPresenter : IPresenter<ICrearTicketView>
    {
        public ICrearTicketView View { get; private set; }
        private readonly ICategoriaTicketBLL _categoriaBLL;
        private readonly IUbicacionBLL _ubicacionBLL;
        private readonly IPrioridadBLL _prioridadBLL;
        private readonly IEquipoBLL _equipoBLL;
        private readonly IEstadoTicketBLL _estadoBLL;
        private readonly ITicketBLL _ticketBLL;
        private readonly Guid _usuarioIdCorriente; 
        public CrearTicketPresenter(
            ICrearTicketView view, 
            Guid usuarioId,
            ICategoriaTicketBLL categoriaBLL,
            IUbicacionBLL ubicacionBLL,
            IPrioridadBLL prioridadBLL,
            IEquipoBLL equipoBLL,
            IEstadoTicketBLL estadoBLL,
            ITicketBLL ticketBLL)
        {
            View = view;
            _usuarioIdCorriente = usuarioId;
            _categoriaBLL = categoriaBLL;
            _ubicacionBLL = ubicacionBLL;
            _prioridadBLL = prioridadBLL;
            _equipoBLL = equipoBLL;
            _estadoBLL = estadoBLL;
            _ticketBLL = ticketBLL;
        }
        public void OnViewLoad()
        {
            try
            {
                View.CargarCategorias(_categoriaBLL.ObtenerCategorias().ToList());
                View.CargarUbicaciones(_ubicacionBLL.ObtenerUbicaciones().ToList());
                View.CargarPrioridades(_prioridadBLL.ObtenerTodas().ToList());
                View.CargarEquipos(_equipoBLL.ObtenerTodos());
                var tecnicos = UsuarioBLL.Instance.ObtenerUsuariosPorFamilia("Tecnico");
                View.CargarTecnicos(tecnicos);
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                View.MostrarError(ex.Message, LanguageBLL.Translate("Ticket_Error_Negocio_Titulo"));
            }
            catch (Exception ex)
            {
                View.MostrarError(string.Format(LanguageBLL.Translate("Ticket_Error_CargarDatosIniciales"), ex.Message));
            }
        }
        public void GuardarTicket()
        {
            string titulo = View.TituloTicket;
            string desc = View.DescripcionTicket;
            if (string.IsNullOrWhiteSpace(titulo))
            {
                View.MostrarAdvertencia(LanguageBLL.Translate("Ticket_Error_Titulo_Vacio"));
                return;
            }
            if (string.IsNullOrWhiteSpace(desc))
            {
                View.MostrarAdvertencia(LanguageBLL.Translate("Ticket_Error_Descripcion_Vacia"));
                return;
            }
            Guid? catId = View.CategoriaSeleccionada;
            if (!catId.HasValue || catId.Value == Guid.Empty)
            {
                View.MostrarAdvertencia(LanguageBLL.Translate("Seleccione_una_categoria_valida"));
                return;
            }
            Guid? ubiId = View.UbicacionSeleccionada;
            if (!ubiId.HasValue || ubiId.Value == Guid.Empty)
            {
                View.MostrarAdvertencia(LanguageBLL.Translate("Seleccione_una_ubicacion_valida"));
                return;
            }
            Guid? prioId = View.PrioridadSeleccionada;
            if (!prioId.HasValue || prioId.Value == Guid.Empty)
            {
                View.MostrarAdvertencia(LanguageBLL.Translate("Seleccione_una_prioridad_valida"));
                return;
            }
            try
            {
                var estadoNuevo = _estadoBLL.ObtenerEstados().FirstOrDefault(est => est.Nombre == "Nuevo");
                if (estadoNuevo == null)
                {
                    View.MostrarError(LanguageBLL.Translate("Ticket_Error_EstadoNuevoNoEncontrado"));
                    return;
                }
                int? eqId = View.EquipoSeleccionado;
                Guid? tecId = View.TecnicoSeleccionado;
                
                var categoria = _categoriaBLL.ObtenerCategorias().FirstOrDefault(c => c.Id == catId.Value);
                var ubicacion = _ubicacionBLL.ObtenerUbicaciones().FirstOrDefault(u => u.Id == ubiId.Value);
                var prioridad = _prioridadBLL.ObtenerPorId(prioId.Value);
                Entity.Domain.Usuario creadorUsuario = null;
                Entity.Domain.Usuario tecnicoAsignado = null;
                EquipoInformatico equipoAsignado = null;
                using (var uow = UnitOfWorkFactory.Current.Create())
                {
                    creadorUsuario = uow.Repositories.UsuarioRepository.GetById(_usuarioIdCorriente);
                    if (eqId.HasValue && eqId.Value != 0)
                    {
                        equipoAsignado = _equipoBLL.BuscarPorId(eqId.Value);
                    }
                    if (tecId.HasValue && tecId.Value != Guid.Empty)
                    {
                        tecnicoAsignado = uow.Repositories.UsuarioRepository.GetById(tecId.Value);
                    }
                }
                var nuevoTicket = new Ticket
                {
                    Id = Guid.NewGuid(),
                    Titulo = titulo,
                    Descripcion = desc,
                    FechaApertura = DateTime.Now,
                    Categoria = categoria,
                    Estado = estadoNuevo,
                    Ubicacion = ubicacion,
                    Prioridad = prioridad,
                    CreadorUsuario = creadorUsuario,
                    EquipoAsignado = equipoAsignado,
                    TecnicoAsignado = tecnicoAsignado
                };
                bool ok = _ticketBLL.CrearTicket(nuevoTicket, out string mensaje);
                if (ok)
                {
                    View.CerrarPantallaExito(mensaje);
                }
                else
                {
                    View.MostrarError(mensaje);
                }
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                View.MostrarError(ex.Message, LanguageBLL.Translate("Ticket_Error_Negocio_Titulo"));
            }
            catch (Exception ex)
            {
                View.MostrarError(string.Format(LanguageBLL.Translate("Ticket_Error_CrearTicket"), ex.Message));
            }
        }
    }
}
