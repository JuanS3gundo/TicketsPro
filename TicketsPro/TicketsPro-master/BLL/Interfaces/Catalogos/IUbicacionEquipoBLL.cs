using Entity.Domain;
using System;
using System.Collections.Generic;
namespace BLL.Interfaces
{
    public interface IUbicacionEquipoBLL
    {
        List<UbicacionEquipo> ObtenerUbicacionesEquipo();
        bool CrearUbicacionEquipo(UbicacionEquipo ubicacionEquipo, out string mensaje);
        bool ActualizarUbicacionEquipo(Guid id, string nuevoNombre, out string mensaje);
        bool EliminarUbicacionEquipo(Guid id, out string mensaje);
    }
}
