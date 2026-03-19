using Entity.Domain;
using System;
using System.Collections.Generic;
using TicketPro.MVP.DTOs;

namespace TicketPro.MVP.Views
{
    public interface IFormNuevoEquipoView
    {
        string NroInventario { get; }
        string Modelo { get; }
        string Ubicacion { get; }
        Guid TipoEquipoId { get; }
        string Procesador { get; }
        IEnumerable<ModuloInventarioDTO> RamModules { get; }
        IEnumerable<ModuloInventarioDTO> RomModules { get; }
        void LlenarTiposEquipo(IEnumerable<TipoEquipo> tipos);
        void MostrarError(string mensaje, string titulo = "Error");
        void MostrarAdvertencia(string mensaje, string titulo = "Advertencia");
        void NotificarExito(string mensaje, string titulo = "Exito");
        void CerrarPantalla();
    }
}
