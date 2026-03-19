using BLL.Helpers;
using BLL.Interfaces;
using DAL.Contracts;
using DAL.Contracts.UnitOfWork;
using Entity;
using Entity.Domain;
using Services;
using Services.DomainModel;
using Services.Facade;
using Services.Implementations;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
namespace BLL.Implementations
{
    public class PrioridadBLL : IPrioridadBLL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _loggerService;
        private readonly IExceptionService _exceptionService;
        private readonly AuditHelper _auditHelper;
        public PrioridadBLL(IUnitOfWork uow, ILoggerService loggerService = null, IExceptionService exceptionService = null, AuditHelper auditHelper = null)
        {
            _unitOfWork = uow;
            _loggerService = loggerService ?? LoggerService.Instance;
            _exceptionService = exceptionService ?? Services.Services.ExceptionService.Current;
            _auditHelper = auditHelper ?? new AuditHelper();
        }
        public List<PrioridadTicket> ObtenerTodas()
        {
            using (var uow = _unitOfWork.Create())
            {
                return uow.Repositories.PrioridadTicketRepository.GetAll()
                    .OrderBy(p => p.NivelPeso)
                    .ToList();
            }
        }
        public PrioridadTicket ObtenerPorId(Guid id)
        {
            using (var uow = _unitOfWork.Create())
            {
                return uow.Repositories.PrioridadTicketRepository.GetById(id);
            }
        }
        public PrioridadTicket ObtenerPorNombre(string nombre)
        {
            using (var uow = _unitOfWork.Create())
            {
                return uow.Repositories.PrioridadTicketRepository.GetByNombre(nombre);
            }
        }
        public bool Crear(PrioridadTicket prioridad, out string mensaje)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(prioridad.Nombre))
                {
                    mensaje = "El nombre de la prioridad es obligatorio.";
                    return false;
                }
                using (var uow = _unitOfWork.Create())
                {
                    var existente = uow.Repositories.PrioridadTicketRepository.GetByNombre(prioridad.Nombre);
                    if (existente != null)
                    {
                        mensaje = $"Ya existe una prioridad con el nombre '{prioridad.Nombre}'.";
                        return false;
                    }
                    if (prioridad.Id == Guid.Empty)
                        prioridad.Id = Guid.NewGuid();
                    uow.Repositories.PrioridadTicketRepository.Add(prioridad);
                    uow.SaveChanges();
                    _auditHelper.RegistrarExito("CREACION_PRIORIDAD",
                        $"Se creo la prioridad '{prioridad.Nombre}' con peso {prioridad.NivelPeso} (ID: {prioridad.Id})", null, uow);
                    mensaje = "Prioridad creada exitosamente.";
                    return true;
                }
            }
            catch (Exception ex)
            {
                var context = new ExceptionContext { OperationName = "PrioridadBLL.Crear" };
                _exceptionService.Handle(ex, context);
                mensaje = $"Error al crear prioridad: {ex.Message}";
                return false;
            }
        }
        public bool Actualizar(Guid id, string nuevoNombre, out string mensaje)
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
                    var prioridad = uow.Repositories.PrioridadTicketRepository.GetById(id);
                    if (prioridad == null)
                    {
                        mensaje = "Prioridad no encontrada.";
                        return false;
                    }
                    var existente = uow.Repositories.PrioridadTicketRepository.GetByNombre(nuevoNombre);
                    if (existente != null && existente.Id != id)
                    {
                        mensaje = $"Ya existe otra prioridad con el nombre '{nuevoNombre}'.";
                        return false;
                    }
                    string nombreAnterior = prioridad.Nombre;
                    prioridad.Nombre = nuevoNombre.Trim();
                    uow.Repositories.PrioridadTicketRepository.Update(prioridad);
                    uow.SaveChanges();
                    _auditHelper.RegistrarExito("ACTUALIZACION_PRIORIDAD",
                        $"Se actualizo la prioridad de '{nombreAnterior}' a '{nuevoNombre}' (ID: {id})", null, uow);
                    mensaje = "Prioridad actualizada exitosamente.";
                    return true;
                }
            }
            catch (Exception ex)
            {
                var context = new ExceptionContext { OperationName = "PrioridadBLL.Actualizar" };
                _exceptionService.Handle(ex, context);
                mensaje = $"Error al actualizar prioridad: {ex.Message}";
                return false;
            }
        }
        public bool Eliminar(Guid id, out string mensaje)
        {
            try
            {
                using (var uow = _unitOfWork.Create())
                {
                    var prioridad = uow.Repositories.PrioridadTicketRepository.GetById(id);
                    if (prioridad == null)
                    {
                        mensaje = "Prioridad no encontrada.";
                        return false;
                    }
                    string nombrePrioridad = prioridad.Nombre;
                    uow.Repositories.PrioridadTicketRepository.Remove(prioridad);
                    uow.SaveChanges();
                    _auditHelper.RegistrarExito("ELIMINACION_PRIORIDAD",
                        $"Se elimino la prioridad '{nombrePrioridad}' (ID: {id})", null, uow);
                    mensaje = "Prioridad eliminada exitosamente.";
                    return true;
                }
            }
            catch (SqlException sqlEx) when (sqlEx.Number == 547)
            {
                var context = new ExceptionContext { OperationName = "PrioridadBLL.Eliminar" };
                _exceptionService.Handle(sqlEx, context);
                mensaje = "No se puede eliminar la prioridad porque esta siendo utilizada por uno o mas tickets.";
                return false;
            }
            catch (Exception ex)
            {
                var context = new ExceptionContext { OperationName = "PrioridadBLL.Eliminar" };
                _exceptionService.Handle(ex, context);
                mensaje = $"Error al eliminar prioridad: {ex.Message}";
                return false;
            }
        }
    }
}
