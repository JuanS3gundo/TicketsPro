using Entity.Domain;
using System;
namespace DAL.Contracts.Repositories
{
    public interface IUbicacionRepository : IGenericRepository<Ubicacion, Guid>
    {
        Ubicacion GetByNombre(string nombre);
    }
}
