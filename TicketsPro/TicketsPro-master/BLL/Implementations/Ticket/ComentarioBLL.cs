using BLL.Helpers;
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
using System;
using System.Collections.Generic;
using System.Linq;
namespace BLL.Implementations
{
    public class ComentarioBLL : IComentarioBLL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _loggerService;
        private readonly IExceptionService _exceptionService;
        private readonly AuditHelper _auditHelper;
        public ComentarioBLL(IUnitOfWork uow, ILoggerService loggerService = null, IExceptionService exceptionService = null, AuditHelper auditHelper = null)
        {
            _unitOfWork = uow;
            _loggerService = loggerService ?? LoggerService.Instance;
            _exceptionService = exceptionService ?? Services.Services.ExceptionService.Current;
            _auditHelper = auditHelper ?? new AuditHelper();
        }
        public bool AgregarComentario(ComentarioTicket comentario, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                if (!ValidarComentarioBasico(comentario, out mensaje))
                    return false;
                using (var uow = _unitOfWork.Create())
                {
                    var ticket = ObtenerTicketValido(uow, comentario.Ticket, out mensaje);
                    if (ticket == null)
                        return false;
                    comentario.Id = Guid.NewGuid();
                    comentario.Fecha = DateTime.Now;
                    comentario.Ticket = ticket;
                    if (comentario.Usuario == null || comentario.Usuario.IdUsuario == Guid.Empty)
                    {
                        var usuarioActualSesion = SessionService.GetUsuario();
                        if (usuarioActualSesion != null)
                        {
                            comentario.Usuario = uow.Repositories.UsuarioRepository.GetById(usuarioActualSesion.IdUsuario);
                        }
                    }
                    uow.Repositories.ComentarioTicketRepository.Add(comentario);
                    uow.SaveChanges();
                    RegistrarAuditoriaExito("AGREGAR_COMENTARIO",
                        $"Se agrego un comentario {(comentario.EsInterno ? "interno" : "publico")} al ticket {comentario.Ticket.Id}.", uow);
                    mensaje = LanguageBLL.Translate("Comentario_Exito_Agregado");
                    return true;
                }
            }
            catch (Exception ex)
            {
                return HandleException("ComentarioBLL", LanguageBLL.Translate("Comentario_Error_Agregar"), ex, out mensaje);
            }
        }
        public List<ComentarioTicket> ObtenerComentariosPorTicket(Guid ticketId)
        {
            using (var uow = _unitOfWork.Create())
            {
                return uow.Repositories.ComentarioTicketRepository.GetByTicketId(ticketId).ToList();
            }
        }
        public List<ComentarioTicket> ObtenerComentariosPublicos(Guid ticketId)
        {
            using (var uow = _unitOfWork.Create())
            {
                return uow.Repositories.ComentarioTicketRepository.GetComentariosPublicosByTicketId(ticketId).ToList();
            }
        }
        public ComentarioTicket ObtenerPorId(Guid id)
        {
            using (var uow = _unitOfWork.Create())
            {
                return uow.Repositories.ComentarioTicketRepository.GetById(id);
            }
        }
        public bool ActualizarComentario(ComentarioTicket comentario, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                if (!ValidarComentarioBasico(comentario, out mensaje))
                    return false;
                using (var uow = _unitOfWork.Create())
                {
                    var existente = uow.Repositories.ComentarioTicketRepository.GetById(comentario.Id);
                    if (existente == null)
                    {
                        mensaje = LanguageBLL.Translate("Comentario_Error_NoExiste");
                        return false;
                    }
                    uow.Repositories.ComentarioTicketRepository.Update(comentario);
                    uow.SaveChanges();
                    RegistrarAuditoriaExito("ACTUALIZAR_COMENTARIO",
                        $"Se actualizo el comentario {comentario.Id} del ticket {comentario.Ticket?.Id}.", uow);
                    mensaje = LanguageBLL.Translate("Comentario_Exito_Actualizado");
                    return true;
                }
            }
            catch (Exception ex)
            {
                return HandleException("ComentarioBLL", LanguageBLL.Translate("Comentario_Error_Actualizar"), ex, out mensaje);
            }
        }
        public bool EliminarComentario(Guid comentarioId, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                using (var uow = _unitOfWork.Create())
                {
                    var comentario = uow.Repositories.ComentarioTicketRepository.GetById(comentarioId);
                    if (comentario == null)
                    {
                        mensaje = LanguageBLL.Translate("Comentario_Error_NoExiste");
                        return false;
                    }
                    uow.Repositories.ComentarioTicketRepository.Remove(comentario);
                    uow.SaveChanges();
                    RegistrarAuditoriaExito("ELIMINAR_COMENTARIO",
                        $"Se elimino el comentario {comentarioId} del ticket {comentario.Ticket?.Id}.", uow);
                    mensaje = LanguageBLL.Translate("Comentario_Exito_Eliminado");
                    return true;
                }
            }
            catch (Exception ex)
            {
                return HandleException("ComentarioBLL", LanguageBLL.Translate("Comentario_Error_Eliminar"), ex, out mensaje);
            }
        }
        private bool ValidarComentarioBasico(ComentarioTicket comentario, out string mensaje)
        {
            mensaje = string.Empty;
            if (string.IsNullOrWhiteSpace(comentario.Mensaje))
            {
                mensaje = LanguageBLL.Translate("Comentario_Error_ContenidoVacio");
                return false;
            }
            return true;
        }
        private Ticket ObtenerTicketValido(IUnitOfWorkAdapter uow, Ticket ticketRef, out string mensaje)
        {
            mensaje = string.Empty;
            if (ticketRef == null || ticketRef.Id == Guid.Empty)
            {
                mensaje = LanguageBLL.Translate("Comentario_Error_TicketInvalido");
                return null;
            }
            var ticket = uow.Repositories.TicketRepository.GetById(ticketRef.Id);
            if (ticket == null)
            {
                mensaje = LanguageBLL.Translate("Comentario_Error_TicketNoExiste");
                return null;
            }
            return ticket;
        }
        private void RegistrarAuditoriaExito(string accion, string detalle, IUnitOfWorkAdapter uow)
        {
            _auditHelper.RegistrarExito(accion, detalle, null, uow);
        }
        private bool HandleException(string operationName, string mensajeError, Exception ex, out string mensaje)
        {
            var context = new ExceptionContext { OperationName = operationName };
            _exceptionService.Handle(ex, context);
            mensaje = mensajeError;
            return false;
        }
    }
}
