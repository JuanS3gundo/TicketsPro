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
    public class FormNuevoEquipoPresenter
    {
        private readonly IFormNuevoEquipoView _view;
        private readonly IEquipoBLL _equipoBLL;
        public FormNuevoEquipoPresenter(IFormNuevoEquipoView view, IEquipoBLL equipoBLL)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _equipoBLL = equipoBLL ?? throw new ArgumentNullException(nameof(equipoBLL));
        }
        public void Iniciar()
        {
            try
            {
                using (var uow = UnitOfWorkFactory.Current.Create())
                {
                    var tiposEquipo = uow.Repositories.TipoEquipoRepository.GetAll().ToList();
                    _view.LlenarTiposEquipo(tiposEquipo);
                }
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                _view.MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                _view.MostrarError(string.Format(LanguageBLL.Translate("Equipo_Error_IniciarFormulario"), ex.Message));
            }
        }
        public void GuardarEquipo()
        {
            string tituloValidacion = LanguageBLL.Translate("Validacion");
            string msgCamposObligatorios = LanguageBLL.Translate("Inventario_NuevoEquipo_Error_CamposObligatorios");
            if (string.IsNullOrWhiteSpace(_view.NroInventario) || string.IsNullOrWhiteSpace(_view.Modelo))
            {
                _view.MostrarAdvertencia(msgCamposObligatorios, tituloValidacion);
                return;
            }
            Guid? ubicacionId = null;
            Guid? procesadorCatId = null;
            Guid? ramCatId = null;
            Guid? romCatId = null;
            try
            {
                using (var uow = UnitOfWorkFactory.Current.Create())
                {
                    var ubicacionNombre = (_view.Ubicacion ?? "").Trim();
                    var ubicacion = uow.Repositories.UbicacionEquipoRepository.GetAll()
                        .FirstOrDefault(u => u.Nombre.Equals(ubicacionNombre, StringComparison.OrdinalIgnoreCase))
                        ?? uow.Repositories.UbicacionEquipoRepository.GetAll().FirstOrDefault();
                    ubicacionId = ubicacion?.Id;
                    var categorias = uow.Repositories.CategoriaItemRepository.GetAll().ToList();
                    procesadorCatId = categorias.FirstOrDefault(c => c.Nombre.Equals("Procesador", StringComparison.OrdinalIgnoreCase))?.Id;
                    ramCatId = categorias.FirstOrDefault(c => c.Nombre.Equals("RAM", StringComparison.OrdinalIgnoreCase))?.Id;
                    romCatId = categorias.FirstOrDefault(c => c.Nombre.Equals("ROM", StringComparison.OrdinalIgnoreCase))?.Id;
                }
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                _view.MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                _view.MostrarError(string.Format(LanguageBLL.Translate("Equipo_Error_ConsultarDependencias"), ex.Message));
                return;
            }
            if (!ubicacionId.HasValue)
            {
                _view.MostrarError(LanguageBLL.Translate("Equipo_Error_UbicacionNoEncontrada"));
                return;
            }
            
            TipoEquipo tipoEquipo = null;
            UbicacionEquipo ubicacionEquipo = null;
            try
            {
                using (var uow = UnitOfWorkFactory.Current.Create())
                {
                    tipoEquipo = uow.Repositories.TipoEquipoRepository.GetById(_view.TipoEquipoId);
                    ubicacionEquipo = uow.Repositories.UbicacionEquipoRepository.GetById(ubicacionId.Value);
                }
            }
            catch (Exception ex)
            {
                _view.MostrarError(string.Format(LanguageBLL.Translate("Equipo_Error_CargarDatos"), ex.Message));
                return;
            }
            var equipo = new EquipoInformatico
            {
                NroInventario = _view.NroInventario.Trim(),
                ModeloEquipo = _view.Modelo.Trim(),
                TipoEquipo = tipoEquipo,
                UbicacionEquipo = ubicacionEquipo,
                FechaCreacion = DateTime.Now,
                Estado = LanguageBLL.Translate("Equipo_Estado_Activo")
            };
            var items = new List<InventarioItem>();
            if (!string.IsNullOrWhiteSpace(_view.Procesador) && procesadorCatId.HasValue)
            {
                
                CategoriaItem categoriaProc = null;
                try
                {
                    using (var uow = UnitOfWorkFactory.Current.Create())
                    {
                        categoriaProc = uow.Repositories.CategoriaItemRepository.GetById(procesadorCatId.Value);
                    }
                }
                catch (Exception ex)
                {
                    _view.MostrarError(string.Format(LanguageBLL.Translate("Equipo_Error_CargarCategoriaProcesador"), ex.Message));
                    return;
                }
                items.Add(new InventarioItem
                {
                    Nombre = _view.Procesador.Trim(),
                    CategoriaItem = categoriaProc,
                    Cantidad = 1,
                    Disponible = false,
                    CodigoInventario = Guid.NewGuid().ToString().Substring(0, 8),
                    FechaIngreso = DateTime.Now,
                    UbicacionEquipo = ubicacionEquipo
                });
            }
            if (ramCatId.HasValue)
            {
                
                CategoriaItem categoriaRam = null;
                try
                {
                    using (var uow = UnitOfWorkFactory.Current.Create())
                    {
                        categoriaRam = uow.Repositories.CategoriaItemRepository.GetById(ramCatId.Value);
                    }
                }
                catch (Exception ex)
                {
                    _view.MostrarError(string.Format(LanguageBLL.Translate("Equipo_Error_CargarCategoriaRAM"), ex.Message));
                    return;
                }
                foreach (var ramDto in _view.RamModules)
                {
                    items.Add(new InventarioItem
                    {
                        Nombre = ramDto.Nombre,
                        CategoriaItem = categoriaRam,
                        Valor = ramDto.Valor,
                        Unidad = ramDto.Unidad,
                        Cantidad = 1,
                        Disponible = false,
                        CodigoInventario = Guid.NewGuid().ToString().Substring(0, 8),
                        FechaIngreso = DateTime.Now,
                        UbicacionEquipo = ubicacionEquipo
                    });
                }
            }
            if (romCatId.HasValue)
            {
                
                CategoriaItem categoriaRom = null;
                try
                {
                    using (var uow = UnitOfWorkFactory.Current.Create())
                    {
                        categoriaRom = uow.Repositories.CategoriaItemRepository.GetById(romCatId.Value);
                    }
                }
                catch (Exception ex)
                {
                    _view.MostrarError(string.Format(LanguageBLL.Translate("Equipo_Error_CargarCategoriaROM"), ex.Message));
                    return;
                }
                foreach (var romDto in _view.RomModules)
                {
                    items.Add(new InventarioItem
                    {
                        Nombre = romDto.Nombre,
                        CategoriaItem = categoriaRom,
                        Valor = romDto.Valor,
                        Unidad = romDto.Unidad,
                        Cantidad = 1,
                        Disponible = false,
                        CodigoInventario = Guid.NewGuid().ToString().Substring(0, 8),
                        FechaIngreso = DateTime.Now,
                        UbicacionEquipo = ubicacionEquipo
                    });
                }
            }
            if (items.Count == 0)
            {
                _view.MostrarAdvertencia(LanguageBLL.Translate("Equipo_Error_ComponentesObligatorios"), tituloValidacion);
                return;
            }
            try
            {
                bool exito = _equipoBLL.CrearEquipoConItems(equipo, items, out string mensaje);
                if (!exito)
                {
                    _view.MostrarError(mensaje);
                    return;
                }
                _view.NotificarExito(LanguageBLL.Translate("Inventario_NuevoEquipo_Msg_Exito"));
                _view.CerrarPantalla();
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                _view.MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                _view.MostrarError(string.Format(LanguageBLL.Translate("Equipo_Error_GuardarEquipo"), ex.Message));
            }
        }
    }
}
