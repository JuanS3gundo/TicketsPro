using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entity.Enums;

namespace BLL
{
    public interface ITicketService
    {
        void CrearTicket(Ticket ticket);
        Ticket ObtenerTicketPorId(Guid id);
        IEnumerable<Ticket> ObtenerTicketsPorEstado(Estado estado);
        void ActualizarTicket(Ticket ticket);
        void AsignarTecnico(Guid ticketId, int tecnicoId);
        void CambiarEstado(Guid ticketId, Estado nuevoEstado);
    }
}
