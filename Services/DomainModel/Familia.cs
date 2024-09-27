using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DomainModel
{
    public class Familia
    {
        public string Nombre { get; set; }
        public List <Patente> Patentes { get; set; }
        public List<Familia> Familias { get; set; }
    }
}
