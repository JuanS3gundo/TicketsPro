using System;
using System.Collections.Generic;
using BLL.Interfaces;
using Entity.Domain;
using Services.BLL;
using TicketPro.MVP.Views;
namespace TicketPro.MVP.Presenters
{
    public class FormEditarCategoriaPresenter
    {
        private readonly IFormEditarCategoriaView _view;
        private readonly ISLABLL _slaBLL;
        private CategoriaTicket _categoriaEdicion;
        private bool _esNueva;
        public FormEditarCategoriaPresenter(IFormEditarCategoriaView view, ISLABLL slaBLL)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _slaBLL = slaBLL ?? throw new ArgumentNullException(nameof(slaBLL));
        }
        public void Iniciar(CategoriaTicket categoriaExistente = null)
        {
            _categoriaEdicion = categoriaExistente ?? new CategoriaTicket { Id = Guid.NewGuid() };
            _esNueva = categoriaExistente == null;
            _view.TituloFormulario = _esNueva ? 
                LanguageBLL.Translate("Nueva_Categoria") : 
                LanguageBLL.Translate("Editar_Categoria");
            CargarSLAs();
            if (!_esNueva)
            {
                _view.NombreCategoria = _categoriaEdicion.Nombre;
                _view.DescripcionCategoria = _categoriaEdicion.Descripcion ?? "";
                _view.SLASeleccionado = _categoriaEdicion.PoliticaSLA?.Id;
            }
        }
        private void CargarSLAs()
        {
            try
            {
                var slas = _slaBLL.ObtenerTodas();
                var items = new List<object>
                {
                    new { Id = (Guid?)null, Nombre = "(Ninguna - usar SLA de prioridad)" }
                };
                foreach (var sla in slas)
                {
                    items.Add(new { Id = (Guid?)sla.Id, Nombre = $"{sla.Nombre} ({sla.HorasAtencion}h resp / {sla.HorasResolucion}h resol)" });
                }
                _view.CargarOpcionesSLA(items);
            }
            catch (Exception)
            {
            }
        }
        public void Aceptar()
        {
            string nombre = _view.NombreCategoria?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(nombre))
            {
                _view.MostrarMensajeValidacion(LanguageBLL.Translate("Nombre_Obligatorio"));
                return;
            }
            _categoriaEdicion.Nombre = nombre;
            _categoriaEdicion.Descripcion = _view.DescripcionCategoria?.Trim() ?? "";
            
            if (_view.SLASeleccionado.HasValue)
            {
                _categoriaEdicion.PoliticaSLA = _slaBLL.ObtenerPorId(_view.SLASeleccionado.Value);
            }
            else
            {
                _categoriaEdicion.PoliticaSLA = null;
            }
            _view.CategoriaResultante = _categoriaEdicion;
            _view.CerrarConExito();
        }
        public void Cancelar()
        {
            _view.CerrarCancelado();
        }
    }
}
