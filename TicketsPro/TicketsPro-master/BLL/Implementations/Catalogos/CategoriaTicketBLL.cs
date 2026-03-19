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
    public class CategoriaTicketBLL : ICategoriaTicketBLL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _loggerService;
        private readonly IExceptionService _exceptionService;
        private readonly AuditHelper _auditHelper;
        public CategoriaTicketBLL(IUnitOfWork uow, ILoggerService loggerService = null, IExceptionService exceptionService = null, AuditHelper auditHelper = null)
        {
            _unitOfWork = uow;
            _loggerService = loggerService ?? LoggerService.Instance;
            _exceptionService = exceptionService ?? Services.Services.ExceptionService.Current;
            _auditHelper = auditHelper ?? new AuditHelper();
        }
        public List<CategoriaTicket> ObtenerCategorias()
        {
            using (var uow = _unitOfWork.Create())
            {
                return uow.Repositories.CategoriaTicketRepository.GetAll().ToList();
            }
        }
        public bool CrearCategoria(CategoriaTicket categoria, out string mensaje)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(categoria.Nombre))
                {
                    mensaje = "El nombre de la categoria es obligatorio.";
                    return false;
                }
                using (var uow = _unitOfWork.Create())
                {
                    var existente = uow.Repositories.CategoriaTicketRepository.GetByNombre(categoria.Nombre);
                    if (existente != null)
                    {
                        mensaje = $"Ya existe una categoria con el nombre '{categoria.Nombre}'.";
                        return false;
                    }
                    if (categoria.Id == Guid.Empty)
                        categoria.Id = Guid.NewGuid();
                    uow.Repositories.CategoriaTicketRepository.Add(categoria);
                    uow.SaveChanges();
                    _auditHelper.RegistrarExito("CREACION_CATEGORIA_TICKET",
                        $"Se creo la categoria de ticket '{categoria.Nombre}' (ID: {categoria.Id})", null, uow);
                    mensaje = "Categoria creada exitosamente.";
                    return true;
                }
            }
            catch (Exception ex)
            {
                var context = new ExceptionContext { OperationName = "CategoriaTicketBLL.CrearCategoria" };
                _exceptionService.Handle(ex, context);
                mensaje = $"Error al crear categoria: {ex.Message}";
                return false;
            }
        }
        public bool ActualizarCategoria(CategoriaTicket categoria, out string mensaje)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(categoria.Nombre))
                {
                    mensaje = "El nuevo nombre es obligatorio.";
                    return false;
                }
                using (var uow = _unitOfWork.Create())
                {
                    var existente = uow.Repositories.CategoriaTicketRepository.GetById(categoria.Id);
                    if (existente == null)
                    {
                        mensaje = "Categoria no encontrada.";
                        return false;
                    }
                    var duplicada = uow.Repositories.CategoriaTicketRepository.GetByNombre(categoria.Nombre);
                    if (duplicada != null && duplicada.Id != categoria.Id)
                    {
                        mensaje = $"Ya existe otra categoria con el nombre '{categoria.Nombre}'.";
                        return false;
                    }
                    string nombreAnterior = existente.Nombre;
                    existente.Nombre = categoria.Nombre.Trim();
                    existente.Descripcion = categoria.Descripcion;
                    existente.PoliticaSLA = categoria.PoliticaSLA;
                    uow.Repositories.CategoriaTicketRepository.Update(existente);
                    uow.SaveChanges();
                    _auditHelper.RegistrarExito("ACTUALIZACION_CATEGORIA_TICKET",
                        $"Se actualizo la categoria de ticket de '{nombreAnterior}' a '{existente.Nombre}' (ID: {categoria.Id})", null, uow);
                    mensaje = "Categoria actualizada exitosamente.";
                    return true;
                }
            }
            catch (Exception ex)
            {
                var context = new ExceptionContext { OperationName = "CategoriaTicketBLL.ActualizarCategoria" };
                _exceptionService.Handle(ex, context);
                mensaje = $"Error al actualizar categoria: {ex.Message}";
                return false;
            }
        }
        public bool EliminarCategoria(Guid id, out string mensaje)
        {
            try
            {
                using (var uow = _unitOfWork.Create())
                {
                    var categoria = uow.Repositories.CategoriaTicketRepository.GetById(id);
                    if (categoria == null)
                    {
                        mensaje = "Categoria no encontrada.";
                        return false;
                    }
                    string nombreCategoria = categoria.Nombre;
                    uow.Repositories.CategoriaTicketRepository.Remove(categoria);
                    uow.SaveChanges();
                    _auditHelper.RegistrarExito("ELIMINACION_CATEGORIA_TICKET",
                        $"Se elimino la categoria '{nombreCategoria}' (ID: {id})", null, uow);
                    mensaje = "Categoria eliminada exitosamente.";
                    return true;
                }
            }
            catch (SqlException sqlEx) when (sqlEx.Number == 547)
            {
                var context = new ExceptionContext { OperationName = "CategoriaTicketBLL.EliminarCategoria" };
                _exceptionService.Handle(sqlEx, context);
                mensaje = "No se puede eliminar la categoria porque esta siendo utilizada.";
                return false;
            }
            catch (Exception ex)
            {
                var context = new ExceptionContext { OperationName = "CategoriaTicketBLL.EliminarCategoria" };
                _exceptionService.Handle(ex, context);
                mensaje = $"Error al eliminar categoria: {ex.Message}";
                return false;
            }
        }
    }
}
