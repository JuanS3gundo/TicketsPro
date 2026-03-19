using Entity.Domain;
using System;
namespace DAL.Contracts.Repositories
{
    public interface IEstadoTicketRepository : IGenericRepository<EstadoTicket, Guid>
    {
        EstadoTicket GetByNombre(string nombre);
    }
}
