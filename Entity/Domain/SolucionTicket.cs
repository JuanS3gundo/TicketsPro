using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class SolucionTicket
    {
        public int IdSolucion { get; set; }
        public Ticket ticket { get; set; }  
        public DateTime FechaCierre { get; set; }
        public string DescripcionSolucion { get; set; }

    }
}
