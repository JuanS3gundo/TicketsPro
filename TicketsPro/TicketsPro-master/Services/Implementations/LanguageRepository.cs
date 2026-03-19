using Services.Facade.ExceptionsManager;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace Services.Implementations
{
    public class LanguageRepository
    {
        private readonly ILoggerService _loggerService;
        private static readonly string BasePath = GetLanguagePath();
        private static string GetLanguagePath()
        {
            string relativePath = ConfigurationManager.AppSettings["LanguagePath"] ?? @".\I18n\";
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string absolutePath = Path.Combine(baseDirectory, relativePath);
            return absolutePath;
        }
        private static string CurrentFile =>
            Path.Combine(BasePath, Thread.CurrentThread.CurrentUICulture.Name + ".lang");
        public static string Translate(string key)
        {
            try
            {
                if (int.TryParse(key, out _))
                    return key;
                if (!Directory.Exists(BasePath))
                {
                    Console.WriteLine($"[LANG]  Directorio de idiomas no existe: {BasePath}");
                    return key; 
                }
                if (!File.Exists(CurrentFile))
                {
                    Console.WriteLine($"[LANG]  Archivo de idioma no existe: {CurrentFile}");
                    return key; 
                }
                var lines = File.ReadAllLines(CurrentFile);
                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line) || !line.Contains("="))
                        continue;
                    var parts = line.Split('=');
                    if (parts.Length < 2)
                        continue;
                    var k = parts[0].Trim();
                    var v = parts[1].Trim();
                    if (string.Equals(k, key, StringComparison.OrdinalIgnoreCase))
                        return v;
                }
                Console.WriteLine($"[LANG]  Clave NO encontrada: '{key}'");
                return key; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[LANG]  Error al traducir '{key}': {ex.Message}");
                return key; 
            }
        }
        public static void WriteKey(string key)
        {
            if (!File.Exists(CurrentFile))
                File.WriteAllText(CurrentFile, "");
            File.AppendAllText(CurrentFile, $"{key}=\n");
        }
        public static List<string> GetLanguages()
        {
            if (!Directory.Exists(BasePath))
                return new List<string>();
            return Directory.GetFiles(BasePath, "*.lang")
                           .Select(f => Path.GetFileNameWithoutExtension(f))
                           .ToList();
        }
        public static string GetDebugInfo()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"BaseDirectory: {AppDomain.CurrentDomain.BaseDirectory}");
            sb.AppendLine($"LanguagePath Config: {ConfigurationManager.AppSettings["LanguagePath"]}");
            sb.AppendLine($"BasePath Calculado: {BasePath}");
            sb.AppendLine($"CurrentUICulture: {Thread.CurrentThread.CurrentUICulture.Name}");
            sb.AppendLine($"CurrentFile: {CurrentFile}");
            sb.AppendLine($"Directory Exists: {Directory.Exists(BasePath)}");
            sb.AppendLine($"File Exists: {File.Exists(CurrentFile)}");
            if (Directory.Exists(BasePath))
            {
                var files = Directory.GetFiles(BasePath, "*.lang");
                sb.AppendLine($"Archivos .lang encontrados: {files.Length}");
                foreach (var file in files)
                {
                    sb.AppendLine($"  - {Path.GetFileName(file)}");
                }
            }
            return sb.ToString();
        }
    }
}
