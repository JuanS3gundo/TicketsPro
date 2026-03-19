using Entity;
using Entity.Domain;
using System.Collections.Generic;
namespace TicketPro.MVP.Views
{
    public interface IFormGestionInventarioView
    {
        string FiltroEquipoSeleccionado { get; }
        string FiltroItemSeleccionado { get; }
        string BusquedaEquipo { get; }
        string BusquedaItem { get; }
        void LlenarFiltros(string[] filtrosEquipos, string[] filtrosItems);
        void RenderizarEquipos(IEnumerable<EquipoInformatico> equiposFiltrados, IEnumerable<InventarioItem> todosLosItems);
        void RenderizarItems(IEnumerable<InventarioItem> itemsFiltrados, IEnumerable<EquipoInformatico> todosLosEquipos);
        void MostrarError(string mensaje, string titulo = "Error");
        void MostrarMensaje(string mensaje, string titulo = "Informacion");
        bool ConfirmarAccion(string mensaje, string titulo = "Confirmar");
        void NotificarExito(string mensaje, string titulo = "Exito");
        string SolicitarRutaExportacionExcel(string defaultFileName);
    }
}
