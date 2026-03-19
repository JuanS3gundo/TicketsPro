using BLL.Interfaces;
using DAL.Contracts.UnitOfWork;
using Entity.Domain;
using Services.BLL;
using Services.DomainModel;
using System;
using System.Linq;
using TicketPro.MVP.Views;
namespace TicketPro.MVP.Presenters
{
    public class FormDetalleEquipoPresenter
    {
        private readonly IFormDetalleEquipoView _view;
        private readonly IEquipoBLL _equipoBLL;
        private readonly IInventarioItemBLL _itemBLL;
        private readonly UsuarioBLL _usuarioBLL;
        private EquipoInformatico _equipo;
        public FormDetalleEquipoPresenter(IFormDetalleEquipoView view, IEquipoBLL equipoBLL, IInventarioItemBLL itemBLL, UsuarioBLL usuarioBLL)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _equipoBLL = equipoBLL ?? throw new ArgumentNullException(nameof(equipoBLL));
            _itemBLL = itemBLL ?? throw new ArgumentNullException(nameof(itemBLL));
            _usuarioBLL = usuarioBLL ?? throw new ArgumentNullException(nameof(usuarioBLL));
        }
        public void Iniciar()
        {
            CargarUsuarios();
            CargarEquipo();
            CargarListas();
        }
        private void CargarUsuarios()
        {
            var usuarios = _usuarioBLL.GetAll();
            _view.LlenarUsuarios(usuarios);
        }
        public void CargarEquipo()
        {
            string msgNoEquipo = LanguageBLL.Translate("Inventario_DetalleEquipo_Msg_EquipoNoEncontrado");
            string sinProcesador = LanguageBLL.Translate("Sin_Procesador");
            _equipo = _equipoBLL.BuscarPorId(_view.EquipoId);
            if (_equipo == null)
            {
                _view.MostrarError(msgNoEquipo);
                _view.CerrarPantalla();
                return;
            }
            if (_equipo.UsuarioAsignado != null)
            {
                
                if (string.IsNullOrEmpty(_equipo.UsuarioAsignado.UserName))
                {
                    using (var uow = UnitOfWorkFactory.Current.Create())
                    {
                        _equipo.UsuarioAsignado = uow.Repositories.UsuarioRepository.GetById(_equipo.UsuarioAsignado.IdUsuario);
                    }
                }
                _view.UsuarioAsignado = _equipo.UsuarioAsignado?.UserName ?? "Sin usuario";
            }
            else
            {
                _view.UsuarioAsignado = "Sin usuario";
            }
            _view.Modelo = _equipo.ModeloEquipo;
            _view.NroInventario = _equipo.NroInventario;
            var items = _itemBLL.ObtenerTodos()
                .Where(i => i.EquipoAsignado != null && i.EquipoAsignado.Id == _equipo.Id)
                .ToList();
            _view.Procesador =
                items.FirstOrDefault(i => (i.CategoriaItem?.Nombre ?? string.Empty).Equals("Procesador", StringComparison.OrdinalIgnoreCase))?.Nombre
                ?? sinProcesador;
            _view.RAM = items.Where(i => i.CategoriaItem?.Nombre == "RAM").Sum(i => i.Valor).ToString();
            _view.ROM = items.Where(i => i.CategoriaItem?.Nombre == "ROM").Sum(i => i.Valor).ToString();
        }
        public void CargarListas()
        {
            if (_equipo == null) return;
            string asignadosLabel = LanguageBLL.Translate("Inventario_DetalleEquipo_LabelAsignados");
            string disponiblesLabel = LanguageBLL.Translate("Inventario_DetalleEquipo_LabelDisponibles");
            var todos = _itemBLL.ObtenerTodos();
            var asignados = todos
                .Where(i => i.EquipoAsignado != null && i.EquipoAsignado.Id == _equipo.Id)
                .OrderBy(x => x.Nombre)
                .ToList();
            var disponibles = todos
                .Where(i => i.EquipoAsignado == null)
                .OrderBy(x => x.Nombre)
                .ToList();
            _view.LlenarItemsAsignados(asignados, $"{asignadosLabel} ({asignados.Count})");
            _view.LlenarItemsDisponibles(disponibles, $"{disponiblesLabel} ({disponibles.Count})");
        }
        public void AsignarItem()
        {
            var item = _view.ItemDisponibleSeleccionado;
            if (item != null)
            {
                if (_itemBLL.AsignarAEquipo(item.Id, _equipo.Id, out string mensaje))
                {
                    CargarListas();
                    CargarEquipo();
                }
                else
                {
                    _view.MostrarError(mensaje);
                }
            }
        }
        public void QuitarItem()
        {
            var item = _view.ItemAsignadoSeleccionado;
            if (item != null)
            {
                if (_itemBLL.QuitarDeEquipo(item.Id, out string mensaje))
                {
                    CargarListas();
                    CargarEquipo();
                }
                else
                {
                    _view.MostrarError(mensaje);
                }
            }
        }
        public void AsignarUsuario()
        {
            var usuario = _view.UsuarioSeleccionado;
            if (usuario != null)
            {
                bool ok = _equipoBLL.AsignarUsuario(_equipo.Id, usuario.IdUsuario, out string mensaje);
                if (ok)
                {
                    _view.MostrarMensaje("Usuario asignado correctamente.", "Exito");
                    CargarEquipo();
                }
                else
                {
                    _view.MostrarError(mensaje);
                }
            }
        }
        public void QuitarUsuario()
        {
            if (_equipo == null) return;
            bool ok = _equipoBLL.QuitarAsignacionUsuario(_equipo.Id, out string mensaje);
            if (ok)
            {
                _view.MostrarMensaje("Usuario quitado del equipo.", "Exito");
                CargarEquipo();
            }
            else
            {
                _view.MostrarError(mensaje);
            }
        }
        public void GuardarCambios()
        {
            if (_equipo == null) return;
            string tituloExito = LanguageBLL.Translate("Exito");
            string msgExito = LanguageBLL.Translate("Inventario_DetalleEquipo_Msg_ExitoActualizar");
            _equipo.ModeloEquipo = _view.Modelo?.Trim();
            _equipo.NroInventario = _view.NroInventario?.Trim();
            bool exito = _equipoBLL.Actualizar(_equipo, out string mensaje);
            if (exito)
            {
                _view.MostrarMensaje(msgExito, tituloExito);
                _view.CerrarConExito();
            }
            else
            {
                _view.MostrarError(mensaje);
            }
        }
    }
}
