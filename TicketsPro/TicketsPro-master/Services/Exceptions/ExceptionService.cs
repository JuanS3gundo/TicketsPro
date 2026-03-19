using System;
using System.Data.SqlClient;
using Services.Implementations;
using Services.DomainModel;
namespace Services.Services
{
    using Services;

    public sealed class ExceptionService : IExceptionService
    {
        private readonly ILoggerService _logger;
        public ExceptionService(ILoggerService logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        private static readonly ExceptionService _instance = new ExceptionService(Implementations.LoggerService.Instance);
        public static ExceptionService Current => _instance;
        public void Handle(Exception ex, ExceptionContext context)
        {
            try
            {
                if (ex == null)
                    return;
                if (context == null)
                    context = new ExceptionContext();
                if (context.Timestamp == default(DateTime))
                    context.Timestamp = DateTime.UtcNow;
                if (DebeAutoclasificar(context))
                {
                    context.CategoryType = DeterminarCategoria(ex);
                    context.SeverityLevel = DeterminarSeveridad(context.CategoryType);
                }
                
                if (context.CategoryType == ExceptionContext.Category.Business)
                {
                    
                    
                    return;
                }
                if (context.CategoryType == ExceptionContext.Category.Technical)
                {
                    var logLevel = context.SeverityLevel == ExceptionContext.Severity.Critical
                        ? LogLevel.Critical
                        : LogLevel.Error;
                    string mensaje = $"[ERROR TÉCNICO] {context.OperationName}\n" +
                                    $"Tipo: {ex.GetType().Name}\n" +
                                    $"Mensaje: {SanitizarMensaje(ex.Message)}\n" +
                                    $"StackTrace:\n{ex.StackTrace}";
                    if (ex.InnerException != null)
                    {
                        mensaje += $"\nInnerException: {ex.InnerException.GetType().Name}\n" +
                                  $"Mensaje Inner: {SanitizarMensaje(ex.InnerException.Message)}";
                    }
                    LogWithLevel(logLevel, mensaje, ex);
                    return;
                }
                
                string mensajeUnexpected = $"[BUG NO PREVISTO] {context.OperationName}\n" +
                                          $"Tipo: {ex.GetType().FullName}\n" +
                                          $"Mensaje: {SanitizarMensaje(ex.Message)}\n" +
                                          $"StackTrace:\n{ex.StackTrace}";
                if (ex.InnerException != null)
                {
                    mensajeUnexpected += $"\nInnerException: {ex.InnerException.GetType().FullName}\n" +
                                        $"Mensaje Inner: {SanitizarMensaje(ex.InnerException.Message)}";
                }
                _logger.LogCritical(mensajeUnexpected, ex);
            }
            catch
            {
                
            }
        }
        private enum LogLevel
        {
            Critical,
            Error
        }
        private void LogWithLevel(LogLevel level, string mensaje, Exception ex)
        {
            if (level == LogLevel.Critical)
                _logger.LogCritical(mensaje, ex);
            else
                _logger.LogError(mensaje, ex);
        }
        private readonly System.Collections.Generic.HashSet<string> _camposSensibles = new System.Collections.Generic.HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "password", "contraseña", "passwd", "pwd",
            "token", "apikey", "api_key", "secret", "authorization",
            "creditcard", "cvv", "ssn", "dni"
        };
        private string SanitizarMensaje(string mensaje)
        {
            if (string.IsNullOrEmpty(mensaje))
                return mensaje;
            var regex = new System.Text.RegularExpressions.Regex(
                @"(\w+)\s*[=:]\s*([^\s,;]+)",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            return regex.Replace(mensaje, match =>
            {
                string paramName = match.Groups[1].Value;
                return _camposSensibles.Contains(paramName)
                    ? $"{paramName}=***REDACTED***"
                    : match.Value;
            });
        }
        private bool DebeAutoclasificar(ExceptionContext context)
        {
            return context.CategoryType == default(ExceptionContext.Category)
                && context.SeverityLevel == default(ExceptionContext.Severity);
        }
        private ExceptionContext.Category DeterminarCategoria(Exception ex)
        {
            if (ex is SqlException || ex is TimeoutException)
                return ExceptionContext.Category.Technical;
            var typeName = ex.GetType().FullName ?? ex.GetType().Name;
            if (typeName == "BLL.Exceptions.DALException")
                return ExceptionContext.Category.Technical;
            if (typeName == "BLL.Exceptions.BLLException" || typeName.StartsWith("BLL.Exceptions."))
                return ExceptionContext.Category.Business;
            if (ex is ArgumentException)
                return ExceptionContext.Category.Business;
            return ExceptionContext.Category.Unexpected;
        }
        private ExceptionContext.Severity DeterminarSeveridad(ExceptionContext.Category category)
        {
            switch (category)
            {
                case ExceptionContext.Category.Business:
                    return ExceptionContext.Severity.Info;
                case ExceptionContext.Category.Technical:
                    return ExceptionContext.Severity.Critical;
                default:
                    return ExceptionContext.Severity.Critical;
            }
        }
    }
}
