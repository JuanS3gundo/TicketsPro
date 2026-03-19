using DAL.Contracts.UnitOfWork;
using Services.Dao.Contracts;
using Services.DomainModel;
using System;
using System.Linq;
using UsuarioTicketsPro = Entity.Domain.Usuario;
using UsuarioServicesPP = Services.DomainModel.Usuario;

namespace DAL.Implementations.SqlServer.Repositories
{
    /// <summary>
    /// Implementación concreta de IUserSyncRepository.
    /// Usa el UnitOfWorkFactory interno para operar sobre la tabla real
    /// de usuarios (Entity.Domain.Usuario) mapeando desde y hacia el DTO
    /// Usuario (de Services.DomainModel).
    /// </summary>
    public class UserSyncRepositoryAdapter : IUserSyncRepository
    {
        public void Add(UsuarioServicesPP usuario)
        {
            var unitOfWork = UnitOfWorkFactory.Current;
            using (var uow = unitOfWork.Create())
            {
                var usuarioRepo = uow.Repositories.UsuarioRepository;
                var newUser = new UsuarioTicketsPro
                {
                    IdUsuario = usuario.IdUsuario,
                    UserName = usuario.UserName
                };
                usuarioRepo.Add(newUser);
                uow.SaveChanges();
            }
        }

        public UsuarioServicesPP GetById(Guid id)
        {
            var unitOfWork = UnitOfWorkFactory.Current;
            using (var uow = unitOfWork.Create())
            {
                var existingUser = uow.Repositories.UsuarioRepository.GetById(id);
                if (existingUser != null)
                {
                    return new UsuarioServicesPP
                    {
                        IdUsuario = existingUser.IdUsuario,
                        UserName = existingUser.UserName
                    };
                }
                return null;
            }
        }

        public UsuarioServicesPP GetByUserName(string userName)
        {
            var unitOfWork = UnitOfWorkFactory.Current;
            using (var uow = unitOfWork.Create())
            {
                var existingUser = uow.Repositories.UsuarioRepository.GetAll()
                    .FirstOrDefault(u => u.UserName != null && u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));
                
                if (existingUser != null)
                {
                    return new UsuarioServicesPP
                    {
                        IdUsuario = existingUser.IdUsuario,
                        UserName = existingUser.UserName
                    };
                }
                return null;
            }
        }

        public void Update(UsuarioServicesPP usuario)
        {
            var unitOfWork = UnitOfWorkFactory.Current;
            using (var uow = unitOfWork.Create())
            {
                var existingUser = uow.Repositories.UsuarioRepository.GetById(usuario.IdUsuario);
                if (existingUser != null)
                {
                    existingUser.UserName = usuario.UserName;
                    uow.Repositories.UsuarioRepository.Update(existingUser);
                    uow.SaveChanges();
                }
            }
        }
    }
}
