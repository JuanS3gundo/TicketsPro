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
    public class TipoEquipoBLL : ITipoEquipoBLL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _loggerService;
        private readonly IExceptionService _exceptionService;
        private readonly AuditHelper _auditHelper;
        public TipoEquipoBLL(IUnitOfWork uow, ILoggerService loggerService = null, IExceptionService exceptionService = null, AuditHelper auditHelper = null)
        {
            _unitOfWork = uow;
            _loggerService = loggerService ?? LoggerService.Instance;
            _exceptionService = exceptionService ?? Services.Services.ExceptionService.Current;
            _auditHelper = auditHelper ?? new AuditHelper();
        }
        public List<TipoEquipo> ObtenerTiposEquipo()
        {
            using (var uow = _unitOfWork.Create())
            {
                return uow.Repositories.TipoEquipoRepository.GetAll().ToList();
            }
        }
        public bool CrearTipoEquipo(TipoEquipo tipoEquipo, out string mensaje)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tipoEquipo.Nombre))
                {
                    mensaje = "El nombre del tipo de equipo es obligatorio.";
                    return false;
                }
                using (var uow = _unitOfWork.Create())
                {
                    var existente = uow.Repositories.TipoEquipoRepository.GetByNombre(tipoEquipo.Nombre);
                    if (existente != null)
                    {
                        mensaje = $"Ya existe un tipo de equipo con el nombre '{tipoEquipo.Nombre}'.";
                        return false;
                    }
                    if (tipoEquipo.Id == Guid.Empty)
                        tipoEquipo.Id = Guid.NewGuid();
                    uow.Repositories.TipoEquipoRepository.Add(tipoEquipo);
                    uow.SaveChanges();
                    _auditHelper.RegistrarExito("CREACION_TIPO_EQUIPO",
                        $"Se creo el tipo de equipo '{tipoEquipo.Nombre}' (ID: {tipoEquipo.Id})", null, uow);
                    mensaje = "Tipo de equipo creado exitosamente.";
                    return true;
                }
            }
            catch (Exception ex)
            {
                var context = new ExceptionContext { OperationName = "TipoEquipoBLL.CrearTipoEquipo" };
                _exceptionService.Handle(ex, context);
                mensaje = $"Error al crear tipo de equipo: {ex.Message}";
                return false;
            }
        }
        public bool ActualizarTipoEquipo(Guid id, string nuevoNombre, out string mensaje)
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
                    var tipoEquipo = uow.Repositories.TipoEquipoRepository.GetById(id);
                    if (tipoEquipo == null)
                    {
                        mensaje = "Tipo de equipo no encontrado.";
                        return false;
                    }
                    var existente = uow.Repositories.TipoEquipoRepository.GetByNombre(nuevoNombre);
                    if (existente != null && existente.Id != id)
                    {
                        mensaje = $"Ya existe otro tipo de equipo con el nombre '{nuevoNombre}'.";
                        return false;
                    }
                    string nombreAnterior = tipoEquipo.Nombre;
                    tipoEquipo.Nombre = nuevoNombre.Trim();
                    uow.Repositories.TipoEquipoRepository.Update(tipoEquipo);
                    uow.SaveChanges();
                    _auditHelper.RegistrarExito("ACTUALIZACION_TIPO_EQUIPO",
                        $"Se actualizo el tipo de equipo de '{nombreAnterior}' a '{nuevoNombre}' (ID: {id})", null, uow);
                    mensaje = "Tipo de equipo actualizado exitosamente.";
                    return true;
                }
            }
            catch (Exception ex)
            {
                var context = new ExceptionContext { OperationName = "TipoEquipoBLL.ActualizarTipoEquipo" };
                _exceptionService.Handle(ex, context);
                mensaje = $"Error al actualizar tipo de equipo: {ex.Message}";
                return false;
            }
        }
        public bool EliminarTipoEquipo(Guid id, out string mensaje)
        {
            try
            {
                using (var uow = _unitOfWork.Create())
                {
                    var tipoEquipo = uow.Repositories.TipoEquipoRepository.GetById(id);
                    if (tipoEquipo == null)
                    {
                        mensaje = "Tipo de equipo no encontrado.";
                        return false;
                    }
                    string nombreTipo = tipoEquipo.Nombre;
                    uow.Repositories.TipoEquipoRepository.Remove(tipoEquipo);
                    uow.SaveChanges();
                    _auditHelper.RegistrarExito("ELIMINACION_TIPO_EQUIPO",
                        $"Se elimino el tipo de equipo '{nombreTipo}' (ID: {id})", null, uow);
                    mensaje = "Tipo de equipo eliminado exitosamente.";
                    return true;
                }
            }
            catch (SqlException sqlEx) when (sqlEx.Number == 547)
            {
                var context = new ExceptionContext { OperationName = "TipoEquipoBLL.EliminarTipoEquipo" };
                _exceptionService.Handle(sqlEx, context);
                mensaje = "No se puede eliminar el tipo de equipo porque esta siendo utilizado.";
                return false;
            }
            catch (Exception ex)
            {
                var context = new ExceptionContext { OperationName = "TipoEquipoBLL.EliminarTipoEquipo" };
                _exceptionService.Handle(ex, context);
                mensaje = $"Error al eliminar tipo de equipo: {ex.Message}";
                return false;
            }
        }
    }
}
