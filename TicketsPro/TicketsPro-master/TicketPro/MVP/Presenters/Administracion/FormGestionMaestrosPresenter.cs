using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interfaces;
using Entity.Domain;
using Services.BLL;
using Services.DTOs;
using TicketPro.MVP.Views;
namespace TicketPro.MVP.Presenters
{
    public class FormGestionMaestrosPresenter
    {
        private readonly IFormGestionMaestrosView _view;
        private readonly IPrioridadBLL _prioridadBLL;
        private readonly IEstadoTicketBLL _estadoBLL;
        private readonly ICategoriaTicketBLL _categoriaBLL;
        private readonly IUbicacionBLL _ubicacionBLL;
        private readonly ITipoEquipoBLL _tipoEquipoBLL;
        private readonly IUbicacionEquipoBLL _ubicacionEquipoBLL;
        private readonly ICategoriaItemBLL _categoriaItemBLL;
        public FormGestionMaestrosPresenter(
            IFormGestionMaestrosView view,
            IPrioridadBLL prioridadBLL,
            IEstadoTicketBLL estadoBLL,
            ICategoriaTicketBLL categoriaBLL,
            IUbicacionBLL ubicacionBLL,
            ITipoEquipoBLL tipoEquipoBLL,
            IUbicacionEquipoBLL ubicacionEquipoBLL,
            ICategoriaItemBLL categoriaItemBLL)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _prioridadBLL = prioridadBLL ?? throw new ArgumentNullException(nameof(prioridadBLL));
            _estadoBLL = estadoBLL ?? throw new ArgumentNullException(nameof(estadoBLL));
            _categoriaBLL = categoriaBLL ?? throw new ArgumentNullException(nameof(categoriaBLL));
            _ubicacionBLL = ubicacionBLL ?? throw new ArgumentNullException(nameof(ubicacionBLL));
            _tipoEquipoBLL = tipoEquipoBLL ?? throw new ArgumentNullException(nameof(tipoEquipoBLL));
            _ubicacionEquipoBLL = ubicacionEquipoBLL ?? throw new ArgumentNullException(nameof(ubicacionEquipoBLL));
            _categoriaItemBLL = categoriaItemBLL ?? throw new ArgumentNullException(nameof(categoriaItemBLL));
        }
        public void Iniciar()
        {
            var tiposMaestro = new List<object>
            {
                new { Clave = "Prioridades", Nombre = LanguageBLL.Translate("Maestro_Prioridades") },
                new { Clave = "Estados", Nombre = LanguageBLL.Translate("Maestro_Estados") },
                new { Clave = "Categorias", Nombre = LanguageBLL.Translate("Maestro_Categorias") },
                new { Clave = "Ubicaciones", Nombre = LanguageBLL.Translate("Maestro_Ubicaciones") },
                new { Clave = "TiposEquipo", Nombre = LanguageBLL.Translate("Maestro_TiposEquipo") },
                new { Clave = "UbicacionesEquipo", Nombre = LanguageBLL.Translate("Maestro_UbicacionesEquipo") },
                new { Clave = "CategoriasItem", Nombre = LanguageBLL.Translate("Maestro_CategoriasItem") }
            };
            _view.CargarTiposMaestro(tiposMaestro);
        }
        public void CambiarTipoMaestro()
        {
            var tipoSeleccionado = _view.TipoMaestroActual;
            if (string.IsNullOrEmpty(tipoSeleccionado)) return;
            CargarMaestro(tipoSeleccionado);
            _view.ConfigurarOpcionesSLA(tipoSeleccionado == "Prioridades" || tipoSeleccionado == "Categorias");
        }
        private void CargarMaestro(string tipoMaestro)
        {
            try
            {
                var items = new List<DisplayItemDTO>();
                switch (tipoMaestro)
                {
                    case "Prioridades":
                        var prioridades = _prioridadBLL.ObtenerTodas();
                        items = prioridades.Select(p => new DisplayItemDTO(p.Id, p.Nombre, "")).ToList();
                        break;
                    case "Estados":
                        var estados = _estadoBLL.ObtenerEstados();
                        items = estados.Select(e => new DisplayItemDTO(e.Id, e.Nombre, e.Descripcion)).ToList();
                        break;
                    case "Categorias":
                        var categorias = _categoriaBLL.ObtenerCategorias();
                        items = categorias.Select(c => new DisplayItemDTO(c.Id, c.Nombre, c.Descripcion)).ToList();
                        break;
                    case "Ubicaciones":
                        var ubicaciones = _ubicacionBLL.ObtenerUbicaciones();
                        items = ubicaciones.Select(u => new DisplayItemDTO(u.Id, u.Nombre, u.Descripcion)).ToList();
                        break;
                    case "TiposEquipo":
                        var tiposEquipo = _tipoEquipoBLL.ObtenerTiposEquipo();
                        items = tiposEquipo.Select(t => new DisplayItemDTO(t.Id, t.Nombre, t.Descripcion)).ToList();
                        break;
                    case "UbicacionesEquipo":
                        var ubicacionesEquipo = _ubicacionEquipoBLL.ObtenerUbicacionesEquipo();
                        items = ubicacionesEquipo.Select(u => new DisplayItemDTO(u.Id, u.Nombre, u.Descripcion)).ToList();
                        break;
                    case "CategoriasItem":
                        var categoriasItem = _categoriaItemBLL.ObtenerTodas();
                        items = categoriasItem.Select(c => new DisplayItemDTO(c.Id, c.Nombre, c.Descripcion)).ToList();
                        break;
                }
                _view.MostrarItems(items.OrderBy(i => i.Display).ToList(), items.Count);
            }
            catch (Exception ex)
            {
                _view.MostrarError("Error al cargar maestros", ex);
            }
        }
        public void Agregar()
        {
            try
            {
                string tipoActual = _view.TipoMaestroActual;
                if (tipoActual == "Categorias")
                {
                    var categoriaNueva = _view.MostrarFormularioNuevaCategoria();
                    if (categoriaNueva != null)
                    {
                        if (_categoriaBLL.CrearCategoria(categoriaNueva, out string msgCat))
                        {
                            _view.MostrarMensajeExito(LanguageBLL.Translate("Item_Creado_OK"));
                            CargarMaestro(tipoActual);
                        }
                        else
                        {
                            _view.MostrarError(msgCat);
                        }
                    }
                    return;
                }
                string titulo = LanguageBLL.Translate("Nuevo_Item");
                string prompt = LanguageBLL.Translate("Ingrese_Nombre_Item");
                string nombre = _view.PedirNombreNuevoItem(titulo, prompt);
                if (string.IsNullOrWhiteSpace(nombre)) return;
                bool exito = false;
                string mensaje = "";
                switch (tipoActual)
                {
                    case "Prioridades":
                        exito = _prioridadBLL.Crear(new PrioridadTicket { Id = Guid.NewGuid(), Nombre = nombre.Trim(), CodigoColor = "#808080", NivelPeso = 1 }, out mensaje);
                        break;
                    case "Estados":
                        exito = _estadoBLL.CrearEstado(new EstadoTicket { Id = Guid.NewGuid(), Nombre = nombre.Trim(), Descripcion = "" }, out mensaje);
                        break;
                    case "Ubicaciones":
                        exito = _ubicacionBLL.CrearUbicacion(new Ubicacion { Id = Guid.NewGuid(), Nombre = nombre.Trim(), Descripcion = "" }, out mensaje);
                        break;
                    case "TiposEquipo":
                        exito = _tipoEquipoBLL.CrearTipoEquipo(new TipoEquipo { Id = Guid.NewGuid(), Nombre = nombre.Trim(), Descripcion = "" }, out mensaje);
                        break;
                    case "UbicacionesEquipo":
                        exito = _ubicacionEquipoBLL.CrearUbicacionEquipo(new UbicacionEquipo { Id = Guid.NewGuid(), Nombre = nombre.Trim(), Descripcion = "" }, out mensaje);
                        break;
                    case "CategoriasItem":
                        exito = _categoriaItemBLL.CrearCategoriaItem(new CategoriaItem { Id = Guid.NewGuid(), Nombre = nombre.Trim(), Descripcion = "" }, out mensaje);
                        break;
                }
                if (exito)
                {
                    _view.MostrarMensajeExito(LanguageBLL.Translate("Item_Creado_OK"));
                    CargarMaestro(tipoActual);
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
                _view.MostrarError("Error", ex);
            }
        }
        public void Editar()
        {
            try
            {
                var itemSeleccionado = _view.ItemSeleccionado as DisplayItemDTO;
                if (itemSeleccionado == null)
                {
                    _view.MostrarMensajeValidacion(LanguageBLL.Translate("Seleccione_Item_Editar"));
                    return;
                }
                string tipoActual = _view.TipoMaestroActual;
                if (tipoActual == "Categorias")
                {
                    var catActual = _categoriaBLL.ObtenerCategorias().FirstOrDefault(c => c.Id == itemSeleccionado.Id);
                    if (catActual == null)
                    {
                        _view.MostrarError(LanguageBLL.Translate("Categoria_No_Encontrada"));
                        return;
                    }
                    var categoriaEditada = _view.MostrarFormularioEdicionCategoria(catActual);
                    if (categoriaEditada != null)
                    {
                        if (_categoriaBLL.ActualizarCategoria(categoriaEditada, out string msgCat))
                        {
                            _view.MostrarMensajeExito(LanguageBLL.Translate("Item_Editado_OK"));
                            CargarMaestro(tipoActual);
                        }
                        else
                        {
                            _view.MostrarError(msgCat);
                        }
                    }
                    return;
                }
                string titulo = LanguageBLL.Translate("Editar_Item");
                string prompt = LanguageBLL.Translate("Ingrese_Nuevo_Nombre");
                string nuevoNombre = _view.PedirNuevoNombreEdicion(titulo, prompt, itemSeleccionado.Display);
                if (string.IsNullOrWhiteSpace(nuevoNombre) || nuevoNombre == itemSeleccionado.Display) return;
                bool exito = false;
                string mensaje = "";
                switch (tipoActual)
                {
                    case "Prioridades":
                        exito = _prioridadBLL.Actualizar(itemSeleccionado.Id, nuevoNombre.Trim(), out mensaje);
                        break;
                    case "Estados":
                        exito = _estadoBLL.ActualizarEstado(itemSeleccionado.Id, nuevoNombre.Trim(), out mensaje);
                        break;
                    case "Ubicaciones":
                        exito = _ubicacionBLL.ActualizarUbicacion(itemSeleccionado.Id, nuevoNombre.Trim(), out mensaje);
                        break;
                    case "TiposEquipo":
                        exito = _tipoEquipoBLL.ActualizarTipoEquipo(itemSeleccionado.Id, nuevoNombre.Trim(), out mensaje);
                        break;
                    case "UbicacionesEquipo":
                        exito = _ubicacionEquipoBLL.ActualizarUbicacionEquipo(itemSeleccionado.Id, nuevoNombre.Trim(), out mensaje);
                        break;
                    case "CategoriasItem":
                        exito = _categoriaItemBLL.ActualizarCategoriaItem(itemSeleccionado.Id, nuevoNombre.Trim(), out mensaje);
                        break;
                }
                if (exito)
                {
                    _view.MostrarMensajeExito(LanguageBLL.Translate("Item_Editado_OK"));
                    CargarMaestro(tipoActual);
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
                _view.MostrarError("Error", ex);
            }
        }
        public void Eliminar()
        {
            try
            {
                var itemSeleccionado = _view.ItemSeleccionado as DisplayItemDTO;
                if (itemSeleccionado == null)
                {
                    _view.MostrarMensajeValidacion(LanguageBLL.Translate("Seleccione_Item_Eliminar"));
                    return;
                }
                string tipoActual = _view.TipoMaestroActual;
                if (!_view.PedirConfirmacionEliminacion(itemSeleccionado.Display)) return;
                bool exito = false;
                string mensaje = "";
                switch (tipoActual)
                {
                    case "Prioridades":
                        exito = _prioridadBLL.Eliminar(itemSeleccionado.Id, out mensaje);
                        break;
                    case "Estados":
                        exito = _estadoBLL.EliminarEstado(itemSeleccionado.Id, out mensaje);
                        break;
                    case "Categorias":
                        exito = _categoriaBLL.EliminarCategoria(itemSeleccionado.Id, out mensaje);
                        break;
                    case "Ubicaciones":
                        exito = _ubicacionBLL.EliminarUbicacion(itemSeleccionado.Id, out mensaje);
                        break;
                    case "TiposEquipo":
                        exito = _tipoEquipoBLL.EliminarTipoEquipo(itemSeleccionado.Id, out mensaje);
                        break;
                    case "UbicacionesEquipo":
                        exito = _ubicacionEquipoBLL.EliminarUbicacionEquipo(itemSeleccionado.Id, out mensaje);
                        break;
                    case "CategoriasItem":
                        exito = _categoriaItemBLL.EliminarCategoriaItem(itemSeleccionado.Id, out mensaje);
                        break;
                }
                if (exito)
                {
                    _view.MostrarMensajeExito(LanguageBLL.Translate("Item_Eliminado_OK"));
                    CargarMaestro(tipoActual);
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
                _view.MostrarError("Error", ex);
            }
        }
        public void GestionarSLA()
        {
            try
            {
                _view.AbrirFormularioSLA();
                if (_view.TipoMaestroActual == "Prioridades" || _view.TipoMaestroActual == "Categorias")
                {
                    CargarMaestro(_view.TipoMaestroActual);
                }
            }
            catch (Exception ex)
            {
                _view.MostrarError("Error al gestionar SLA", ex);
            }
        }
    }
}
