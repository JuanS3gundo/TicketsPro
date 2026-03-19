using BLL.Exceptions;
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
using System.Data.SqlClient;
using System.Linq;
namespace BLL.Implementations
{
    public class EstadoTicketBLL : IEstadoTicketBLL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _loggerService;
        private readonly IExceptionService _exceptionService;
        private readonly AuditHelper _auditHelper;
        public EstadoTicketBLL(IUnitOfWork uow, ILoggerService loggerService = null, IExceptionService exceptionService = null, AuditHelper auditHelper = null)
        {
            _unitOfWork = uow;
            _loggerService = loggerService ?? LoggerService.Instance;
            _exceptionService = exceptionService ?? Services.Services.ExceptionService.Current;
            _auditHelper = auditHelper ?? new AuditHelper();
        }
        public List<EstadoTicket> ObtenerEstados()
        {
            using (var uow = _unitOfWork.Create())
            {
                return uow.Repositories.EstadoTicketRepository.GetAll().ToList();
            }
        }
        public bool CrearEstado(EstadoTicket estado, out string mensaje)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(estado.Nombre))
                {
                    mensaje = "El nombre del estado es obligatorio.";
                    return false;
                }
                using (var uow = _unitOfWork.Create())
                {
                    var existente = uow.Repositories.EstadoTicketRepository.GetByNombre(estado.Nombre);
                    if (existente != null)
                    {
                        mensaje = $"Ya existe un estado con el nombre '{estado.Nombre}'.";
                        return false;
                    }
                    if (estado.Id == Guid.Empty)
                        estado.Id = Guid.NewGuid();
                    uow.Repositories.EstadoTicketRepository.Add(estado);
                    uow.SaveChanges();
                    _auditHelper.RegistrarExito("CREACION_ESTADO_TICKET",
                        $"Se creo el estado de ticket '{estado.Nombre}' (ID: {estado.Id})", null, uow);
                    mensaje = "Estado creado exitosamente.";
                    return true;
                }
            }
            catch (Exception ex)
            {
                var context = new ExceptionContext { OperationName = "EstadoTicketBLL.CrearEstado" };
                _exceptionService.Handle(ex, context);
                mensaje = $"Error al crear estado: {ex.Message}";
                return false;
            }
        }
        public bool ActualizarEstado(Guid id, string nuevoNombre, out string mensaje)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nuevoNombre))
                {
                    mensaje = "El nuevo nombre es obligatorio.";
                    return false;
                }
                using (var uow = _unitOfWork.Create())
                {
                    var estado = uow.Repositories.EstadoTicketRepository.GetById(id);
                    if (estado == null)
                    {
                        mensaje = "Estado no encontrado.";
                        return false;
                    }
                    var existente = uow.Repositories.EstadoTicketRepository.GetByNombre(nuevoNombre);
                    if (existente != null && existente.Id != id)
                    {
                        mensaje = $"Ya existe otro estado con el nombre '{nuevoNombre}'.";
                        return false;
                    }
                    string nombreAnterior = estado.Nombre;
                    estado.Nombre = nuevoNombre.Trim();
                    uow.Repositories.EstadoTicketRepository.Update(estado);
                    uow.SaveChanges();
                    _auditHelper.RegistrarExito("ACTUALIZACION_ESTADO_TICKET",
                        $"Se actualizo el estado de ticket de '{nombreAnterior}' a '{nuevoNombre}' (ID: {id})", null, uow);
                    mensaje = "Estado actualizado exitosamente.";
                    return true;
                }
            }
            catch (Exception ex)
            {
                var context = new ExceptionContext { OperationName = "EstadoTicketBLL.ActualizarEstado" };
                _exceptionService.Handle(ex, context);
                mensaje = $"Error al actualizar estado: {ex.Message}";
                return false;
            }
        }
        public bool EliminarEstado(Guid id, out string mensaje)
        {
            try
            {
                using (var uow = _unitOfWork.Create())
                {
                    var estado = uow.Repositories.EstadoTicketRepository.GetById(id);
                    if (estado == null)
                    {
                        mensaje = "Estado no encontrado.";
                        return false;
                    }
                    string nombreEstado = estado.Nombre;
                    uow.Repositories.EstadoTicketRepository.Remove(estado);
                    uow.SaveChanges();
                    _auditHelper.RegistrarExito("ELIMINACION_ESTADO_TICKET",
                        $"Se elimino el estado de ticket '{nombreEstado}' (ID: {id})", null, uow);
                    mensaje = "Estado eliminado exitosamente.";
                    return true;
                }
            }
            catch (SqlException sqlEx) when (sqlEx.Number == 547)
            {
                var context = new ExceptionContext { OperationName = "EstadoTicketBLL.EliminarEstado" };
                _exceptionService.Handle(sqlEx, context);
                mensaje = "No se puede eliminar el estado porque esta siendo utilizado por uno o mas tickets.";
                return false;
            }
            catch (Exception ex)
            {
                var context = new ExceptionContext { OperationName = "EstadoTicketBLL.EliminarEstado" };
                _exceptionService.Handle(ex, context);
                mensaje = $"Error al eliminar estado: {ex.Message}";
                return false;
            }
        }
    }
}
