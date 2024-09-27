using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DomainModel
{
    public class Usuario
    {
        public string Nombre { get; set; }  
        public string Password { get; set; }    
        public List<Familia> Familias { get; set; } 
        public List <Patente> Patents { get; set; } 
    }
}
