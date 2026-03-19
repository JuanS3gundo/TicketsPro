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
    public class CategoriaItemBLL : ICategoriaItemBLL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _loggerService;
        private readonly IExceptionService _exceptionService;
        private readonly AuditHelper _auditHelper;
        public CategoriaItemBLL(IUnitOfWork unitOfWork, ILoggerService loggerService = null, IExceptionService exceptionService = null)
        {
            _unitOfWork = unitOfWork;
            _exceptionService = exceptionService ?? Services.Services.ExceptionService.Current;
            _loggerService = loggerService ?? LoggerService.Instance;
            _auditHelper = new AuditHelper();
        }
        public List<CategoriaItem> ObtenerTodas()
        {
            using (var uow = _unitOfWork.Create())
            {
                return uow.Repositories.CategoriaItemRepository.GetAll().ToList();
            }
        }
        public bool CrearCategoriaItem(CategoriaItem categoriaItem, out string mensaje)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(categoriaItem.Nombre))
                {
                    mensaje = "El nombre de la categoria es obligatorio.";
                    return false;
                }
                using (var uow = _unitOfWork.Create())
                {
                    var existente = uow.Repositories.CategoriaItemRepository.GetByNombre(categoriaItem.Nombre);
                    if (existente != null)
                    {
                        mensaje = $"Ya existe una categoria con el nombre '{categoriaItem.Nombre}'.";
                        return false;
                    }
                    if (categoriaItem.Id == Guid.Empty)
                        categoriaItem.Id = Guid.NewGuid();
                    uow.Repositories.CategoriaItemRepository.Add(categoriaItem);
                    uow.SaveChanges();
                    _auditHelper.RegistrarExito("CREACION_CATEGORIA_ITEM",
                        $"Se creo la categoria de item '{categoriaItem.Nombre}' (ID: {categoriaItem.Id})", null, uow);
                    mensaje = "Categoria de item creada exitosamente.";
                    return true;
                }
            }
            catch (Exception ex)
            {
                var context = new ExceptionContext { OperationName = "CategoriaItemBLL.CrearCategoriaItem" };
                _exceptionService.Handle(ex, context);
                mensaje = $"Error al crear categoria de item: {ex.Message}";
                return false;
            }
        }
        public bool ActualizarCategoriaItem(Guid id, string nuevoNombre, out string mensaje)
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
                    var categoriaItem = uow.Repositories.CategoriaItemRepository.GetById(id);
                    if (categoriaItem == null)
                    {
                        mensaje = "Categoria de item no encontrada.";
                        return false;
                    }
                    var existente = uow.Repositories.CategoriaItemRepository.GetByNombre(nuevoNombre);
                    if (existente != null && existente.Id != id)
                    {
                        mensaje = $"Ya existe otra categoria con el nombre '{nuevoNombre}'.";
                        return false;
                    }
                    string nombreAnterior = categoriaItem.Nombre;
                    categoriaItem.Nombre = nuevoNombre.Trim();
                    uow.Repositories.CategoriaItemRepository.Update(categoriaItem);
                    uow.SaveChanges();
                    _auditHelper.RegistrarExito("ACTUALIZACION_CATEGORIA_ITEM",
                        $"Se actualizo la categoria de item de '{nombreAnterior}' a '{nuevoNombre}' (ID: {id})", null, uow);
                    mensaje = "Categoria de item actualizada exitosamente.";
                    return true;
                }
            }
            catch (Exception ex)
            {
                var context = new ExceptionContext { OperationName = "CategoriaItemBLL.ActualizarCategoriaItem" };
                _exceptionService.Handle(ex, context);
                mensaje = $"Error al actualizar categoria de item: {ex.Message}";
                return false;
            }
        }
        public bool EliminarCategoriaItem(Guid id, out string mensaje)
        {
            try
            {
                using (var uow = _unitOfWork.Create())
                {
                    var categoriaItem = uow.Repositories.CategoriaItemRepository.GetById(id);
                    if (categoriaItem == null)
                    {
                        mensaje = "Categoria de item no encontrada.";
                        return false;
                    }
                    string nombreCategoria = categoriaItem.Nombre;
                    uow.Repositories.CategoriaItemRepository.Remove(categoriaItem);
                    uow.SaveChanges();
                    _auditHelper.RegistrarExito("ELIMINACION_CATEGORIA_ITEM",
                        $"Se elimino la categoria de item '{nombreCategoria}' (ID: {id})", null, uow);
                    mensaje = "Categoria de item eliminada exitosamente.";
                    return true;
                }
            }
            catch (SqlException sqlEx) when (sqlEx.Number == 547)
            {
                var context = new ExceptionContext { OperationName = "CategoriaItemBLL.EliminarCategoriaItem" };
                _exceptionService.Handle(sqlEx, context);
                mensaje = "No se puede eliminar la categoria porque esta siendo utilizada por uno o mas items.";
                return false;
            }
            catch (Exception ex)
            {
                var context = new ExceptionContext { OperationName = "CategoriaItemBLL.EliminarCategoriaItem" };
                _exceptionService.Handle(ex, context);
                mensaje = $"Error al eliminar categoria de item: {ex.Message}";
                return false;
            }
        }
    }
}
