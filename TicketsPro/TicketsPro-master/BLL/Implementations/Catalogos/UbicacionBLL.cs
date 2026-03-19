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
    public class UbicacionBLL : IUbicacionBLL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _loggerService;
        private readonly IExceptionService _exceptionService;
        private readonly AuditHelper _auditHelper;
        public UbicacionBLL(IUnitOfWork uow, ILoggerService loggerService = null, IExceptionService exceptionService = null, AuditHelper auditHelper = null)
        {
            _unitOfWork = uow;
            _loggerService = loggerService ?? LoggerService.Instance;
            _exceptionService = exceptionService ?? Services.Services.ExceptionService.Current;
            _auditHelper = auditHelper ?? new AuditHelper();
        }
        public List<Ubicacion> ObtenerUbicaciones()
        {
            using (var uow = _unitOfWork.Create())
            {
                return uow.Repositories.UbicacionRepository.GetAll().ToList();
            }
        }
        public bool CrearUbicacion(Ubicacion ubicacion, out string mensaje)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ubicacion.Nombre))
                {
                    mensaje = "El nombre de la ubicacion es obligatorio.";
                    return false;
                }
                using (var uow = _unitOfWork.Create())
                {
                    var existente = uow.Repositories.UbicacionRepository.GetByNombre(ubicacion.Nombre);
                    if (existente != null)
                    {
                        mensaje = $"Ya existe una ubicacion con el nombre '{ubicacion.Nombre}'.";
                        return false;
                    }
                    if (ubicacion.Id == Guid.Empty)
                        ubicacion.Id = Guid.NewGuid();
                    uow.Repositories.UbicacionRepository.Add(ubicacion);
                    uow.SaveChanges();
                    _auditHelper.RegistrarExito("CREACION_UBICACION_TICKET",
                        $"Se creo la ubicacion de ticket '{ubicacion.Nombre}' (ID: {ubicacion.Id})", null, uow);
                    mensaje = "Ubicacion creada exitosamente.";
                    return true;
                }
            }
            catch (Exception ex)
            {
                var context = new ExceptionContext { OperationName = "UbicacionBLL.CrearUbicacion" };
                _exceptionService.Handle(ex, context);
                mensaje = $"Error al crear ubicacion: {ex.Message}";
                return false;
            }
        }
        public bool ActualizarUbicacion(Guid id, string nuevoNombre, out string mensaje)
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
                    var ubicacion = uow.Repositories.UbicacionRepository.GetById(id);
                    if (ubicacion == null)
                    {
                        mensaje = "Ubicacion no encontrada.";
                        return false;
                    }
                    var existente = uow.Repositories.UbicacionRepository.GetByNombre(nuevoNombre);
                    if (existente != null && existente.Id != id)
                    {
                        mensaje = $"Ya existe otra ubicacion con el nombre '{nuevoNombre}'.";
                        return false;
                    }
                    string nombreAnterior = ubicacion.Nombre;
                    ubicacion.Nombre = nuevoNombre.Trim();
                    uow.Repositories.UbicacionRepository.Update(ubicacion);
                    uow.SaveChanges();
                    _auditHelper.RegistrarExito("ACTUALIZACION_UBICACION_TICKET",
                        $"Se actualizo la ubicacion de ticket de '{nombreAnterior}' a '{nuevoNombre}' (ID: {id})", null, uow);
                    mensaje = "Ubicacion actualizada exitosamente.";
                    return true;
                }
            }
            catch (Exception ex)
            {
                var context = new ExceptionContext { OperationName = "UbicacionBLL.ActualizarUbicacion" };
                _exceptionService.Handle(ex, context);
                mensaje = $"Error al actualizar ubicacion: {ex.Message}";
                return false;
            }
        }
        public bool EliminarUbicacion(Guid id, out string mensaje)
        {
            try
            {
                using (var uow = _unitOfWork.Create())
                {
                    var ubicacion = uow.Repositories.UbicacionRepository.GetById(id);
                    if (ubicacion == null)
                    {
                        mensaje = "Ubicacion no encontrada.";
                        return false;
                    }
                    string nombreUbicacion = ubicacion.Nombre;
                    uow.Repositories.UbicacionRepository.Remove(ubicacion);
                    uow.SaveChanges();
                    _auditHelper.RegistrarExito("ELIMINACION_UBICACION_TICKET",
                        $"Se elimino la ubicacion '{nombreUbicacion}' (ID: {id})", null, uow);
                    mensaje = "Ubicacion eliminada exitosamente.";
                    return true;
                }
            }
            catch (SqlException sqlEx) when (sqlEx.Number == 547)
            {
                var context = new ExceptionContext { OperationName = "UbicacionBLL.EliminarUbicacion" };
                _exceptionService.Handle(sqlEx, context);
                mensaje = "No se puede eliminar la ubicacion porque esta siendo utilizada.";
                return false;
            }
            catch (Exception ex)
            {
                var context = new ExceptionContext { OperationName = "UbicacionBLL.EliminarUbicacion" };
                _exceptionService.Handle(ex, context);
                mensaje = $"Error al eliminar ubicacion: {ex.Message}";
                return false;
            }
        }
    }
}
