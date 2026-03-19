using BLL.Interfaces;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Controller
{
    public class DatosController
    {
        private readonly IEquipoService _equipoService;
        public DatosController(IEquipoService equipoService)
        {
            _equipoService = equipoService;
        }
        public List<EquipoInformatico> GetEquipos() { return _equipoService.ObtenerTodos(); }
    }
}
