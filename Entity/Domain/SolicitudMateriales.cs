using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class SolicitudMateriales
    {
        public int IdSolicitud { get; set; }
        public string DescripcionPedido { get; set; }
        public EncargadoDeposito Encargado { get; set; }
    }
}
