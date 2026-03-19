using BLL.Helpers;
using BLL.Interfaces;
using DAL.Contracts;
using DAL.Contracts.UnitOfWork;
using Entity;
using Entity.Domain;
using Services;
using Services.DomainModel;
using Services.Implementations;
using Services.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace BLL.Implementations
{
    public class AdjuntoBLL : IAdjuntoBLL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _loggerService;
        private readonly IExceptionService _exceptionService;
        private readonly AuditHelper _auditHelper;
        public AdjuntoBLL(IUnitOfWork uow, ILoggerService loggerService = null, IExceptionService exceptionService = null, AuditHelper auditHelper = null)
        {
            _unitOfWork = uow;
            _loggerService = loggerService ?? LoggerService.Instance;
            _exceptionService = exceptionService ?? ExceptionService.Current;
            _auditHelper = auditHelper ?? new AuditHelper();
        }
        public bool AgregarAdjunto(AdjuntoTicket adjunto, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                if (string.IsNullOrWhiteSpace(adjunto.NombreArchivo))
                {
                    mensaje = "El nombre del archivo no puede estar vacio.";
                    return false;
                }
                if (string.IsNullOrWhiteSpace(adjunto.Ruta))
                {
                    mensaje = "La ruta del archivo no puede estar vacia.";
                    return false;
                }
                using (var uow = _unitOfWork.Create())
                {
                    if (adjunto.Ticket == null || adjunto.Ticket.Id == Guid.Empty)
                    {
                        mensaje = "El ticket especificado no es v�lido.";
                        return false;
                    }
                    var ticket = uow.Repositories.TicketRepository.GetById(adjunto.Ticket.Id);
                    if (ticket == null)
                    {
                        mensaje = "El ticket especificado no existe.";
                        return false;
                    }
                    string[] extensionesProhibidas = { ".exe", ".bat", ".cmd", ".com", ".scr", ".vbs", ".js" };
                    if (Array.Exists(extensionesProhibidas, ext => ext.Equals(adjunto.Extension, StringComparison.OrdinalIgnoreCase)))
                    {
                        mensaje = "El tipo de archivo no esta permitido por razones de seguridad.";
                        return false;
                    }
                    adjunto.Id = Guid.NewGuid();
                    adjunto.Ticket = ticket;
                    if (adjunto.Usuario == null || adjunto.Usuario.IdUsuario == Guid.Empty)
                    {
                        var usuarioActualSesion = SessionService.GetUsuario();
                        if (usuarioActualSesion != null)
                        {
                            adjunto.Usuario = uow.Repositories.UsuarioRepository.GetById(usuarioActualSesion.IdUsuario);
                        }
                    }
                    uow.Repositories.AdjuntoTicketRepository.Add(adjunto);
                    uow.SaveChanges();
                    _auditHelper.RegistrarExito("AGREGAR_ADADJUNTO",
                        $"Se agrego el archivo '{adjunto.NombreArchivo}' al ticket {adjunto.Ticket.Id}.", null, uow);
                    mensaje = "Adjunto agregado correctamente.";
                    return true;
                }
            }
            catch (Exception ex)
            {
                var context = new ExceptionContext { OperationName = "AdjuntoBLL" };
                _exceptionService.Handle(ex, context);
                mensaje = "Error al agregar el adjunto.";
                return false;
            }
        }
        public List<AdjuntoTicket> ObtenerAdjuntosPorTicket(Guid ticketId)
        {
            using (var uow = _unitOfWork.Create())
            {
                return uow.Repositories.AdjuntoTicketRepository.GetByTicketId(ticketId).ToList();
            }
        }
        public AdjuntoTicket ObtenerPorId(Guid id)
        {
            using (var uow = _unitOfWork.Create())
            {
                return uow.Repositories.AdjuntoTicketRepository.GetById(id);
            }
        }
        public bool EliminarAdjunto(Guid adjuntoId, bool eliminarArchivo, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                using (var uow = _unitOfWork.Create())
                {
                    var adjunto = uow.Repositories.AdjuntoTicketRepository.GetById(adjuntoId);
                    if (adjunto == null)
                    {
                        mensaje = "El adjunto no existe.";
                        return false;
                    }
                    string rutaArchivo = adjunto.Ruta;
                    string nombreArchivo = adjunto.NombreArchivo;
                    Guid ticketId = adjunto.Ticket?.Id ?? Guid.Empty;
                    uow.Repositories.AdjuntoTicketRepository.Remove(adjunto);
                    uow.SaveChanges();
                    if (eliminarArchivo && !string.IsNullOrEmpty(rutaArchivo))
                    {
                        try
                        {
                            if (File.Exists(rutaArchivo))
                            {
                                File.Delete(rutaArchivo);
                            }
                        }
                        catch (Exception exFile)
                        {
                            var contextFile = new ExceptionContext { OperationName = "AdjuntoBLL" };
                            _exceptionService.Handle(exFile, contextFile);
                        }
                    }
                    _auditHelper.RegistrarExito("ELIMINAR_ADJUNTO",
                        $"Se elimino el adjunto '{nombreArchivo}' del ticket {ticketId}.", null, uow);
                    mensaje = "Adjunto eliminado correctamente.";
                    return true;
                }
            }
            catch (Exception ex)
            {
                var context = new ExceptionContext { OperationName = "AdjuntoBLL" };
                _exceptionService.Handle(ex, context);
                mensaje = "Error al eliminar el adjunto.";
                return false;
            }
        }
    }
}
