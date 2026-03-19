using DAL.Contracts;
using Entity.Domain;
using System;
using System.Collections.Generic;
namespace DAL.Contracts.Repositories
{
    public interface ITipoEquipoRepository : IGenericRepository<TipoEquipo, Guid>
    {
        TipoEquipo GetByNombre(string nombre);
    }
}
