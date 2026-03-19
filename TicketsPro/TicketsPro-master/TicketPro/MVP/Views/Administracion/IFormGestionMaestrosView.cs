using System;
using Entity.Domain;
using Services.DTOs;

namespace TicketPro.MVP.Views
{
    public interface IFormGestionMaestrosView
    {
        string TipoMaestroActual { get; }
        object ItemSeleccionado { get; }
        void CargarTiposMaestro(object tiposMaestro);
        void MostrarItems(object items, int cantidad);
        void ConfigurarOpcionesSLA(bool visible); 
        string PedirNombreNuevoItem(string titulo, string prompt);
        string PedirNuevoNombreEdicion(string titulo, string prompt, string valorActual);
        bool PedirConfirmacionEliminacion(string nombreItem);
        CategoriaTicket MostrarFormularioNuevaCategoria();
        CategoriaTicket MostrarFormularioEdicionCategoria(CategoriaTicket categoriaActual);
        void AbrirFormularioSLA();
        void MostrarMensajeExito(string mensaje);
        void MostrarMensajeValidacion(string mensaje);
        void MostrarError(string mensaje, Exception ex = null);
    }
}
