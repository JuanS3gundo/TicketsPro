using Services.DomainModel;
using Services.Facade.ExceptionsManager;
using Services.Implementations;
using Services.Services;
using System;
using System.Diagnostics;
using System.IO;
namespace Services.BLL
{
    public static class LanguageBLL
    {
        private static string _lastLanguage = "es-ES";
        public static string LastLanguage => _lastLanguage;
        public static string Translate(string key)
        {
            try
            {
                if (int.TryParse(key, out _))
                    return key;
                if (key.Length <= 1)
                    return key;
                return LanguageRepository.Translate(key);
            }
            catch (WordNotFoundException)
            {
                System.Diagnostics.Debug.WriteLine($"[LANG] Clave NO encontrada: '{key}'");
                RegistrarBitacora(
                    usuario: SessionService.GetUsuario()?.UserName ?? "Sistema",
                    accion: "Clave de idioma faltante",
                    detalle: $"La clave '{key}' no existe en el archivo .lang",
                    nivel: "Warning",
                    logMensaje: $"[LANG] Clave de idioma faltante: {key}"
                );
                try
                {
                }
                catch (Exception)
                {
                }
                return $"[{key}]";
            }
            catch (FileNotFoundException ex)
            {
                RegistrarBitacora(
                    usuario: SessionService.GetUsuario()?.UserName ?? "Sistema",
                    accion: "Archivo de idioma faltante",
                    detalle: ex.Message,
                    nivel: "Error",
                    logMensaje: $"[LANG] Archivo de idioma faltante: {ex.Message}"
                );
                return $"[{key}]";
            }
            catch (DirectoryNotFoundException ex)
            {
                RegistrarBitacora(
                    usuario: SessionService.GetUsuario()?.UserName ?? "Sistema",
                    accion: "Carpeta de idiomas faltante",
                    detalle: ex.Message,
                    nivel: "Error",
                    logMensaje: $"[LANG] Carpeta I18n no encontrada: {ex.Message}"
                );
                return $"[{key}]";
            }
            catch (Exception ex)
            {
                RegistrarBitacora(
                    usuario: SessionService.GetUsuario()?.UserName ?? "Sistema",
                    accion: "Error inesperado en traduccion",
                    detalle: ex.Message,
                    nivel: "Error",
                    logMensaje: $"[LANG] Error inesperado traduciendo clave '{key}': {ex.Message}"
                );
                return $"[{key}]";
            }
        }
        public static void SetLanguage(string lang)
        {
            try
            {
                _lastLanguage = lang;     
                LanguageManager.SetLanguage(lang);
                RegistrarBitacora(
                    usuario: SessionService.GetUsuario()?.UserName ?? "Sistema",
                    accion: "Cambio de idioma",
                    detalle: $"Idioma establecido a {lang}",
                    nivel: "Info",
                    logMensaje: $"[LANG] Idioma cambiado a {lang}"
                );
            }
            catch (Exception ex)
            {
                LoggerService.Instance.LogError($"[LANG] Error al cambiar idioma a '{lang}'", ex);
            }
        }
        public static string GetCurrentLanguage()
        {
            return LanguageManager.CurrentLanguage;
        }
        private static void RegistrarBitacora(string usuario, string accion, string detalle, string nivel, string logMensaje)
        {
            if (nivel == "Error")
            {
                LoggerService.Instance.LogError(logMensaje);
            }
            BitacoraRepository.Instance.Agregar(new Bitacora
            {
                Id = Guid.NewGuid(),
                Fecha = DateTime.Now,
                Usuario = usuario,
                Accion = accion,
                Detalle = detalle,
                Nivel = nivel
            });
        }
    }
}
