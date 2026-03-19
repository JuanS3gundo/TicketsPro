using Services;
using Services.DomainModel;
using Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Services.Services
{
    public static class SessionService
    {
        private static Usuario _usuarioActual;
        private static HashSet<string> _permisosDataKey =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        public static void SetUsuario(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));
            _usuarioActual = usuario;
            CargarPermisos(usuario.IdUsuario);
        }
        public static void Clear()
        {
            _usuarioActual = null;
            _permisosDataKey.Clear();
        }
        public static bool IsLogged()
        {
            return _usuarioActual != null;
        }
        public static Usuario GetUsuario()
        {
            return _usuarioActual;
        }
        public static void RefreshPermisos()
        {
            if (_usuarioActual == null)
                return;
            CargarPermisos(_usuarioActual.IdUsuario);
        }
        private static void CargarPermisos(Guid userId)
        {
            var patentes = PatenteRepository.Current.GetEfectivasByUsuario(userId);
            if (patentes == null)
                patentes = new List<Patente>();
            _permisosDataKey = new HashSet<string>(
                patentes
                    .Where(p => !string.IsNullOrWhiteSpace(p.DataKey))
                    .Select(p => p.DataKey),
                StringComparer.OrdinalIgnoreCase
            );
        }
        public static bool TienePermiso(string dataKey)
        {
            return !string.IsNullOrWhiteSpace(dataKey) &&
                   _permisosDataKey.Contains(dataKey);
        }
        public static bool TienePermisos(params string[] dataKeys)
        {
            if (dataKeys == null || dataKeys.Length == 0)
                return false;
            return dataKeys.All(k => _permisosDataKey.Contains(k));
        }
        public static bool TieneAlMenosUno(params string[] dataKeys)
        {
            if (dataKeys == null || dataKeys.Length == 0)
                return false;
            return dataKeys.Any(k => _permisosDataKey.Contains(k));
        }
        public static IReadOnlyCollection<string> GetPermisos()
        {
            return _permisosDataKey;
        }
        public static bool EsAdministrador()
        {
            return TienePermiso("ADMIN");
        }
        public static bool Denegado(string dataKey)
        {
            return !TienePermiso(dataKey);
        }
    }
}
