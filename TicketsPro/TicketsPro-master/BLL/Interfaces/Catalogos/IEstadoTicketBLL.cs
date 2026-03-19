using Entity.Domain;
using System;
using System.Collections.Generic;
namespace BLL.Interfaces
{
    public interface IEstadoTicketBLL
    {
        List<EstadoTicket> ObtenerEstados();
        bool CrearEstado(EstadoTicket estado, out string mensaje);
        bool ActualizarEstado(Guid id, string nuevoNombre, out string mensaje);
        bool EliminarEstado(Guid id, out string mensaje);
    }
}
