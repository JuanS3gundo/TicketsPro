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
    public class SLABLL : ISLABLL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _loggerService;
        private readonly IExceptionService _exceptionService;
        private readonly AuditHelper _auditHelper;
        private readonly BLL.Interfaces.SLA.ISlaStrategyFactory _slaStrategyFactory;
        public SLABLL(IUnitOfWork uow, BLL.Interfaces.SLA.ISlaStrategyFactory slaStrategyFactory, ILoggerService loggerService = null, IExceptionService exceptionService = null, AuditHelper auditHelper = null)
        {
            _unitOfWork = uow;
            _slaStrategyFactory = slaStrategyFactory;
            _loggerService = loggerService ?? LoggerService.Instance;
            _exceptionService = exceptionService ?? Services.Services.ExceptionService.Current;
            _auditHelper = auditHelper ?? new AuditHelper();
        }
        public List<PoliticaSLA> ObtenerTodas()
        {
            using (var uow = _unitOfWork.Create())
            {
                return uow.Repositories.PoliticaSLARepository.GetAll().ToList();
            }
        }
        public PoliticaSLA ObtenerPorId(Guid id)
        {
            using (var uow = _unitOfWork.Create())
            {
                return uow.Repositories.PoliticaSLARepository.GetById(id);
            }
        }
        public PoliticaSLA ObtenerPorPrioridad(Guid prioridadId)
        {
            using (var uow = _unitOfWork.Create())
            {
                return uow.Repositories.PoliticaSLARepository.GetByPrioridadId(prioridadId);
            }
        }
        public bool CrearPoliticaSLA(PoliticaSLA politica, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                if (politica.HorasAtencion <= 0)
                {
                    mensaje = "El tiempo de respuesta debe ser mayor a 0 horas.";
                    return false;
                }
                if (politica.HorasResolucion <= 0)
                {
                    mensaje = "El tiempo de resolucion debe ser mayor a 0 horas.";
                    return false;
                }
                using (var uow = _unitOfWork.Create())
                {
                    if (politica.Prioridad == null || politica.Prioridad.Id == Guid.Empty)
                    {
                        mensaje = "La prioridad es obligatoria.";
                        return false;
                    }
                    var existente = uow.Repositories.PoliticaSLARepository.GetByPrioridadId(politica.Prioridad.Id);
                    if (existente != null)
                    {
                        mensaje = "Ya existe una politica SLA para esta prioridad.";
                        return false;
                    }
                    politica.Id = Guid.NewGuid();
                    uow.Repositories.PoliticaSLARepository.Add(politica);
                    uow.SaveChanges();
                    _auditHelper.RegistrarExito("CREACION_POLITICA_SLA",
                        $"Se creo una nueva politica SLA con {politica.HorasAtencion}h de respuesta y {politica.HorasResolucion}h de resolucion.", null, uow);
                    mensaje = "Politica SLA creada correctamente.";
                    return true;
                }
            }
            catch (Exception ex)
            {
                var context = new ExceptionContext { OperationName = "SLABLL.CrearPoliticaSLA" };
                _exceptionService.Handle(ex, context);
                mensaje = "Error al crear la politica SLA.";
                return false;
            }
        }
        public bool ActualizarPoliticaSLA(PoliticaSLA politica, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                if (politica.HorasAtencion <= 0)
                {
                    mensaje = "El tiempo de respuesta debe ser mayor a 0 horas.";
                    return false;
                }
                if (politica.HorasResolucion <= 0)
                {
                    mensaje = "El tiempo de resolucion debe ser mayor a 0 horas.";
                    return false;
                }
                using (var uow = _unitOfWork.Create())
                {
                    var existente = uow.Repositories.PoliticaSLARepository.GetById(politica.Id);
                    if (existente == null)
                    {
                        mensaje = "La politica SLA no existe.";
                        return false;
                    }
                    uow.Repositories.PoliticaSLARepository.Update(politica);
                    uow.SaveChanges();
                    _auditHelper.RegistrarExito("ACTUALIZACION_POLITICA_SLA",
                        $"Se actualizo la politica SLA {politica.Id}.", null, uow);
                    mensaje = "Politica SLA actualizada correctamente.";
                    return true;
                }
            }
            catch (Exception ex)
            {
                var context = new ExceptionContext { OperationName = "SLABLL.ActualizarPoliticaSLA" };
                _exceptionService.Handle(ex, context);
                mensaje = "Error al actualizar la politica SLA.";
                return false;
            }
        }
        public bool EliminarPoliticaSLA(Guid id, out string mensaje)
        {
            try
            {
                using (var uow = _unitOfWork.Create())
                {
                    var politica = uow.Repositories.PoliticaSLARepository.GetById(id);
                    if (politica == null)
                    {
                        mensaje = "Politica SLA no encontrada.";
                        return false;
                    }
                    uow.Repositories.PoliticaSLARepository.Remove(politica);
                    uow.SaveChanges();
                    _auditHelper.RegistrarExito("ELIMINACION_POLITICA_SLA",
                        $"Se elimino la politica SLA '{politica.Nombre}' (ID: {politica.Id}).", null, uow);
                    mensaje = "Politica SLA eliminada correctamente.";
                    return true;
                }
            }
            catch (SqlException sqlEx) when (sqlEx.Number == 547)
            {
                var context = new ExceptionContext { OperationName = "SLABLL.EliminarPoliticaSLA" };
                _exceptionService.Handle(sqlEx, context);
                mensaje = "No se puede eliminar la politica SLA porque esta siendo utilizada por uno o mas tickets.";
                return false;
            }
            catch (Exception ex)
            {
                var context = new ExceptionContext { OperationName = "SLABLL.EliminarPoliticaSLA" };
                _exceptionService.Handle(ex, context);
                mensaje = "Error al eliminar la politica SLA.";
                return false;
            }
        }
        public DateTime CalcularFechaVencimiento(DateTime fechaInicio, PoliticaSLA sla)
        {
            var estrategia = _slaStrategyFactory.ObtenerEstrategia(sla);
            return estrategia.CalcularFechaVencimiento(fechaInicio, sla);
        }
    }
}
