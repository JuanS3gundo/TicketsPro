using System;
using System.Collections.Generic;
namespace TicketPro.MVP.Views
{
    public interface IFormClienteView
    {
        Guid IdUsuario { get; }
        string EstadoSeleccionado { get; }
        string TextoBusqueda { get; }
        void MostrarUsuario(string nombreUsuario);
        void CargarCombosEstados(IEnumerable<string> estados);
        void MostrarMensajeAdvertencia(string mensaje, string titulo = "Advertencia");
        void MostrarMensajeError(string mensaje, string titulo = "Error");
        void MostrarError(string mensaje, Exception ex = null);
        void ReiniciarAplicacion();
        void MostrarTickets(List<Entity.Domain.Ticket> tickets);
    }
}
