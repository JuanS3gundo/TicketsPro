using Entity.Domain;
using System;
namespace DAL.Contracts.Repositories
{
    public interface IUsuarioRepository : IGenericRepository<Usuario, Guid>
    {
        Usuario GetByUserName(string userName);
    }
}
