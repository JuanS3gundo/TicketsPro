using Services.DomainModel;
using Services.DomainModel.Composite;
using Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
namespace Services.Services
{
    public class LoginService
    {
        private readonly IExceptionService _exceptionService;
        public LoginService(IExceptionService exceptionService)
        {
            _exceptionService = exceptionService ?? throw new ArgumentNullException(nameof(exceptionService));
        }
        public bool Register(Usuario user, Guid familiaDefaultId)
        {
            try
            {
                if (user.IdUsuario == Guid.Empty)
                    user.IdUsuario = Guid.NewGuid();
                user.Password = CryptographyService.HashPassword(user.Password);
                using (var tx = new TransactionScope())
                {
                    if (!UsuarioRepository.Current.Add(user))
                        return false;
                    var relacion = new UsuarioFamilia
                    {
                        IdUsuario = user.IdUsuario,
                        IdFamilia = familiaDefaultId
                    };
                    if (!UsuarioFamiliaRepository.Current.Add(relacion))
                        return false;
                    tx.Complete();
                }
                UserSyncService.SyncUser(user);
                return true;
            }
            catch (Exception ex)
            {
                _exceptionService.Handle(ex, new ExceptionContext { OperationName = "LoginService" });
                throw;
            }
        }
        public Usuario GetUsuarioConAccesos(string userName, string password)
        {
            try
            {
                var userDb = UsuarioRepository.Current.GetByUserName(userName);
                if (userDb == null) return null;
                bool needsUpgrade;
                bool ok = CryptographyService.ComparePassword(password, userDb.Password, out needsUpgrade);
                if (!ok) return null;
                if (needsUpgrade)
                {
                    userDb.Password = CryptographyService.HashPassword(password);
                    UsuarioRepository.Current.Update(userDb);
                }
                var accesos = new List<Acceso>();
                var familias = UsuarioFamiliaRepository.Current.GetByUsuario(userDb.IdUsuario);
                foreach (var uf in familias)
                {
                    var fam = FamiliaRepository.Current.GetById(uf.IdFamilia);
                    if (fam != null) accesos.Add(fam);
                }
                var patentes = UsuarioPatenteRepository.Current.GetByUsuario(userDb.IdUsuario);
                foreach (var up in patentes)
                {
                    var pat = PatenteRepository.Current.GetById(up.IdPatente);
                    if (pat != null) accesos.Add(pat);
                }
                userDb.Accesos = accesos;
                userDb.Password = null;
                UserSyncService.SyncUser(userDb);
                return userDb;
            }
            catch (Exception ex)
            {
                _exceptionService.Handle(ex, new ExceptionContext { OperationName = "LoginService" });
                throw;
            }
        }
    }
}
