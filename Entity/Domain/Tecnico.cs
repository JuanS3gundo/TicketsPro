using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Tecnico : Empleado
    {
        public int TicketsResueltos { get; set; }
        public int IdTecnico { get; set; }  
    }
}
