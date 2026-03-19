using System;
using System.Collections.Generic;
namespace Entity.Domain
{
    public class Usuario
    {
        public Guid IdUsuario { get; set; }
        public string UserName { get; set; }
        public ICollection<Ticket> TicketsCreados { get; set; }
        public ICollection<Ticket> TicketsAsignados { get; set; }
        public ICollection<EquipoInformatico> EquiposAsignados { get; set; }
    }
}
