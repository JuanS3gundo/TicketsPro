using System;
using System.Collections.Generic;
namespace Entity.Domain
{
    public class EquipoInformatico
    {
        public int Id { get; set; }
        public string NroInventario { get; set; }
        public string ModeloEquipo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Estado { get; set; }
        public TipoEquipo TipoEquipo { get; set; }
        public UbicacionEquipo UbicacionEquipo { get; set; }
        public Usuario UsuarioAsignado { get; set; }
        public ICollection<InventarioItem> Componentes { get; set; } = new List<InventarioItem>();
        public ICollection<Ticket> TicketsAsignados { get; set; } = new List<Ticket>();
    }
}
