using Services.DomainModel;
using Services.DomainModel.Composite;
using Services.Implementations;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
namespace Services.BLL
{
    public class UsuarioAccesoBLL
    {
        private readonly UsuarioRepository _usuarioRepo = UsuarioRepository.Current;
        private readonly FamiliaBLL _familiaBLL = new FamiliaBLL();
        private readonly PatenteBLL _patenteBLL = new PatenteBLL();
        #region Singleton
        private static readonly UsuarioAccesoBLL _instance = new UsuarioAccesoBLL();
        public static UsuarioAccesoBLL Instance => _instance;
        private UsuarioAccesoBLL() { }
        #endregion
        public List<Usuario> GetAllUsuarios() =>
            _usuarioRepo.GetAll().OrderBy(u => u.UserName).ToList();
        public (List<Familia> familiasAsignadas, List<Familia> familiasDisponibles,
                List<Patente> patentesAsignadas, List<Patente> patentesDisponibles)
            CargarAccesosUsuario(Guid idUsuario)
        {
            var todasFam = _familiaBLL.GetAll();
            var famUser = UsuarioFamiliaRepository.Current.GetByUsuario(idUsuario);
            var idsFamUser = new HashSet<Guid>(famUser.Select(f => f.IdFamilia));
            var todasPat = _patenteBLL.GetAll();
            var patUser = UsuarioPatenteRepository.Current.GetByUsuario(idUsuario);
            var idsPatUser = new HashSet<Guid>(patUser.Select(p => p.IdPatente));
            return (
                familiasAsignadas: todasFam.Where(f => idsFamUser.Contains(f.IdFamilia)).ToList(),
                familiasDisponibles: todasFam.Where(f => !idsFamUser.Contains(f.IdFamilia)).ToList(),
                patentesAsignadas: todasPat.Where(p => idsPatUser.Contains(p.idPatente)).ToList(),
                patentesDisponibles: todasPat.Where(p => !idsPatUser.Contains(p.idPatente)).ToList()
            );
        }
        public void GuardarCambios(Guid idUsuario, List<Guid> familias, List<Guid> patentes)
        {
            var usuarioActual = SessionService.GetUsuario()?.UserName ?? "(desconocido)";
            try
            {
                var usuario = UsuarioRepository.Current.GetById(idUsuario);
                var nombreUsuario = usuario != null ? usuario.UserName : "(usuario no encontrado)";
                UsuarioFamiliaRepository.Current.Remove(idUsuario);
                familias.ForEach(id =>
                {
                    UsuarioFamiliaRepository.Current.Add(new UsuarioFamilia
                    {
                        IdUsuario = idUsuario,
                        IdFamilia = id
                    });
                });
                UsuarioPatenteRepository.Current.Remove(idUsuario);
                patentes.ForEach(id =>
                {
                    UsuarioPatenteRepository.Current.Add(new UsuarioPatente
                    {
                        IdUsuario = idUsuario,
                        IdPatente = id
                    });
                });
                var nombresFamilias = FamiliaRepository.Current.GetAll()
                    .Where(f => familias.Contains(f.IdFamilia))
                    .Select(f => f.NombreFamilia);
                var nombresPatentes = PatenteRepository.Current.GetAll()
                    .Where(p => patentes.Contains(p.idPatente))
                    .Select(p => p.Nombre);
                BitacoraService.RegistrarEvento(new Bitacora
                {
                    Usuario = usuarioActual,
                    Accion = "ACTUALIZACION_ACCESOS_USUARIO",
                    Detalle = $"Se actualizaron los accesos del usuario '{nombreUsuario}' (ID: {idUsuario}). " +
                              $"Familias: [{string.Join(", ", nombresFamilias)}]. " +
                              $"Patentes: [{string.Join(", ", nombresPatentes)}]",
                    Nivel = "Info"
                });
            }
            catch (Exception ex)
            {
                BitacoraService.RegistrarEvento(new Bitacora
                {
                    Usuario = usuarioActual,
                    Accion = "ERROR_ACTUALIZACION_ACCESOS_USUARIO",
                    Detalle = $"Error al actualizar accesos del usuario (ID: {idUsuario}): {ex.Message}",
                    Nivel = "Error"
                });
                LoggerService.Instance.LogError($"Error al actualizar los accesos del usuario con ID {idUsuario} por {usuarioActual}", ex);
                throw;
            }
        }
        public bool UsuarioPerteneceAFamilia(Guid idUsuario, Guid IdFamilia)
        {
            var familiasUsuario = UsuarioFamiliaRepository.Current.GetByUsuario(idUsuario);
            return familiasUsuario.Any(f => f.IdFamilia == IdFamilia);
        }
        public bool UsuarioTienePatente(Guid idUsuario, Guid idPatente)
        {
            var familiasUsuario = UsuarioFamiliaRepository.Current.GetByUsuario(idUsuario);
            foreach (var fam in familiasUsuario)
            {
                var patentesFamilia = FamiliaRepository.Current.GetPatentesPorFamilia(fam.IdFamilia);
                if (patentesFamilia.Any(p => p.idPatente == idPatente))
                    return true;
            }
            var patentesUsuario = UsuarioPatenteRepository.Current.GetByUsuario(idUsuario);
            return patentesUsuario.Any(p => p.IdPatente == idPatente);
        }
        public bool UsuarioTieneFamilia(Usuario usuario, string nombreFamilia)
        {
            if (usuario == null || string.IsNullOrWhiteSpace(nombreFamilia))
                return false;
            var familiasUsuario = UsuarioFamiliaRepository.Current.GetByUsuario(usuario.IdUsuario);
            var idsFamiliasUsuario = familiasUsuario.Select(f => f.IdFamilia).ToList();
            var familia = FamiliaRepository.Current.GetAll()
                .FirstOrDefault(f => f.NombreFamilia.Equals(nombreFamilia, StringComparison.OrdinalIgnoreCase)
                                     && idsFamiliasUsuario.Contains(f.IdFamilia));
            return familia != null;
        }
        public bool UsuarioTieneFamilias(Usuario usuario, params string[] nombresFamilias)
        {
            if (usuario == null || nombresFamilias == null || nombresFamilias.Length == 0)
                return false;
            var familiasUsuario = UsuarioFamiliaRepository.Current.GetByUsuario(usuario.IdUsuario);
            var idsFamiliasUsuario = familiasUsuario.Select(f => f.IdFamilia).ToList();
            var todasFamilias = FamiliaRepository.Current.GetAll();
            return todasFamilias.Any(f =>
                nombresFamilias.Contains(f.NombreFamilia, StringComparer.OrdinalIgnoreCase)
                && idsFamiliasUsuario.Contains(f.IdFamilia));
        }
        public void CrearFamilia(string nombre) => _familiaBLL.Add(nombre);
        public void EliminarFamilia(Guid id) => _familiaBLL.Delete(id);
        public void CrearPatente(string nombre) => _patenteBLL.Add(nombre);
        public void EliminarPatente(Guid id) => _patenteBLL.Delete(id);
    }
}
