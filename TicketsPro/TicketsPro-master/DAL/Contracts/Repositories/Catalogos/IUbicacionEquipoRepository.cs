using DAL.Contracts;
using Entity.Domain;
using System;
using System.Collections.Generic;
namespace DAL.Contracts.Repositories
{
    public interface IUbicacionEquipoRepository : IGenericRepository<UbicacionEquipo, Guid>
    {
        UbicacionEquipo GetByNombre(string nombre);
    }
}
