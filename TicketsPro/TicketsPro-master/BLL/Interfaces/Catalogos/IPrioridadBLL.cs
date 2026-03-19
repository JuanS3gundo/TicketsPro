using Entity.Domain;
using System;
using System.Collections.Generic;
namespace BLL.Interfaces
{
    public interface IPrioridadBLL
    {
        List<PrioridadTicket> ObtenerTodas();
        PrioridadTicket ObtenerPorId(Guid id);
        PrioridadTicket ObtenerPorNombre(string nombre);
        bool Crear(PrioridadTicket prioridad, out string mensaje);
        bool Actualizar(Guid id, string nuevoNombre, out string mensaje);
        bool Eliminar(Guid id, out string mensaje);
    }
}
