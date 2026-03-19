using Entity.Domain;
using System;
using System.Collections.Generic;
namespace BLL.Interfaces
{
    public interface IUbicacionBLL
    {
        List<Ubicacion> ObtenerUbicaciones();
        bool CrearUbicacion(Ubicacion ubicacion, out string mensaje);
        bool ActualizarUbicacion(Guid id, string nuevoNombre, out string mensaje);
        bool EliminarUbicacion(Guid id, out string mensaje);
    }
}
