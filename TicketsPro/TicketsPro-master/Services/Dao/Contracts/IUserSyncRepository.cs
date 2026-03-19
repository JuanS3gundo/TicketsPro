using Services.DomainModel;
using System;

namespace Services.Dao.Contracts
{
    /// <summary>
    /// Abstracción del repositorio de usuarios para sincronización.
    /// Permite que UserSyncService opere con Services.DomainModel.Usuario
    /// sin depender de DAL.Contracts.UnitOfWork ni de Entity.Domain.Usuario.
    /// La implementación concreta debe ser inyectada mediante
    /// UserSyncService.UserRepository al arranque de la aplicación.
    /// </summary>
    public interface IUserSyncRepository
    {
        Usuario GetById(Guid id);
        Usuario GetByUserName(string userName);
        void Add(Usuario usuario);
        void Update(Usuario usuario);
    }
}
