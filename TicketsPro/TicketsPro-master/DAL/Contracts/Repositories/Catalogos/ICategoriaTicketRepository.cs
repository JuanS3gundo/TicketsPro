using Entity.Domain;
using System;
namespace DAL.Contracts.Repositories
{
    public interface ICategoriaTicketRepository : IGenericRepository<CategoriaTicket, Guid>
    {
        CategoriaTicket GetByNombre(string nombre);
    }
}
