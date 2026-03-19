using BLL.Exceptions;
using BLL.Helpers;
using BLL.Implementations;
using BLL.Interfaces;
using DAL.Contracts;
using DAL.Contracts.UnitOfWork;
using Entity.Domain;
using Services;
using Services.BLL;
using Services.DomainModel;
using Services.Services;
using Services.Templates;
using System;
using System.Linq;
using Services.Implementations;
namespace BLL.TicketServices
{
    internal class TicketCreationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExceptionService _exceptionService;
        private readonly AuditHelper _auditHelper;
        private readonly EmailHelper _emailHelper;
        private readonly ISLABLL _slaBll;
        public TicketCreationService(
            IUnitOfWork unitOfWork,
            IExceptionService exceptionService,
            AuditHelper auditHelper,
            EmailHelper emailHelper,
            ISLABLL slaBll)
        {
            _unitOfWork = unitOfWork;
            _exceptionService = exceptionService;
            _auditHelper = auditHelper;
            _emailHelper = emailHelper;
            _slaBll = slaBll;
        }
        public bool CrearTicket(Entity.Domain.Ticket ticket, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                ValidarTicket(ticket);
                using (var uow = _unitOfWork.Create())
                {
                    PrepararTicketParaCreacion(ticket, uow);
                    uow.Repositories.TicketRepository.Add(ticket);
                    ActualizarIntegridadSinCommit(uow, ticket);
                    mensaje = "Ticket creado correctamente.";
                    EnviarNotificacionCreacion(ticket, uow);
                    RegistrarAuditoriaCreacion(ticket, uow);
                    uow.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                var context = new ExceptionContext { OperationName = "CrearTicket" };
                _exceptionService.Handle(ex, context);
                mensaje = $"Error al crear el ticket: {ex.Message}";
                return false;
            }
        }
        private void PrepararTicketParaCreacion(Entity.Domain.Ticket ticket, IUnitOfWorkAdapter uow)
        {
            ticket.FechaApertura = DateTime.Now;
            AsignarDefaults(ticket, uow);
            CalcularSLA(ticket, uow);
        }
        private void RegistrarAuditoriaCreacion(Entity.Domain.Ticket ticket, IUnitOfWorkAdapter uow)
        {
            _auditHelper.RegistrarExito("CREACION_TICKET",
                $"El usuario '{_auditHelper.ObtenerUsuarioActual()}' creo el ticket '{ticket.Id}' con exito.", null, uow);
        }
        private void ActualizarIntegridadSinCommit(IUnitOfWorkAdapter uow, Entity.Domain.Ticket ticket)
        {
            var ticketDb = uow.Repositories.TicketRepository.GetById(ticket.Id);
            var adapter = new TicketIntegridadAdapter(ticketDb);
            var cadena = IntegrityService.ConstruirCadena(adapter);
            var hash = IntegrityService.CrearHash(cadena);
            ticket.IntegridadHash = hash;
            uow.Repositories.TicketRepository.UpdateHash(ticket.Id, hash);
        }
        private void ValidarTicket(Entity.Domain.Ticket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException(nameof(ticket), LanguageBLL.Translate("Ticket_Error_TicketNulo"));
            if (string.IsNullOrWhiteSpace(ticket.Titulo))
                throw new ArgumentException(LanguageBLL.Translate("Ticket_Error_TituloObligatorio"), nameof(ticket.Titulo));
            if (string.IsNullOrWhiteSpace(ticket.Descripcion))
                throw new ArgumentException(LanguageBLL.Translate("Ticket_Error_DescripcionObligatoria"), nameof(ticket.Descripcion));
            if (ticket.CreadorUsuario == null || ticket.CreadorUsuario.IdUsuario == Guid.Empty)
                throw new ArgumentException(LanguageBLL.Translate("Ticket_Error_CreadorObligatorio"), nameof(ticket.CreadorUsuario));
        }
        private void AsignarDefaults(Entity.Domain.Ticket ticket, IUnitOfWorkAdapter uow)
        {
            if (ticket.Prioridad == null)
            {
                var prioridadDefault = uow.Repositories.PrioridadTicketRepository.GetByNombre("Media")
                    ?? uow.Repositories.PrioridadTicketRepository.GetByNombre("Normal")
                    ?? uow.Repositories.PrioridadTicketRepository.GetAll().FirstOrDefault();
                if (prioridadDefault == null)
                    throw new BLLException(LanguageBLL.Translate("Ticket_Error_NoPrioridades"));
                ticket.Prioridad = prioridadDefault;
            }
            if (ticket.Estado == null)
            {
                var estadoNuevo = uow.Repositories.EstadoTicketRepository.GetByNombre("Nuevo")
                    ?? uow.Repositories.EstadoTicketRepository.GetByNombre("Abierto")
                    ?? uow.Repositories.EstadoTicketRepository.GetAll().FirstOrDefault();
                if (estadoNuevo == null)
                    throw new BLLException(LanguageBLL.Translate("Ticket_Error_NoEstados"));
                ticket.Estado = estadoNuevo;
            }
        }
        private void CalcularSLA(Entity.Domain.Ticket ticket, IUnitOfWorkAdapter uow)
        {
            
            SLAResolver.ResolverYAsignarSLA(ticket, uow, _slaBll);
        }
        private void EnviarNotificacionCreacion(Entity.Domain.Ticket ticket, IUnitOfWorkAdapter uow)
        {
            var estadoNombre = "Nuevo";
            
            if (ticket.Estado != null)
            {
                if (string.IsNullOrEmpty(ticket.Estado.Nombre))
                {
                    
                    var estadoEntity = uow.Repositories.EstadoTicketRepository.GetById(ticket.Estado.Id);
                    if (estadoEntity != null)
                        estadoNombre = estadoEntity.Nombre;
                }
                else
                {
                    
                    estadoNombre = ticket.Estado.Nombre;
                }
            }
            string cuerpo = TicketCreadoTemplate.Generar(
                SessionService.GetUsuario()?.UserName ?? "Usuario",
                ticket.Titulo,
                ticket.Descripcion,
                ticket.FechaApertura,
                estadoNombre,
                ticket.Id
            );
            if (ticket.CreadorUsuario != null)
            {
                _emailHelper.EnviarNotificacionSiPosible(
                    ticket.CreadorUsuario.IdUsuario,
                    $"Nuevo ticket creado #{ticket.Id}",
                    cuerpo);
            }
        }    }
}
