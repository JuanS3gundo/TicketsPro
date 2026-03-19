using BLL.Implementations;
using BLL.Interfaces;
using DAL.Contracts;
using DAL.Contracts.UnitOfWork;
using Entity.Domain;
using Services;
using Services.BLL;
using Services.DomainModel;
using Services.Services;
using System;
using System.Collections.Generic;
using BLL.Exceptions;
using BLL.Helpers;
namespace BLL.TicketServices
{
    internal class TicketUpdateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExceptionService _exceptionService;
        private readonly AuditHelper _auditHelper;
        private readonly ISLABLL _slaBll;
        public TicketUpdateService(
            IUnitOfWork unitOfWork,
            IExceptionService exceptionService,
            AuditHelper auditHelper,
            ISLABLL slaBll)
        {
            _unitOfWork = unitOfWork;
            _exceptionService = exceptionService;
            _auditHelper = auditHelper;
            _slaBll = slaBll;
        }
        public bool ActualizarTicket(Entity.Domain.Ticket ticket, out string mensaje)
        {
            using (var uow = _unitOfWork.Create())
            {
                bool result = ActualizarTicketTransaccional(ticket, uow, out mensaje);
                if (result)
                {
                    uow.SaveChanges();
                }
                return result;
            }
        }
        public bool ActualizarTicketTransaccional(Entity.Domain.Ticket ticket, IUnitOfWorkAdapter uow, out string mensaje)
        {
            mensaje = "";
            if (ticket == null)
            {
                mensaje = LanguageBLL.Translate("Ticket_Update_TicketNulo");
                return false;
            }
            try
            {
                var repo = uow.Repositories.TicketRepository;
                var original = repo.GetById(ticket.Id);
                if (original == null)
                {
                    mensaje = LanguageBLL.Translate("Ticket_Update_TicketNoExiste");
                    return false;
                }
                AplicarCambiosBasicos(original, ticket);
                var prioridadAnteriorId = original.Prioridad?.Id;
                var estadoAnteriorId = original.Estado?.Id;
                var estadoCambiado = EstadoCambiado(estadoAnteriorId, ticket.Estado?.Id);
                if (estadoCambiado)
                {
                    var estadoAnteriorObj = ResolverEstadoCompleto(original.Estado, estadoAnteriorId, uow);
                    var estadoNuevoObj = ResolverEstadoCompleto(ticket.Estado, ticket.Estado?.Id, uow);
                    TicketEstadoValidator.ValidarYLanzarSiInvalida(estadoAnteriorObj?.Nombre, estadoNuevoObj?.Nombre);
                }
                original.Estado = ticket.Estado;
                original.Prioridad = ticket.Prioridad;
                RecalcularSLASiNecesario(original, prioridadAnteriorId, uow);
                TrackPrimeraRespuesta(original);
                repo.Update(original);
                ActualizarIntegridad(original, uow);
                _auditHelper.RegistrarExito("ACTUALIZAR_TICKET",
                    string.Format(LanguageBLL.Translate("Ticket_Update_ActualizadoOk"), ticket.Id), null, uow);
                return true;
            }
            catch (Exception ex)
            {
                var context = new ExceptionContext { OperationName = "ActualizarTicket" };
                _exceptionService.Handle(ex, context);
                mensaje = ex.Message;
                return false;
            }
        }
        public void AsignarTecnico(Guid ticketId, Guid tecnicoId)
        {
            using (var uow = _unitOfWork.Create())
            {
                var ticket = uow.Repositories.TicketRepository.GetById(ticketId);
                if (ticket == null)
                    throw new BLLException(LanguageBLL.Translate("Ticket_Error_NoExiste"));
                
                var tecnico = uow.Repositories.UsuarioRepository.GetById(tecnicoId);
                if (tecnico == null)
                    throw new BLLException(LanguageBLL.Translate("Ticket_Error_TecnicoNoExiste"));
                ticket.TecnicoAsignado = tecnico;
                if (!ActualizarTicketTransaccional(ticket, uow, out string mensaje))
                    throw new BLLException(mensaje);
                uow.SaveChanges();
            }
        }
        public void CambiarEstado(Guid ticketId, Guid nuevoEstadoId)
        {
            using (var uow = _unitOfWork.Create())
            {
                var ticket = uow.Repositories.TicketRepository.GetById(ticketId);
                if (ticket == null)
                    throw new BLLException(LanguageBLL.Translate("Ticket_Error_NoExiste"));
                
                var nuevoEstado = uow.Repositories.EstadoTicketRepository.GetById(nuevoEstadoId);
                if (nuevoEstado == null)
                    throw new BLLException(LanguageBLL.Translate("Ticket_Error_EstadoNoExiste"));
                
                TicketEstadoValidator.ValidarYLanzarSiInvalida(ticket.Estado?.Nombre, nuevoEstado.Nombre);
                ticket.Estado = nuevoEstado;
                if (!ActualizarTicketTransaccional(ticket, uow, out string mensaje))
                    throw new BLLException(mensaje);
                uow.SaveChanges();
            }
        }
        private void AplicarCambiosBasicos(Entity.Domain.Ticket original, Entity.Domain.Ticket ticket)
        {
            original.Titulo = ticket.Titulo;
            original.Descripcion = ticket.Descripcion;
            original.Categoria = ticket.Categoria;
            original.Ubicacion = ticket.Ubicacion;
            original.TecnicoAsignado = ticket.TecnicoAsignado;
            original.EquipoAsignado = ticket.EquipoAsignado;
            original.FechaUltModificacion = DateTime.Now;
        }
        private static bool EstadoCambiado(Guid? estadoAnteriorId, Guid? estadoNuevoId)
        {
            return estadoAnteriorId != estadoNuevoId;
        }
        private static EstadoTicket ResolverEstadoCompleto(EstadoTicket estado, Guid? estadoId, IUnitOfWorkAdapter uow)
        {
            if (estado == null)
                return null;
            if (string.IsNullOrEmpty(estado.Nombre) && estadoId.HasValue)
                return uow.Repositories.EstadoTicketRepository.GetById(estadoId.Value);
            return estado;
        }
        private void ActualizarIntegridad(Entity.Domain.Ticket original, IUnitOfWorkAdapter uow)
        {
            var ticketDb = uow.Repositories.TicketRepository.GetById(original.Id);
            var adapter = new TicketIntegridadAdapter(ticketDb);
            string cadena = IntegrityService.ConstruirCadena(adapter);
            string nuevoHash = IntegrityService.CrearHash(cadena);
            original.IntegridadHash = nuevoHash;
            uow.Repositories.TicketRepository.UpdateHash(original.Id, nuevoHash);
        }
        private void RecalcularSLASiNecesario(Entity.Domain.Ticket original, Guid? prioridadAnteriorId, IUnitOfWorkAdapter uow)
        {
            var prioridadActualId = original.Prioridad?.Id;
            
            if (prioridadAnteriorId != prioridadActualId && prioridadActualId.HasValue)
            {
                SLAResolver.ResolverYAsignarSLA(original, uow, _slaBll);
            }
        }
        private void TrackPrimeraRespuesta(Entity.Domain.Ticket original)
        {
            if (original.FechaPrimeraRespuesta == null && original.TecnicoAsignado != null)
            {
                original.FechaPrimeraRespuesta = DateTime.Now;
            }
        }
    }
}
