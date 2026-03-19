using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Services.DomainModel
{
    public class Recuperacion
    {
        public int IdRecuperacion { get; set; }
        public string Codigo { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public string Email { get; set; }
    }
}
