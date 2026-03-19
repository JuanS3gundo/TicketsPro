using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Services.DomainModel
{
    public class Bitacora
    {
        public Guid Id { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Usuario { get; set; }
        public string Accion { get; set; }
        public string Detalle { get; set; }
        public string Nivel { get; set; }  
        public int? EquipoId { get; set; }
        public Guid? TicketId { get; set; }  
    }
}
