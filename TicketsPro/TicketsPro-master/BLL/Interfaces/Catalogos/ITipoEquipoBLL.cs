using Entity.Domain;
using System;
using System.Collections.Generic;
namespace BLL.Interfaces
{
    public interface ITipoEquipoBLL
    {
        List<TipoEquipo> ObtenerTiposEquipo();
        bool CrearTipoEquipo(TipoEquipo tipoEquipo, out string mensaje);
        bool ActualizarTipoEquipo(Guid id, string nuevoNombre, out string mensaje);
        bool EliminarTipoEquipo(Guid id, out string mensaje);
    }
}
