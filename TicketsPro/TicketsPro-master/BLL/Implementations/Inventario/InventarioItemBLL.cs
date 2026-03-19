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
    public class InventarioItemBLL : IInventarioItemBLL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExceptionService _exceptionService;
        private readonly AuditHelper _auditHelper;
        public InventarioItemBLL(IUnitOfWork unitOfWork, IExceptionService exceptionService, AuditHelper auditHelper = null)
        {
            _unitOfWork = unitOfWork;
            _exceptionService = exceptionService;
            _auditHelper = auditHelper ?? new AuditHelper();
        }
        public bool AgregarItem(InventarioItem item, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                using (var uow = _unitOfWork.Create())
                {
                    uow.Repositories.InventarioItemRepository.Add(item);
                    uow.SaveChanges();
                    RegistrarExito("CREACION_ITEM",
                        $"Se creo el item '{item.Nombre}' con codigo '{item.CodigoInventario}'.", uow);
                    mensaje = $"El item '{item.Nombre}' se creo correctamente.";
                    return true;
                }
            }
            catch (Exception ex)
            {
                return HandleException("InventarioItemBLL.AgregarItem", ex, LanguageBLL.Translate("Inventario_ErrorAgregarItem"), out mensaje);
            }
        }
        public bool ModificarItem(InventarioItem item, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                using (var uow = _unitOfWork.Create())
                {
                    uow.Repositories.InventarioItemRepository.Update(item);
                    uow.SaveChanges();
                    RegistrarExito("MODIFICACION_ITEM",
                        $"Se modifico el item '{item.Nombre}' con codigo '{item.CodigoInventario}'.", uow);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return HandleException("InventarioItemBLL.ModificarItem", ex, LanguageBLL.Translate("Inventario_ErrorModificarItem"), out mensaje);
            }
        }
        public List<InventarioItem> ObtenerTodos()
        {
            using (var uow = _unitOfWork.Create())
            {
                return uow.Repositories.InventarioItemRepository.GetAll().ToList();
            }
        }
        public bool AsignarAEquipo(int itemId, int equipoId, out string mensaje)
        {
            mensaje = string.Empty;
            InventarioItem item = null;
            try
            {
                using (var uow = _unitOfWork.Create())
                {
                    item = uow.Repositories.InventarioItemRepository.GetById(itemId);
                    if (item == null)
                    {
                        mensaje = LanguageBLL.Translate("Inventario_ItemNoEncontrado");
                        return false;
                    }
                    string nombreItem = item.Nombre ?? itemId.ToString();
                    uow.Repositories.InventarioItemRepository.AsignarAEquipo(itemId, equipoId);
                    uow.SaveChanges();
                    RegistrarExito("ASIGNACION_ITEM",
                        $"Se asigno el item '{nombreItem}' (ID: {itemId}) al equipo (ID: {equipoId})", uow);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return HandleException("InventarioItemBLL.AsignarAEquipo", ex, LanguageBLL.Translate("Inventario_ErrorAsignarItem"), out mensaje);
            }
        }
        public bool QuitarDeEquipo(int itemId, out string mensaje)
        {
            mensaje = string.Empty;
            int? equipoIdAnterior = null;
            string itemNombre = "Desconocido";
            try
            {
                using (var uow = _unitOfWork.Create())
                {
                    var item = uow.Repositories.InventarioItemRepository.GetById(itemId);
                    if (item != null)
                    {
                        equipoIdAnterior = item.EquipoAsignado?.Id;
                        itemNombre = item.Nombre ?? itemId.ToString();
                    }
                    uow.Repositories.InventarioItemRepository.QuitarDeEquipo(itemId);
                    uow.SaveChanges();
                    RegistrarExito("QUITAR_ITEM_DE_EQUIPO",
                        $"Se quito el item '{itemNombre}' (ID: {itemId}) del equipo (ID: {equipoIdAnterior ?? 0})", uow);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return HandleException("InventarioItemBLL.QuitarDeEquipo", ex, LanguageBLL.Translate("Inventario_ErrorQuitarItem"), out mensaje);
            }
        }
        public List<UbicacionEquipo> ObtenerUbicacionesEquipo()
        {
            using (var uow = _unitOfWork.Create())
            {
                return uow.Repositories.UbicacionEquipoRepository.GetAll().ToList();
            }
        }
        public bool EliminarItem(string codigoInventario, out string mensaje)
        {
            mensaje = "";
            InventarioItem item = null;
            int? equipoIdEnBitacora = null;
            try
            {
                using (var uow = _unitOfWork.Create())
                {
                    var repo = uow.Repositories.InventarioItemRepository;
                    item = repo.GetAll().FirstOrDefault(i => i.CodigoInventario == codigoInventario);
                    if (item == null)
                    {
                        mensaje = LanguageBLL.Translate("Inventario_ItemNoExiste");
                        return false;
                    }
                    if (item.EquipoAsignado != null && item.EquipoAsignado.Id != 0)
                    {
                        mensaje = LanguageBLL.Translate("Inventario_ItemAsignadoDebeQuitar");
                        return false;
                    }
                    equipoIdEnBitacora = item.EquipoAsignado?.Id;
                    bool ok = repo.DeleteById(item.Id);
                    if (!ok)
                    {
                        mensaje = LanguageBLL.Translate("Inventario_NoSePudoEliminar");
                        return false;
                    }
                    uow.SaveChanges();
                    RegistrarExito("ELIMINACION_ITEM",
                        $"Se elimino el item '{item.Nombre}' (Codigo: {item.CodigoInventario}).", uow);
                    mensaje = LanguageBLL.Translate("Inventario_ItemEliminadoOk");
                    return true;
                }
            }
            catch (Exception ex)
            {
                return HandleException("InventarioItemBLL.EliminarItem", ex, LanguageBLL.Translate("Inventario_ErrorEliminarItem"), out mensaje);
            }
        }
        private void RegistrarExito(string accion, string detalle, IUnitOfWorkAdapter uow)
        {
            _auditHelper.RegistrarExito(accion, detalle, null, uow);
        }
        private bool HandleException(string operationName, Exception ex, string mensajeError, out string mensaje)
        {
            var context = new ExceptionContext { OperationName = operationName };
            _exceptionService.Handle(ex, context);
            mensaje = mensajeError;
            return false;
        }
    }
}
