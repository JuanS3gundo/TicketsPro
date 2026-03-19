using System;
namespace Services.Services
{
    public interface ILoggerService
    {
        void LogError(string message, Exception exception = null);
        void LogWarning(string message);
        void LogCritical(string message, Exception exception = null);
    }
}
