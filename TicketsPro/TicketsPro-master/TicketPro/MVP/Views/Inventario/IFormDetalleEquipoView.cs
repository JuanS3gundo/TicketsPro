using Entity.Domain;
using Services.DomainModel;
using System.Collections.Generic;
namespace TicketPro.MVP.Views
{
    public interface IFormDetalleEquipoView
    {
        int EquipoId { get; }
        string Modelo { get; set; }
        string NroInventario { get; set; }
        string UsuarioAsignado { set; }
        string Procesador { set; }
        string RAM { set; }
        string ROM { set; }
        InventarioItem ItemAsignadoSeleccionado { get; }
        InventarioItem ItemDisponibleSeleccionado { get; }
        Services.DomainModel.Usuario UsuarioSeleccionado { get; }
        void LlenarUsuarios(IEnumerable<Services.DomainModel.Usuario> usuarios);
        void LlenarItemsAsignados(IEnumerable<InventarioItem> items, string labelText);
        void LlenarItemsDisponibles(IEnumerable<InventarioItem> items, string labelText);
        void MostrarMensaje(string mensaje, string titulo = "Informacion");
        void MostrarError(string mensaje, string titulo = "Error");
        void CerrarConExito();
        void CerrarPantalla();
    }
}
