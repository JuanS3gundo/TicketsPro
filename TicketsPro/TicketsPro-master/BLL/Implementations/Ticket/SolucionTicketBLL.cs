using BLL.Interfaces;
using DAL.Contracts;
using DAL.Contracts.UnitOfWork;
using Entity;
using Entity.Domain;
using Services;
using Services.BLL;
using Services.DomainModel;
using Services.Implementations;
using Services.Services;
using Services.Templates;
using System;
using System.Collections.Generic;
using BLL.Exceptions;
using BLL.Helpers;
namespace BLL
{
    public class SolucionTicketBLL : ISolucionTicketBLL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _loggerService;
        private readonly IExceptionService _exceptionService;
        private readonly AuditHelper _auditHelper;
        public SolucionTicketBLL(IUnitOfWork uow, ILoggerService loggerService = null, IExceptionService exceptionService = null, AuditHelper auditHelper = null)
        {
            _unitOfWork = uow;
            _loggerService = loggerService ?? LoggerService.Instance;
            _exceptionService = exceptionService ?? Services.Services.ExceptionService.Current;
            _auditHelper = auditHelper ?? new AuditHelper();
        }
        public void AgregarSolucion(Guid ticketId, string descripcionSolucion, DAL.Contracts.UnitOfWork.IUnitOfWorkAdapter uowExterna = null)
        {
            if (string.IsNullOrWhiteSpace(descripcionSolucion))
                throw new BLLException(LanguageBLL.Translate("Ticket_Solucion_DescripcionVacia"));
            bool ownUow = false;
            var uow = uowExterna;
            if (uow == null)
            {
                uow = _unitOfWork.Create();
                ownUow = true;
            }
            try
            {
                var ticket = uow.Repositories.TicketRepository.GetById(ticketId);
                if (ticket == null)
                    throw new BLLException(LanguageBLL.Translate("Ticket_Error_TicketNoEncontrado"));
                var tecnicoActualSesion = SessionService.GetUsuario();
                if (tecnicoActualSesion == null)
                    throw new BLLException(LanguageBLL.Translate("Ticket_Solucion_TecnicoNoEnSesion"));
                
                var tecnicoActual = uow.Repositories.UsuarioRepository.GetById(tecnicoActualSesion.IdUsuario);
                if (tecnicoActual == null)
                    throw new BLLException(LanguageBLL.Translate("Ticket_Solucion_TecnicoNoExiste"));
                var solucionExistente = uow.Repositories.SolucionTicketRepository.GetByTicketId(ticketId);
                if (solucionExistente != null)
                {
                    var comentarioSolucion = new ComentarioTicket
                    {
                        Id = Guid.NewGuid(),
                        Ticket = ticket,
                        Usuario = tecnicoActual,
                        Mensaje = $" SOLUCION ADICIONAL:\n{descripcionSolucion}",
                        Fecha = DateTime.Now,
                        EsInterno = false 
                    };
                    uow.Repositories.ComentarioTicketRepository.Add(comentarioSolucion);
                }
                else
                {
                    var solucion = new SolucionTicket
                    {
                        Id = Guid.NewGuid(),
                        Ticket = ticket,
                        DescripcionSolucion = descripcionSolucion,
                        FechaCierre = DateTime.Now
                    };
                    uow.Repositories.SolucionTicketRepository.Add(solucion);
                }
                var estadoResuelto = uow.Repositories.EstadoTicketRepository.GetByNombre("Resuelto");
                if (estadoResuelto != null)
                {
                    ticket.Estado = estadoResuelto;
                }
                ticket.TecnicoAsignado = tecnicoActual;
                ticket.FechaCierre = DateTime.Now;
                var adapter = new TicketIntegridadAdapter(ticket);
                IntegrityService.RecalcularHash(adapter);
                ticket.IntegridadHash = adapter.IntegridadHash;
                uow.Repositories.TicketRepository.Update(ticket);
                uow.SaveChanges();
                string accion = solucionExistente != null ? "SOLUCION_ADICIONAL" : "SOLUCION_TICKET";
                string detalle = solucionExistente != null
                    ? $"El tecnico {tecnicoActual.UserName} agrego una solucion adicional al ticket {ticket.Id}."
                    : $"El tecnico {tecnicoActual.UserName} resolvio el ticket {ticket.Id}.";
                _auditHelper.RegistrarExito(accion, detalle, null, uow);
                
                var usuarioCreador = ticket.CreadorUsuario;
                if (usuarioCreador != null && usuarioCreador.IdUsuario != Guid.Empty)
                {
                    var usuarioServices = Services.Implementations.UsuarioRepository.Current.GetById(usuarioCreador.IdUsuario);
                    if (usuarioServices?.Email != null && !string.IsNullOrWhiteSpace(usuarioServices.Email))
                    {
                        try
                        {
                            if (IsValidEmail(usuarioServices.Email))
                            {
                                var emailService = new EmailService();
                                string asunto = $"Tu ticket #{ticket.Id} ha sido resuelto";
                                string cuerpo = TicketResueltoTemplate.Generar(
                                    usuarioServices.UserName,
                                    tecnicoActualSesion.UserName,
                                    ticket.Titulo,
                                    ticket.Descripcion,
                                    descripcionSolucion,
                                    DateTime.Now, 
                                    ticket.Id
                                );
                                emailService.EnviarEmail(usuarioServices.Email, asunto, cuerpo);
                            }
                            else
                            {
                                _loggerService.LogWarning($"No se envio email de resolucion: Email invalido para usuario {usuarioServices.UserName} (Email: '{usuarioServices.Email}')");
                            }
                        }
                        catch (Exception ex)
                        {
                            var context = new ExceptionContext { OperationName = "SolucionTicketBLL" };
                            _exceptionService.Handle(ex, context);
                        }
                    }
                }
                if (ownUow)
                {
                    uow.SaveChanges();
                }
            }
            finally
            {
                if (ownUow && uow != null)
                {
                    uow.Dispose();
                }
            }
        }
        public SolucionTicket ObtenerSolucionPorTicket(Guid ticketId)
        {
            using (var uow = _unitOfWork.Create())
            {
                return uow.Repositories.SolucionTicketRepository.GetByTicketId(ticketId);
            }
        }
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
