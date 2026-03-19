using BLL.Interfaces;
using Entity;
using Entity.Domain;
using Services.BLL;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using TicketPro.MVP.Views;
namespace TicketPro.MVP.Presenters
{
    public class FormGestionInventarioPresenter
    {
        private readonly IFormGestionInventarioView _view;
        private readonly IEquipoBLL _equipoBLL;
        private readonly IInventarioItemBLL _itemBLL;
        private readonly ExportarExcelService _exportService;
        private List<EquipoInformatico> _equiposCache = new List<EquipoInformatico>();
        private List<InventarioItem> _itemsCache = new List<InventarioItem>();
        private string _idKey, _modeloKey, _nroInventarioKey, _tipoKey, _ubicacionKey;
        private string _codigoKey, _nombreKey, _categoriaKey, _asignacionKey;
        public FormGestionInventarioPresenter(
            IFormGestionInventarioView view,
            IEquipoBLL equipoBLL,
            IInventarioItemBLL itemBLL)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _equipoBLL = equipoBLL ?? throw new ArgumentNullException(nameof(equipoBLL));
            _itemBLL = itemBLL ?? throw new ArgumentNullException(nameof(itemBLL));
            _exportService = ExportarExcelService.Instance;
        }
        public void Iniciar()
        {
            CargarTraduccionesCombos();
            CargarTodo();
        }
        private void CargarTraduccionesCombos()
        {
            _idKey = LanguageBLL.Translate("Inventario_Gestion_FiltroEquipo_ID");
            _modeloKey = LanguageBLL.Translate("Inventario_Gestion_FiltroEquipo_Modelo");
            _nroInventarioKey = LanguageBLL.Translate("Inventario_Gestion_FiltroEquipo_NroInventario");
            _tipoKey = LanguageBLL.Translate("Inventario_Gestion_FiltroEquipo_Tipo");
            _ubicacionKey = LanguageBLL.Translate("Inventario_Gestion_FiltroEquipo_Ubicacion");
            _codigoKey = LanguageBLL.Translate("Inventario_Gestion_FiltroItem_Codigo");
            _nombreKey = LanguageBLL.Translate("Inventario_Gestion_FiltroItem_Nombre");
            _categoriaKey = LanguageBLL.Translate("Inventario_Gestion_FiltroItem_Categoria");
            _asignacionKey = LanguageBLL.Translate("Inventario_Gestion_FiltroItem_Asignacion");
            _view.LlenarFiltros(
                new[] { _idKey, _modeloKey, _nroInventarioKey, _tipoKey, _ubicacionKey },
                new[] { _idKey, _codigoKey, _nombreKey, _categoriaKey, _ubicacionKey, _asignacionKey }
            );
        }
        public void CargarTodo()
        {
            try
            {
                _equiposCache = _equipoBLL.ObtenerTodos();
                _itemsCache = _itemBLL.ObtenerTodos();
                AplicarFiltroEquipos();
                AplicarFiltroItems();
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                _view.MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                _view.MostrarError(string.Format(LanguageBLL.Translate("Inventario_Gestion_Error_CargarDatos"), ex.Message));
            }
        }
        public void AplicarFiltroEquipos()
        {
            var texto = (_view.BusquedaEquipo ?? "").Trim().ToLower();
            var filtro = _view.FiltroEquipoSeleccionado ?? "";
            IEnumerable<EquipoInformatico> filtrados = _equiposCache;
            if (!string.IsNullOrEmpty(texto))
            {
                if (filtro == _idKey)
                {
                    if (int.TryParse(texto, out int idBuscado))
                        filtrados = _equiposCache.Where(eq => eq.Id == idBuscado);
                    else
                        filtrados = Enumerable.Empty<EquipoInformatico>();
                }
                else if (filtro == _modeloKey)
                {
                    filtrados = _equiposCache.Where(eq => (eq.ModeloEquipo ?? "").ToLower().Contains(texto));
                }
                else if (filtro == _nroInventarioKey)
                {
                    filtrados = _equiposCache.Where(eq => (eq.NroInventario ?? "").ToLower().Contains(texto));
                }
                else if (filtro == _tipoKey)
                {
                    filtrados = _equiposCache.Where(eq => (eq.TipoEquipo?.Nombre ?? (eq.TipoEquipo?.Id.ToString() ?? "")).ToLower().Contains(texto));
                }
                else if (filtro == _ubicacionKey)
                {
                    filtrados = _equiposCache.Where(eq => (eq.UbicacionEquipo?.Nombre ?? (eq.UbicacionEquipo?.Id.ToString() ?? "")).ToLower().Contains(texto));
                }
            }
            _view.RenderizarEquipos(filtrados, _itemsCache);
        }
        public void AplicarFiltroItems()
        {
            var texto = (_view.BusquedaItem ?? "").Trim().ToLower();
            var filtro = _view.FiltroItemSeleccionado ?? "";
            IEnumerable<InventarioItem> filtrados = _itemsCache;
            if (!string.IsNullOrEmpty(texto))
            {
                if (filtro == _idKey)
                {
                    if (int.TryParse(texto, out int idItem))
                        filtrados = _itemsCache.Where(i => i.Id == idItem);
                    else
                        filtrados = Enumerable.Empty<InventarioItem>();
                }
                else if (filtro == _codigoKey)
                {
                    filtrados = _itemsCache.Where(i => (i.CodigoInventario ?? "").ToLower().Contains(texto));
                }
                else if (filtro == _nombreKey)
                {
                    filtrados = _itemsCache.Where(i => (i.Nombre ?? "").ToLower().Contains(texto));
                }
                else if (filtro == _categoriaKey)
                {
                    filtrados = _itemsCache.Where(i => (i.CategoriaItem?.Nombre ?? (i.CategoriaItem?.Id.ToString() ?? "")).ToLower().Contains(texto));
                }
                else if (filtro == _ubicacionKey)
                {
                    filtrados = _itemsCache.Where(i => (i.UbicacionEquipo?.Nombre ?? (i.UbicacionEquipo?.Id.ToString() ?? "")).ToLower().Contains(texto));
                }
                else if (filtro == _asignacionKey)
                {
                    string sinKey = LanguageBLL.Translate("Asignacion_Filtro_Sin");
                    string libreKey = LanguageBLL.Translate("Asignacion_Filtro_Libre");
                    string noKey = LanguageBLL.Translate("Asignacion_Filtro_No");
                    bool buscarAsignados = !(texto.Contains(sinKey.ToLower()) || texto.Contains(libreKey.ToLower()) || texto.StartsWith(noKey.ToLower()));
                    filtrados = buscarAsignados
                        ? _itemsCache.Where(i => i.EquipoAsignado != null)
                        : _itemsCache.Where(i => i.EquipoAsignado == null);
                }
            }
            _view.RenderizarItems(filtrados, _equiposCache);
        }
        public void EliminarEquipo(EquipoInformatico eq)
        {
            string msgConfirm = LanguageBLL.Translate("Inventario_Gestion_Equipo_ConfirmarEliminar");
            string tituloConfirm = LanguageBLL.Translate("Confirmar_Eliminacion");
            if (_view.ConfirmarAccion(string.Format(msgConfirm, eq.ModeloEquipo), tituloConfirm))
            {
                try
                {
                    bool ok = _equipoBLL.EliminarEquipo(eq.Id, out string mensaje);
                    if (ok)
                    {
                        _view.NotificarExito(mensaje);
                        CargarTodo();
                    }
                    else
                    {
                        _view.MostrarError(mensaje);
                    }
                }
                catch (BLL.Exceptions.BLLException ex)
                {
                    _view.MostrarError(ex.Message);
                }
                catch (Exception ex)
                {
                    _view.MostrarError(string.Format(LanguageBLL.Translate("Inventario_Gestion_Error_EliminarEquipo"), ex.Message));
                }
            }
        }
        public void EliminarItem(InventarioItem item)
        {
            string msgConfirm = LanguageBLL.Translate("Inventario_Gestion_Item_ConfirmarEliminar");
            string tituloConfirm = LanguageBLL.Translate("Confirmar_Eliminacion");
            if (_view.ConfirmarAccion(string.Format(msgConfirm, item.Nombre), tituloConfirm))
            {
                try
                {
                    bool ok = _itemBLL.EliminarItem(item.CodigoInventario, out string mensaje);
                    if (ok)
                    {
                        _view.NotificarExito(mensaje);
                        CargarTodo();
                    }
                    else
                    {
                        _view.MostrarError(mensaje);
                    }
                }
                catch (BLL.Exceptions.BLLException ex)
                {
                    _view.MostrarError(ex.Message);
                }
                catch (Exception ex)
                {
                    _view.MostrarError(string.Format(LanguageBLL.Translate("Inventario_Gestion_Error_EliminarItem"), ex.Message));
                }
            }
        }
        public void ExportarEquiposAExcel()
        {
            string msgNoDatos = LanguageBLL.Translate("Inventario_Gestion_Exportar_MsgNoDatos");
            string msgExito = LanguageBLL.Translate("Inventario_Gestion_Exportar_MsgExito");
            if (!_equiposCache.Any())
            {
                _view.MostrarError(msgNoDatos);
                return;
            }
            string defaultName = $"Inventario_Equipos_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
            string path = _view.SolicitarRutaExportacionExcel(defaultName);
            if (!string.IsNullOrEmpty(path))
            {
                try
                {
                    // Mapear entidades de Entity.Domain a DTOs de Services
                    var equiposDto = _equiposCache.Select(eq => new Services.DTOs.EquipoInformaticoDto
                    {
                        Id                   = eq.Id,
                        ModeloEquipo         = eq.ModeloEquipo,
                        NroInventario        = eq.NroInventario,
                        TipoEquipoNombre     = eq.TipoEquipo?.Nombre,
                        UbicacionEquipoNombre= eq.UbicacionEquipo?.Nombre,
                        UsuarioAsignadoUserName = eq.UsuarioAsignado?.UserName
                    }).ToList();

                    var itemsDto = _itemsCache.Select(i => new Services.DTOs.InventarioItemDto
                    {
                        EquipoAsignadoId = i.EquipoAsignado?.Id ?? 0,
                        CategoriaNombre  = i.CategoriaItem?.Nombre,
                        Nombre           = i.Nombre,
                        Valor            = (double)i.Valor
                    }).ToList();

                    string csvContent = _exportService.ExportarEquipos(equiposDto, itemsDto);
                    if (string.IsNullOrEmpty(csvContent))
                    {
                        _view.MostrarError(msgNoDatos);
                        return;
                    }
                    System.IO.File.WriteAllText(path, csvContent, System.Text.Encoding.UTF8);
                    _view.NotificarExito(string.Format(msgExito, path));
                }
                catch (BLL.Exceptions.BLLException ex)
                {
                    _view.MostrarError(ex.Message);
                }
                catch (Exception ex)
                {
                    _view.MostrarError(string.Format(LanguageBLL.Translate("Inventario_Gestion_Error_ExportarInventario"), ex.Message));
                }
            }
        }
    }
}
