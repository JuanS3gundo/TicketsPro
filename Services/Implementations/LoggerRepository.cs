using Services.Dao.Contracts;
using Services.DomainModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    internal static class LoggerRepository
    {
        public static string FolderLogs { get; set; } = ConfigurationManager.AppSettings["FolderLogs"];
        public static string PathLogError { get; set; } = System.IO.Path.Combine(FolderLogs, ConfigurationManager.AppSettings["PathLogError"]);
        public static string PathLogInfo { get; set; } = System.IO.Path.Combine(FolderLogs, ConfigurationManager.AppSettings["PathLogInfo"]);

        public static void WriteLog(Log log, Exception ex = null)
        {
            string message = FormatMessage(log);

            if (ex != null)
            {
                message += Environment.NewLine + ex.StackTrace;
            }

            switch (log.Level)
            {
                case TraceLevel.Error:
                    if (!string.IsNullOrEmpty(PathLogError))
                    {
                        EnsureLogDirectoryExists();
                        WriteLogToFile(PathLogError, message);
                    }
                    break;

                case TraceLevel.Info:
                    if (!string.IsNullOrEmpty(PathLogInfo))
                    {
                        EnsureLogDirectoryExists();
                        WriteLogToFile(PathLogInfo, message);
                    }
                    break;
            }
        }
        private static void EnsureLogDirectoryExists()
        {
            // Crea el directorio de logs si no existe
            if (!System.IO.Directory.Exists(FolderLogs))
            {
                System.IO.Directory.CreateDirectory(FolderLogs);
            }
        }

        public static string FormatMessage(Log log)
        {
            return $"{log.date.ToString("dd/MM/yyyy HH:mm:ss")} - {log.Level}: {log.Message} - ";
        }
        private static void WriteLogToFile(string path, string message)
        {
            if (!System.IO.File.Exists(path))
            {
                System.IO.File.Create(path).Dispose();
            }

            System.IO.File.AppendAllText(path, message + Environment.NewLine);
        }
    }
}
