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
    public class UbicacionEquipoBLL : IUbicacionEquipoBLL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _loggerService;
        private readonly IExceptionService _exceptionService;
        private readonly AuditHelper _auditHelper;
        public UbicacionEquipoBLL(IUnitOfWork uow, ILoggerService loggerService = null, IExceptionService exceptionService = null, AuditHelper auditHelper = null)
        {
            _unitOfWork = uow;
            _loggerService = loggerService ?? LoggerService.Instance;
            _exceptionService = exceptionService ?? Services.Services.ExceptionService.Current;
            _auditHelper = auditHelper ?? new AuditHelper();
        }
        public List<UbicacionEquipo> ObtenerUbicacionesEquipo()
        {
            using (var uow = _unitOfWork.Create())
            {
                return uow.Repositories.UbicacionEquipoRepository.GetAll().ToList();
            }
        }
        public bool CrearUbicacionEquipo(UbicacionEquipo ubicacionEquipo, out string mensaje)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ubicacionEquipo.Nombre))
                {
                    mensaje = "El nombre de la ubicacion es obligatorio.";
                    return false;
                }
                using (var uow = _unitOfWork.Create())
                {
                    var existente = uow.Repositories.UbicacionEquipoRepository.GetByNombre(ubicacionEquipo.Nombre);
                    if (existente != null)
                    {
                        mensaje = $"Ya existe una ubicacion con el nombre '{ubicacionEquipo.Nombre}'.";
                        return false;
                    }
                    if (ubicacionEquipo.Id == Guid.Empty)
                        ubicacionEquipo.Id = Guid.NewGuid();
                    uow.Repositories.UbicacionEquipoRepository.Add(ubicacionEquipo);
                    uow.SaveChanges();
                    _auditHelper.RegistrarExito("CREACION_UBICACION_EQUIPO",
                        $"Se creo la ubicacion de equipo '{ubicacionEquipo.Nombre}' (ID: {ubicacionEquipo.Id})", null, uow);
                    mensaje = "Ubicacion creada exitosamente.";
                    return true;
                }
            }
            catch (Exception ex)
            {
                var context = new ExceptionContext { OperationName = "UbicacionEquipoBLL.CrearUbicacionEquipo" };
                _exceptionService.Handle(ex, context);
                mensaje = $"Error al crear ubicacion: {ex.Message}";
                return false;
            }
        }
        public bool ActualizarUbicacionEquipo(Guid id, string nuevoNombre, out string mensaje)
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
                    var ubicacionEquipo = uow.Repositories.UbicacionEquipoRepository.GetById(id);
                    if (ubicacionEquipo == null)
                    {
                        mensaje = "Ubicacion no encontrada.";
                        return false;
                    }
                    var existente = uow.Repositories.UbicacionEquipoRepository.GetByNombre(nuevoNombre);
                    if (existente != null && existente.Id != id)
                    {
                        mensaje = $"Ya existe otra ubicacion con el nombre '{nuevoNombre}'.";
                        return false;
                    }
                    string nombreAnterior = ubicacionEquipo.Nombre;
                    ubicacionEquipo.Nombre = nuevoNombre.Trim();
                    uow.Repositories.UbicacionEquipoRepository.Update(ubicacionEquipo);
                    uow.SaveChanges();
                    _auditHelper.RegistrarExito("ACTUALIZACION_UBICACION_EQUIPO",
                        $"Se actualizo la ubicacion de equipo de '{nombreAnterior}' a '{nuevoNombre}' (ID: {id})", null, uow);
                    mensaje = "Ubicacion actualizada exitosamente.";
                    return true;
                }
            }
            catch (Exception ex)
            {
                var context = new ExceptionContext { OperationName = "UbicacionEquipoBLL.ActualizarUbicacionEquipo" };
                _exceptionService.Handle(ex, context);
                mensaje = $"Error al actualizar ubicacion: {ex.Message}";
                return false;
            }
        }
        public bool EliminarUbicacionEquipo(Guid id, out string mensaje)
        {
            try
            {
                using (var uow = _unitOfWork.Create())
                {
                    var ubicacionEquipo = uow.Repositories.UbicacionEquipoRepository.GetById(id);
                    if (ubicacionEquipo == null)
                    {
                        mensaje = "Ubicacion no encontrada.";
                        return false;
                    }
                    string nombreUbicacion = ubicacionEquipo.Nombre;
                    uow.Repositories.UbicacionEquipoRepository.Remove(ubicacionEquipo);
                    uow.SaveChanges();
                    _auditHelper.RegistrarExito("ELIMINACION_UBICACION_EQUIPO",
                        $"Se elimino la ubicacion de equipo '{nombreUbicacion}' (ID: {id})", null, uow);
                    mensaje = "Ubicacion eliminada exitosamente.";
                    return true;
                }
            }
            catch (SqlException sqlEx) when (sqlEx.Number == 547)
            {
                var context = new ExceptionContext { OperationName = "UbicacionEquipoBLL.EliminarUbicacionEquipo" };
                _exceptionService.Handle(sqlEx, context);
                mensaje = "No se puede eliminar la ubicacion porque esta siendo utilizada.";
                return false;
            }
            catch (Exception ex)
            {
                var context = new ExceptionContext { OperationName = "UbicacionEquipoBLL.EliminarUbicacionEquipo" };
                _exceptionService.Handle(ex, context);
                mensaje = $"Error al eliminar ubicacion: {ex.Message}";
                return false;
            }
        }
    }
}
