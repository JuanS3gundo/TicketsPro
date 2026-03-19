using Services.Services;
using System;
using System.IO;
using System.Text;
namespace Services.Implementations
{
    public sealed class LoggerService : ILoggerService
    {
        private static readonly Lazy<LoggerService> _instance = new Lazy<LoggerService>(() => new LoggerService());
        public static LoggerService Instance => _instance.Value;
        private string _logsPath;
        private readonly object _lockObject = new object();
        private LoggerService()
        {
            _logsPath = ResolverRutaLogs();
        }
        private string ResolverRutaLogs()
        {
            string[] candidatas = new[]
            {
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments),
                    "..", "TicketPro", "Logs"),
                Path.Combine(Path.GetTempPath(), "TicketPro", "Logs")
            };
            foreach (var ruta in candidatas)
            {
                if (string.IsNullOrEmpty(ruta)) continue;
                try
                {
                    Directory.CreateDirectory(ruta);
                    string testFile = Path.Combine(ruta, "_test_write.tmp");
                    File.WriteAllText(testFile, "test");
                    File.Delete(testFile);
                    return ruta; 
                }
                catch
                {
                    continue;
                }
            }
            return Path.GetTempPath();
        }
        public void LogError(string message, Exception exception = null)
        {
            WriteLog("ERROR", message, exception);
        }
        public void LogWarning(string message)
        {
            WriteLog("WARNING", message, null);
        }
        public void LogCritical(string message, Exception exception = null)
        {
            WriteLog("CRITICAL", message, exception);
        }
        private void WriteLog(string level, string message, Exception exception)
        {
            try
            {
                if (!Directory.Exists(_logsPath))
                {
                    try { Directory.CreateDirectory(_logsPath); }
                    catch
                    {
                        _logsPath = ResolverRutaLogs();
                    }
                }
                string fileName = $"{DateTime.Now:yyyy-MM-dd}_Errores.log";
                string filePath = Path.Combine(_logsPath, fileName);
                var logEntry = BuildLogEntry(level, message, exception);
                lock (_lockObject)
                {
                    File.AppendAllText(filePath, logEntry, Encoding.UTF8);
                }
            }
            catch (Exception writeEx)
            {
                TryEmergencyWrite(level, message, exception, writeEx);
            }
        }
        private string BuildLogEntry(string level, string message, Exception exception)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}");
            if (exception != null)
            {
                sb.AppendLine($"  Exception Type: {exception.GetType().FullName}");
                sb.AppendLine($"  Exception Message: {exception.Message}");
                if (!string.IsNullOrEmpty(exception.StackTrace))
                {
                    sb.AppendLine("  Stack Trace:");
                    sb.AppendLine(IndentText(exception.StackTrace, "    "));
                }
                if (exception.InnerException != null)
                {
                    sb.AppendLine("  Inner Exception:");
                    sb.AppendLine(IndentText(exception.InnerException.ToString(), "    "));
                }
            }
            sb.AppendLine(new string('-', 80));
            return sb.ToString();
        }
        private string IndentText(string text, string indent)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            var lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            return indent + string.Join(Environment.NewLine + indent, lines);
        }
        private void TryEmergencyWrite(string level, string message, Exception originalEx, Exception writeEx)
        {
            try
            {
                string emergencyFile = Path.Combine(
                    Path.GetTempPath(),
                    $"TicketPro_Emergency_{DateTime.Now:yyyy-MM-dd}.log");
                var sb = new StringBuilder();
                sb.AppendLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] EMERGENCY LOG (ruta principal fallo)");
                sb.AppendLine($"  Ruta fallida: {_logsPath}");
                sb.AppendLine($"  Error de escritura: {writeEx.Message}");
                sb.AppendLine($"  [{level}] {message}");
                if (originalEx != null)
                    sb.AppendLine($"  Original Exception: {originalEx.Message}");
                sb.AppendLine(new string('=', 80));
                File.AppendAllText(emergencyFile, sb.ToString(), Encoding.UTF8);
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine(
                    $"[CRITICAL] No se pudo escribir log en ninguna ubicacion: {message}");
            }
        }
        public string ObtenerRutaLogs() => _logsPath;
    }
}
