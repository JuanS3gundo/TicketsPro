using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Enums
    {
        public enum Categoria
        {
            Software,
            hardware,
            actualizaciones,
            problemasVarios, 
            conectividad
        }   
        public enum Estado
        {
            Nuevo,
            Asignado,
            Cerrado
        }   
        public enum Ubicacion
        {
            RRHH,
            Compras, 
            Ventas,
            Marketing,
            Administracion
        }
    }
}
