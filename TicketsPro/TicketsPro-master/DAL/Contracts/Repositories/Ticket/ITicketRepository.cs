using Entity.Domain;
using System;
using System.Collections.Generic;
namespace DAL.Contracts.Repositories
{
    public interface ITicketRepository : IGenericRepository<Ticket, Guid>
    {
        IEnumerable<Ticket> GetTicketsByEstadoId(Guid estadoId);
        IEnumerable<Ticket> GetTicketsByCategoriaId(Guid categoriaId);
        IEnumerable<Ticket> GetTicketsByPrioridadId(Guid prioridadId);
        void UpdateHash(Guid id, string hash);
    }
}
