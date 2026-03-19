using Entity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL.Contracts.Repositories
{
    public interface ISolucionTicketRepository : IGenericRepository<SolucionTicket, Guid>
    {
        SolucionTicket GetByTicketId(Guid ticketId);
        List<SolucionTicket> GetAllByTicketId(Guid ticketId);
    }
}
