using Entity.Domain;
using System;
namespace DAL.Contracts.Repositories
{
    public interface IPrioridadTicketRepository : IGenericRepository<PrioridadTicket, Guid>
    {
        PrioridadTicket GetByNombre(string nombre);
    }
}
