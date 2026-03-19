using DAL.Contracts;
using Entity.Domain;
using System;
using System.Collections.Generic;
namespace DAL.Contracts.Repositories
{
    public interface ICategoriaItemRepository : IGenericRepository<CategoriaItem, Guid>
    {
        CategoriaItem GetByNombre(string nombre);
    }
}
