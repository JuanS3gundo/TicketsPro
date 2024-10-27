using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Ticket
    {
        public Guid IdTicket { get; set; }   
        public string Titulo { get; set; }
        public string Descripcion { get; set; } 
        public DateTime FechaApertura { get; set; }
        public Enums.Categoria Categoria { get; set; }
        public Enums.Estado Estado { get; set; }
        public Enums.Ubicacion Ubicacion { get; set; }  
        public Tecnico TecnicoAsignado { get; set; }
        public SolucionTicket Solucion { get; set; }
        public EquipoInformatico equipo { get; set; }   

    }
}
