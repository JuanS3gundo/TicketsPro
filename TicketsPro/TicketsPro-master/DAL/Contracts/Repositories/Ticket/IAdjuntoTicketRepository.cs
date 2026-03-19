using Entity.Domain;
using System;
using System.Collections.Generic;
namespace DAL.Contracts.Repositories
{
    public interface IAdjuntoTicketRepository : IGenericRepository<AdjuntoTicket, Guid>
    {
        IEnumerable<AdjuntoTicket> GetByTicketId(Guid ticketId);
    }
}
