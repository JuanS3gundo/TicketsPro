using BLL.Interfaces;
using DAL.Contracts.UnitOfWork;
using Entity.Domain;
using Services.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using TicketPro.MVP.Views;
namespace TicketPro.MVP.Presenters
{
    public class FormNuevoItemPresenter
    {
        private readonly IFormNuevoItemView _view;
        private readonly ICategoriaItemBLL _categoriaBLL;
        private readonly IInventarioItemBLL _itemBLL;
        private readonly List<int> _potenciasDos = new List<int>
        {
            1, 2, 4, 8, 16, 32, 64, 128, 256, 512,
            1024, 2048, 4096, 8192, 16384
        };
        public FormNuevoItemPresenter(IFormNuevoItemView view, ICategoriaItemBLL categoriaBLL, IInventarioItemBLL itemBLL)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _categoriaBLL = categoriaBLL ?? throw new ArgumentNullException(nameof(categoriaBLL));
            _itemBLL = itemBLL ?? throw new ArgumentNullException(nameof(itemBLL));
        }
        public void Iniciar()
        {
            string[] categorias = new string[]
            {
                LanguageBLL.Translate("Item_Categoria_Procesador"),
                LanguageBLL.Translate("Item_Categoria_RAM"),
                LanguageBLL.Translate("Item_Categoria_ROM"),
                LanguageBLL.Translate("Item_Categoria_Monitor"),
                LanguageBLL.Translate("Item_Categoria_Teclado"),
                LanguageBLL.Translate("Item_Categoria_Mouse"),
                LanguageBLL.Translate("Item_Categoria_Fuente"),
                LanguageBLL.Translate("Item_Categoria_Otro")
            };
            _view.LlenarCategorias(categorias);
        }
        public void AlCambiarCategoria()
        {
            string categoria = _view.CategoriaSeleccionada;
            bool esMemoria =
                categoria.Equals(LanguageBLL.Translate("Item_Categoria_RAM"), StringComparison.OrdinalIgnoreCase) ||
                categoria.Equals(LanguageBLL.Translate("Item_Categoria_ROM"), StringComparison.OrdinalIgnoreCase);
            string[] unidades = esMemoria ? new string[] { "MB", "GB", "TB" } : new string[0];
            _view.ConfigurarControlesMemoria(esMemoria, _potenciasDos, unidades);
        }
        public void GuardarItem()
        {
            string tituloValidacion = LanguageBLL.Translate("Validacion");
            if (string.IsNullOrWhiteSpace(_view.Codigo) || string.IsNullOrWhiteSpace(_view.Nombre))
            {
                _view.MostrarAdvertencia(LanguageBLL.Translate("Inventario_NuevoItem_Error_CamposObligatorios"), tituloValidacion);
                return;
            }
            string categoriaOriginal = _view.CategoriaSeleccionada;
            string categoriaNormalizada = categoriaOriginal;
            if (categoriaOriginal.Equals(LanguageBLL.Translate("Item_Categoria_RAM"), StringComparison.OrdinalIgnoreCase))
            {
                categoriaNormalizada = "RAM";
            }
            else if (categoriaOriginal.Equals(LanguageBLL.Translate("Item_Categoria_ROM"), StringComparison.OrdinalIgnoreCase))
            {
                categoriaNormalizada = "ROM";
            }
            bool esMemoria = categoriaNormalizada == "RAM" || categoriaNormalizada == "ROM";
            var categorias = _categoriaBLL.ObtenerTodas();
            var categoriaDb = categorias.FirstOrDefault(c =>
                c.Nombre.Equals(categoriaNormalizada, StringComparison.OrdinalIgnoreCase));
            if (categoriaDb == null)
            {
                _view.MostrarAdvertencia(LanguageBLL.Translate("Inventario_NuevoItem_Error_CategoriaNoEncontrada"), tituloValidacion);
                return;
            }
            Guid? ubicacionEquipoId = null;
            string ubicacionTexto = (_view.Ubicacion ?? "").Trim();
            if (!string.IsNullOrWhiteSpace(ubicacionTexto))
            {
                var ubicaciones = _itemBLL.ObtenerUbicacionesEquipo();
                var ubicacionDb = ubicaciones.FirstOrDefault(u =>
                    u.Nombre.Equals(ubicacionTexto, StringComparison.OrdinalIgnoreCase));
                if (ubicacionDb != null)
                    ubicacionEquipoId = ubicacionDb.Id;
            }
            
            UbicacionEquipo ubicacionEquipo = null;
            if (ubicacionEquipoId.HasValue)
            {
                using (var uow = UnitOfWorkFactory.Current.Create())
                {
                    ubicacionEquipo = uow.Repositories.UbicacionEquipoRepository.GetById(ubicacionEquipoId.Value);
                }
            }
            var item = new InventarioItem
            {
                CodigoInventario = _view.Codigo.Trim(),
                Nombre = _view.Nombre.Trim(),
                CategoriaItem = categoriaDb,
                Cantidad = _view.Cantidad,
                UbicacionEquipo = ubicacionEquipo,
                Descripcion = _view.Descripcion.Trim(),
                FechaIngreso = DateTime.Now,
                Disponible = _view.Disponible,
                Valor = esMemoria && _view.ValorSeleccionado != null
                            ? Convert.ToInt32(_view.ValorSeleccionado)
                            : 0,
                Unidad = esMemoria && _view.UnidadSeleccionada != null
                            ? _view.UnidadSeleccionada.ToString()
                            : string.Empty
            };
            bool exito = _itemBLL.AgregarItem(item, out string mensaje);
            string tituloExito = LanguageBLL.Translate("Exito");
            string tituloAdvertencia = LanguageBLL.Translate("Advertencia");
            if (exito)
            {
                _view.NotificarExitoYAsignarItem($"aœ… {mensaje}", tituloExito, item);
            }
            else
            {
                _view.MostrarAdvertencia($"aš i¸ {mensaje}", tituloAdvertencia);
            }
        }
    }
}
