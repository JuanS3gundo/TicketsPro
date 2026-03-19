using Services.BLL;
using Services.DomainModel;
using Services.Implementations;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
namespace Services.BLL
{
    public class UsuarioBLL
    {
        private readonly UsuarioRepository _usuarioRepo = UsuarioRepository.Current;
        private static readonly Lazy<UsuarioBLL> _instance = new Lazy<UsuarioBLL>(() => new UsuarioBLL());
        public static UsuarioBLL Instance => _instance.Value;
        public UsuarioBLL() { }
        public List<Usuario> GetAll()
        {
            return _usuarioRepo.GetAll()
                .OrderBy(u => u.UserName)
                .ToList();
        }
        public List<Usuario> ObtenerUsuariosPorFamilia(string nombreFamilia)
        {
            if (string.IsNullOrWhiteSpace(nombreFamilia))
                throw new ArgumentException(LanguageBLL.Translate("Usuario_Error_NombreFamiliaObligatorio"), nameof(nombreFamilia));
            var usuarios = _usuarioRepo.GetAll();
            var nombreBuscadoNormalizado = RemoverAcentos(nombreFamilia);
            var usuariosConFamilia = UsuarioFamiliaRepository.Current.GetAll()
                .Where(uf => FamiliaRepository.Current
                    .GetAll()
                    .Any(f => f.IdFamilia == uf.IdFamilia &&
                              RemoverAcentos(f.NombreFamilia).Equals(nombreBuscadoNormalizado, StringComparison.OrdinalIgnoreCase)))
                .Select(uf => uf.IdUsuario)
                .Distinct()
                .ToList();
            return usuarios
                .Where(u => usuariosConFamilia.Contains(u.IdUsuario))
                .OrderBy(u => u.UserName)
                .ToList();
        }
        private static string RemoverAcentos(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return texto;
            var normalizado = texto.Normalize(System.Text.NormalizationForm.FormD);
            var sinAcentos = new System.Text.StringBuilder();
            foreach (var c in normalizado)
            {
                var categoria = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c);
                if (categoria != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sinAcentos.Append(c);
                }
            }
            return sinAcentos.ToString().Normalize(System.Text.NormalizationForm.FormC);
        }
        public void Add(Usuario usuario)
        {
            var usuarioActual = SessionService.GetUsuario()?.UserName ?? "(desconocido)";
            try
            {
                if (usuario == null)
                    throw new ArgumentNullException(nameof(usuario), LanguageBLL.Translate("Usuario_Error_UsuarioNulo"));
                if (string.IsNullOrWhiteSpace(usuario.UserName))
                    throw new ArgumentException(LanguageBLL.Translate("Login_Error_UsuarioObligatorio"), nameof(usuario.UserName));
                if (string.IsNullOrWhiteSpace(usuario.Password))
                    throw new ArgumentException(LanguageBLL.Translate("Login_Error_PasswordObligatoria"), nameof(usuario.Password));
                if (_usuarioRepo.GetAll().Any(u => u.UserName.Equals(usuario.UserName, StringComparison.OrdinalIgnoreCase)))
                    throw new InvalidOperationException(string.Format(LanguageBLL.Translate("Login_Error_UsuarioDuplicado"), usuario.UserName));
                _usuarioRepo.Add(usuario);
                BitacoraService.RegistrarEvento(new Bitacora
                {
                    Usuario = usuarioActual,
                    Accion = "CREACION_USUARIO",
                    Detalle = $"Se creo el usuario '{usuario.UserName}' (ID: {usuario.IdUsuario})",
                    Nivel = "Info"
                });
            }
            catch (Exception ex)
            {
                BitacoraService.RegistrarEvento(new Bitacora
                {
                    Usuario = usuarioActual,
                    Accion = "ERROR_CREACION_USUARIO",
                    Detalle = $"Error al crear usuario '{usuario?.UserName}': {ex.Message}",
                    Nivel = "Error"
                });
                LoggerService.Instance.LogError($"Error al crear usuario '{usuario?.UserName}' por {usuarioActual}", ex);
                throw;
            }
        }
        public List<Familia> ObtenerFamiliasDelUsuario(Guid idUsuario)
        {
            if (idUsuario == Guid.Empty)
                throw new ArgumentException(LanguageBLL.Translate("Usuario_Error_IdObligatorio"), nameof(idUsuario));
            var familiasUsuario = UsuarioFamiliaRepository.Current.GetByUsuario(idUsuario);
            if (familiasUsuario == null || familiasUsuario.Count == 0)
                return new List<Familia>();
            var todasLasFamilias = FamiliaRepository.Current.GetAll();
            return todasLasFamilias
                .Where(f => familiasUsuario.Any(uf => uf.IdFamilia == f.IdFamilia))
                .OrderBy(f => f.NombreFamilia)
                .ToList();
        }
        public void Delete(Guid idUsuario)
        {
            var usuarioActual = SessionService.GetUsuario()?.UserName ?? "(desconocido)";
            var usuario = _usuarioRepo.GetById(idUsuario);
            try
            {
                if (usuario == null)
                    throw new InvalidOperationException(string.Format(LanguageBLL.Translate("Usuario_Error_UsuarioNoEncontrado"), idUsuario));
                bool tieneFamilias = UsuarioFamiliaRepository.Current.GetAll()
                    .Any(uf => uf.IdUsuario == idUsuario);
                bool tienePatentes = UsuarioPatenteRepository.Current.GetAll()
                    .Any(up => up.IdUsuario == idUsuario);
                if (tieneFamilias || tienePatentes)
                    throw new InvalidOperationException(string.Format(LanguageBLL.Translate("Usuario_Error_UsuarioConAsociaciones"), usuario.UserName));
                string nombreUsuario = usuario.UserName;
                _usuarioRepo.Remove(idUsuario);
                BitacoraService.RegistrarEvento(new Bitacora
                {
                    Usuario = usuarioActual,
                    Accion = "ELIMINACION_USUARIO",
                    Detalle = $"Se elimino el usuario '{nombreUsuario}' (ID: {idUsuario})",
                    Nivel = "Info"
                });
            }
            catch (Exception ex)
            {
                BitacoraService.RegistrarEvento(new Bitacora
                {
                    Usuario = usuarioActual,
                    Accion = "ERROR_ELIMINACION_USUARIO",
                    Detalle = $"Error al eliminar usuario '{usuario?.UserName ?? "(no encontrado)"}' (ID: {idUsuario}): {ex.Message}",
                    Nivel = "Error"
                });
                LoggerService.Instance.LogError($"Error al eliminar usuario '{usuario?.UserName ?? "(no encontrado)"}' por {usuarioActual}", ex);
                throw;
            }
        }
        public void Update(Usuario usuario)
        {
            var usuarioActual = SessionService.GetUsuario()?.UserName ?? "(desconocido)";
            try
            {
                if (usuario == null)
                    throw new ArgumentNullException(nameof(usuario));
                if (string.IsNullOrWhiteSpace(usuario.UserName))
                    throw new ArgumentException(LanguageBLL.Translate("Login_Error_UsuarioObligatorio"), nameof(usuario.UserName));
                _usuarioRepo.Update(usuario);
                BitacoraService.RegistrarEvento(new Bitacora
                {
                    Usuario = usuarioActual,
                    Accion = "ACTUALIZACION_USUARIO",
                    Detalle = $"Se actualizo el usuario '{usuario.UserName}' (ID: {usuario.IdUsuario})",
                    Nivel = "Info"
                });
            }
            catch (Exception ex)
            {
                BitacoraService.RegistrarEvento(new Bitacora
                {
                    Usuario = usuarioActual,
                    Accion = "ERROR_ACTUALIZACION_USUARIO",
                    Detalle = $"Error al actualizar usuario '{usuario?.UserName}' (ID: {usuario.IdUsuario}): {ex.Message}",
                    Nivel = "Error"
                });
                LoggerService.Instance.LogError($"Error al actualizar usuario '{usuario?.UserName}' por {usuarioActual}", ex);
                throw;
            }
        }
        public Usuario GetById(Guid idUsuario)
        {
            return _usuarioRepo.GetById(idUsuario);
        }
        public Usuario GetByUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException(LanguageBLL.Translate("Login_Error_UsuarioObligatorio"), nameof(userName));
            return _usuarioRepo.GetAll()
                .FirstOrDefault(u => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
