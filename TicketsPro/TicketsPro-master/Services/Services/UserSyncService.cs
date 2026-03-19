using Services.Dao.Contracts;
using Services.DomainModel;
using Services.Implementations;
using System;
using System.Linq;
using UsuarioServicesPP = Services.DomainModel.Usuario;

namespace Services.Services
{
    /// <summary>
    /// Servicio de sincronización de usuarios entre sistemas.
    /// Usa IUserSyncRepository por inyección — la implementación concreta
    /// debe asignarse al arranque de la app:
    ///   UserSyncService.UserRepository = new UserSyncRepositoryAdapter(...);
    /// </summary>
    public static class UserSyncService
    {
        /// <summary>
        /// Repositorio de sincronización de usuarios inyectado externamente al arranque.
        /// </summary>
        public static IUserSyncRepository UserRepository { get; set; }

        private static IUserSyncRepository GetRepository()
        {
            if (UserRepository == null)
                throw new InvalidOperationException(
                    "[UserSyncService] UserRepository no está configurado. " +
                    "Asigná UserSyncService.UserRepository al iniciar la aplicación.");
            return UserRepository;
        }

        public static bool SyncUser(UsuarioServicesPP usuario)
        {
            if (usuario == null || usuario.IdUsuario == Guid.Empty)
                return false;
            try
            {
                var repo = GetRepository();
                var existingUser = repo.GetById(usuario.IdUsuario);
                if (existingUser == null)
                    existingUser = repo.GetByUserName(usuario.UserName);

                if (existingUser == null)
                {
                    repo.Add(new UsuarioServicesPP
                    {
                        IdUsuario = usuario.IdUsuario,
                        UserName = usuario.UserName
                    });
                    Console.WriteLine($"[UserSyncService] Usuario '{usuario.UserName}' sincronizado exitosamente.");
                    return true;
                }
                else
                {
                    if (existingUser.UserName != usuario.UserName)
                    {
                        existingUser.UserName = usuario.UserName;
                        repo.Update(existingUser);
                        Console.WriteLine($"[UserSyncService] Usuario '{usuario.UserName}' actualizado exitosamente.");
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UserSyncService] Error sincronizando usuario '{usuario.UserName}': {ex.Message}");
                try
                {
                    var exceptionService = new ExceptionService(LoggerService.Instance);
                    exceptionService.Handle(ex, new ExceptionContext { OperationName = "UserSyncService.SyncUser" });
                }
                catch (Exception logEx)
                {
                    Console.WriteLine($"[UserSyncService] Error al registrar excepcion: {logEx.Message}");
                }
                return false;
            }
        }

        public static bool SyncUser(Guid idUsuario, string userName)
        {
            if (idUsuario == Guid.Empty || string.IsNullOrWhiteSpace(userName))
                return false;
            var usuario = new UsuarioServicesPP
            {
                IdUsuario = idUsuario,
                UserName = userName
            };
            return SyncUser(usuario);
        }

        public static int SyncAllUsers()
        {
            int syncCount = 0;
            try
            {
                var allUsers = UsuarioRepository.Current.GetAll();
                foreach (var user in allUsers)
                {
                    if (SyncUser(user))
                    {
                        syncCount++;
                    }
                }
                Console.WriteLine($"[UserSyncService] {syncCount} usuarios sincronizados exitosamente.");
                return syncCount;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UserSyncService] Error en sincronizacion masiva: {ex.Message}");
                return syncCount;
            }
        }
    }
}
