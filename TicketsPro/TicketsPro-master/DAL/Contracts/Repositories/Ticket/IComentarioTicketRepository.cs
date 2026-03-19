using Entity.Domain;
using System;
using System.Collections.Generic;
namespace DAL.Contracts.Repositories
{
    public interface IComentarioTicketRepository : IGenericRepository<ComentarioTicket, Guid>
    {
        IEnumerable<ComentarioTicket> GetByTicketId(Guid ticketId);
        IEnumerable<ComentarioTicket> GetComentariosPublicosByTicketId(Guid ticketId);
    }
}
