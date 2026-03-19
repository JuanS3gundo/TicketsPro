using BLL.Interfaces;
using BLL.Helpers;
using DAL.Contracts;
using DAL.Contracts.UnitOfWork;
using Entity;
using Entity.Domain;
using Services.BLL;
using Services.DomainModel;
using Services.Implementations;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using BLL.Exceptions;
namespace BLL
{
    public class EquipoBLL : IEquipoBLL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _loggerService;
        private readonly IExceptionService _exceptionService;
        private readonly AuditHelper _auditHelper;
        public EquipoBLL(IUnitOfWork unitOfWork, ILoggerService loggerService, IExceptionService exceptionService, AuditHelper auditHelper)
        {
            _unitOfWork = unitOfWork;
            _loggerService = loggerService;
            _exceptionService = exceptionService;
            _auditHelper = auditHelper;
        }
        public List<EquipoInformatico> ObtenerTodos()
        {
            using (var uow = _unitOfWork.Create())
                return uow.Repositories.EquipoInformaticoRepository.GetAll().ToList();
        }
        public bool CrearEquipoConItems(EquipoInformatico equipo, List<InventarioItem> items, out string mensaje)
        {
            mensaje = string.Empty;
            if (!ValidarEquipoBasico(equipo, items, out mensaje))
                return false;
            try
            {
                using (var uow = _unitOfWork.Create())
                {
                    var repoEquipo = uow.Repositories.EquipoInformaticoRepository;
                    var repoItem = uow.Repositories.InventarioItemRepository;
                    if (repoEquipo.GetAll().Any(e => e.NroInventario == equipo.NroInventario))
                    {
                        mensaje = string.Format(LanguageBLL.Translate("Equipo_Error_InventarioDuplicado"), equipo.NroInventario);
                        return false;
                    }
                    int idEquipo = repoEquipo.InsertAndReturnId(equipo);
                    equipo.Id = idEquipo;
                    foreach (var item in items)
                    {
                        ValidarItemInventario(item, uow);
                        AsignarValoresBasicosItem(item, equipo);
                        repoItem.Add(item);
                    }
                    uow.SaveChanges();
                    RegistrarAuditoriaExito("CREACION_EQUIPO",
                        $"El usuario '{SessionService.GetUsuario()?.UserName}' creo el equipo '{equipo.ModeloEquipo}' con NroInventario '{equipo.NroInventario}' y {items.Count} componentes.", uow);
                    mensaje = $"El equipo '{equipo.ModeloEquipo}' se creo correctamente con {items.Count} componentes asignados.";
                    return true;
                }
            }
            catch (Exception ex)
            {
                return HandleException("EquipoBLL.CrearEquipoConItems", ex, LanguageBLL.Translate("Equipo_Error_CrearEquipo"), out mensaje);
            }
        }
        public bool EditarEquipoConComponentes(
            EquipoInformatico equipoEditado,
            List<InventarioItem> itemsAInsertar,
            List<InventarioItem> itemsAActualizar,
            List<int> idsAEliminar,
            out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                using (var uow = _unitOfWork.Create())
                {
                    var repoEquipo = uow.Repositories.EquipoInformaticoRepository;
                    var repoItem = uow.Repositories.InventarioItemRepository;
                    repoEquipo.Update(equipoEditado);
                    foreach (var id in idsAEliminar ?? new List<int>())
                        repoItem.DeleteById(id);
                    foreach (var item in itemsAActualizar ?? new List<InventarioItem>())
                    {
                        if (item.EquipoAsignado == null || item.EquipoAsignado.Id == 0)
                            item.EquipoAsignado = equipoEditado;
                        repoItem.Update(item);
                    }
                    foreach (var item in itemsAInsertar ?? new List<InventarioItem>())
                    {
                        item.EquipoAsignado = equipoEditado;
                        item.Disponible = false;
                        item.FechaIngreso = DateTime.Now;
                        if (string.IsNullOrWhiteSpace(item.CodigoInventario))
                            item.CodigoInventario = Guid.NewGuid().ToString("N");
                        repoItem.Add(item);
                    }
                    uow.SaveChanges();
                    mensaje = LanguageBLL.Translate("Equipo_Exito_ComponentesActualizados");
                    RegistrarAuditoriaExito("EDICION_EQUIPO",
                        $"El usuario '{SessionService.GetUsuario()?.UserName}' edito el equipo '{equipoEditado.ModeloEquipo}' con NroInventario '{equipoEditado.NroInventario}'.", uow);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return HandleException("EquipoBLL.EditarEquipoConComponentes", ex, LanguageBLL.Translate("Equipo_Error_ActualizarComponentes"), out mensaje);
            }
        }
        public EquipoInformatico BuscarPorId(int id)
        {
            try
            {
                using (var uow = _unitOfWork.Create())
                    return uow.Repositories.EquipoInformaticoRepository
                        .GetAll()
                        .FirstOrDefault(e => e.Id == id);
            }
            catch (Exception ex)
            {
                var context = new ExceptionContext { OperationName = "EquipoBLL.BuscarPorId" };
                _exceptionService.Handle(ex, context);
                throw;
            }
        }
        public bool EliminarEquipo(int idEquipo, out string mensaje)
        {
            mensaje = "";
            try
            {
                using (var uow = _unitOfWork.Create())
                {
                    var repoEquipo = uow.Repositories.EquipoInformaticoRepository;
                    var repoItem = uow.Repositories.InventarioItemRepository;
                    var equipo = repoEquipo.GetAll().FirstOrDefault(e => e.Id == idEquipo);
                    if (equipo == null)
                    {
                        mensaje = LanguageBLL.Translate("Equipo_Error_NoExiste");
                        return false;
                    }
                    var itemsAsignados = repoItem
                        .GetAll()
                        .Where(i => i.EquipoAsignado?.Id == idEquipo)
                        .ToList();
                    foreach (var item in itemsAsignados)
                    {
                        item.EquipoAsignado = null;
                        item.Disponible = true;
                        repoItem.Update(item);
                    }
                    repoEquipo.Remove(equipo);
                    uow.SaveChanges();
                    mensaje = LanguageBLL.Translate("Equipo_Exito_Eliminado");
                    RegistrarAuditoriaExito("ELIMINACION_EQUIPO",
                        $"El usuario '{SessionService.GetUsuario()?.UserName}' elimino el equipo '{equipo.ModeloEquipo}' con NroInventario '{equipo.NroInventario}'.", uow);
                    return true;
                }
            }
            catch (SqlException sqlEx) when (sqlEx.Number == 547)
            {
                var context = new ExceptionContext { OperationName = "EquipoBLL.EliminarEquipo" };
                _exceptionService.Handle(sqlEx, context);
                mensaje = LanguageBLL.Translate("Equipo_Error_AsignadoTickets");
                return false;
            }
            catch (Exception ex)
            {
                return HandleException("EquipoBLL.EliminarEquipo", ex, LanguageBLL.Translate("Equipo_Error_Eliminar") + ": " + ex.Message, out mensaje);
            }
        }
        public bool Actualizar(EquipoInformatico equipo, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                using (var uow = _unitOfWork.Create())
                {
                    uow.Repositories.EquipoInformaticoRepository.Update(equipo);
                    uow.SaveChanges();
                    mensaje = LanguageBLL.Translate("Equipo_Exito_Actualizado");
                    RegistrarAuditoriaExito("ACTUALIZACION_EQUIPO",
                        $"El usuario '{SessionService.GetUsuario()?.UserName}' actualizo el equipo '{equipo.ModeloEquipo}' con NroInventario '{equipo.NroInventario}'.", uow);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return HandleException("EquipoBLL.Actualizar", ex, LanguageBLL.Translate("Equipo_Error_Actualizar") + ": " + ex.Message, out mensaje);
            }
        }
        public bool AsignarUsuario(int idEquipo, Guid idUsuario, out string mensaje)
        {
            mensaje = "";
            try
            {
                using (var uow = _unitOfWork.Create())
                {
                    var repoEquipo = uow.Repositories.EquipoInformaticoRepository;
                    var repoUsuarioTickets = uow.Repositories.UsuarioRepository;
                    var equipo = repoEquipo.GetById(idEquipo);
                    if (equipo == null)
                    {
                        mensaje = LanguageBLL.Translate("Equipo_Error_EquipoNoExiste");
                        return false;
                    }
                    var usuarioTickets = repoUsuarioTickets.GetById(idUsuario);
                    if (usuarioTickets == null)
                    {
                        var usuarioServices = Services.Implementations.UsuarioRepository.Current.GetById(idUsuario);
                        if (usuarioServices == null)
                        {
                            mensaje = LanguageBLL.Translate("Equipo_Error_UsuarioNoExiste");
                            return false;
                        }
                        Services.Services.UserSyncService.SyncUser(usuarioServices);
                    }
                    var userName = usuarioTickets?.UserName
                        ?? Services.Implementations.UsuarioRepository.Current.GetById(idUsuario)?.UserName
                        ?? idUsuario.ToString();
                    equipo.UsuarioAsignado = usuarioTickets ?? repoUsuarioTickets.GetById(idUsuario);
                    repoEquipo.Update(equipo);
                    uow.SaveChanges();
                    mensaje = LanguageBLL.Translate("Equipo_Exito_Asignado");
                    RegistrarAuditoriaExito("ASIGNACION_EQUIPO",
                        $"Se asigno el equipo {equipo.NroInventario} al usuario {userName}.", uow);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return HandleException("EquipoBLL.AsignarUsuario", ex, LanguageBLL.Translate("Equipo_Error_Asignar"), out mensaje);
            }
        }
        public bool QuitarAsignacionUsuario(int idEquipo, out string mensaje)
        {
            mensaje = "";
            try
            {
                using (var uow = _unitOfWork.Create())
                {
                    var repoEquipo = uow.Repositories.EquipoInformaticoRepository;
                    var equipo = repoEquipo.GetById(idEquipo);
                    if (equipo == null)
                    {
                        mensaje = LanguageBLL.Translate("Equipo_Error_EquipoNoExiste");
                        return false;
                    }
                    string modeloEquipo = equipo.ModeloEquipo;
                    equipo.UsuarioAsignado = null;
                    repoEquipo.Update(equipo);
                    uow.SaveChanges();
                    RegistrarAuditoriaExito("QUITAR_ASIGNACION_EQUIPO",
                        $"Se quito la asignacion del equipo '{modeloEquipo}' (ID: {idEquipo})", uow);
                    mensaje = LanguageBLL.Translate("Equipo_Exito_AsignacionEliminada");
                    return true;
                }
            }
            catch (Exception ex)
            {
                return HandleException("EquipoBLL.QuitarAsignacionUsuario", ex, LanguageBLL.Translate("Equipo_Error_QuitarAsignacion"), out mensaje);
            }
        }
        private bool ValidarEquipoBasico(EquipoInformatico equipo, List<InventarioItem> items, out string mensaje)
        {
            if (equipo == null)
            {
                mensaje = LanguageBLL.Translate("Equipo_Error_EquipoNulo");
                return false;
            }
            if (string.IsNullOrWhiteSpace(equipo.NroInventario))
            {
                mensaje = LanguageBLL.Translate("Equipo_Error_InventarioObligatorio");
                return false;
            }
            if (items == null || items.Count == 0)
            {
                mensaje = LanguageBLL.Translate("Equipo_Error_ComponentesObligatorios");
                return false;
            }
            mensaje = string.Empty;
            return true;
        }
        private void ValidarItemInventario(InventarioItem item, IUnitOfWorkAdapter uow)
        {
            if (string.IsNullOrWhiteSpace(item.Nombre))
                throw new ArgumentException(LanguageBLL.Translate("Equipo_Componente_NombreObligatorio"), nameof(item.Nombre));
            if (item.CategoriaItem == null || item.CategoriaItem.Id == Guid.Empty)
                throw new ArgumentException(string.Format(LanguageBLL.Translate("Equipo_Componente_CategoriaObligatoria"), item.Nombre), nameof(item.CategoriaItem));
            
            if (string.IsNullOrEmpty(item.CategoriaItem.Nombre))
            {
                item.CategoriaItem = uow.Repositories.CategoriaItemRepository.GetById(item.CategoriaItem.Id);
            }
            if (item.CategoriaItem?.Nombre?.Equals("Procesador", StringComparison.OrdinalIgnoreCase) == true)
            {
                if (string.IsNullOrWhiteSpace(item.Nombre))
                    throw new ArgumentException(LanguageBLL.Translate("Equipo_Componente_ProcesadorObligatorio"), nameof(item.Nombre));
            }
            else
            {
                if (item.Valor <= 0)
                    throw new ArgumentOutOfRangeException(nameof(item.Valor), item.Valor, LanguageBLL.Translate("Equipo_Componente_ValorPositivo"));
                if (string.IsNullOrWhiteSpace(item.Unidad))
                    throw new ArgumentException(LanguageBLL.Translate("Equipo_Componente_UnidadObligatoria"), nameof(item.Unidad));
            }
        }
        private void AsignarValoresBasicosItem(InventarioItem item, EquipoInformatico equipo)
        {
            if (string.IsNullOrWhiteSpace(item.CodigoInventario))
                item.CodigoInventario = Guid.NewGuid().ToString("N");
            item.EquipoAsignado = equipo;
            item.Disponible = false;
            item.FechaIngreso = DateTime.Now;
        }
        private void RegistrarAuditoriaExito(string accion, string detalle, IUnitOfWorkAdapter uow)
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
