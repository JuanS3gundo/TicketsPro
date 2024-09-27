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
        public static string PathLogError { get; set; } = ConfigurationManager.AppSettings["PathLogError"].ToString();
        public static string PathLogInfo { get; set; } = ConfigurationManager.AppSettings["PathLogInfo"].ToString();

        public static void WriteLog(Log log, Exception ex )
        {
            string message = FormatMessage(log);
            if (ex != null)
            {
                message += ex.StackTrace;
            }
            switch (log.Level)
            {
                case TraceLevel.Error:
                    message += ex.StackTrace;
                    WriteLogToFile(PathLogError, message);
                    break;
                case TraceLevel.Info:
                    message = FormatMessage(log);   
                    WriteLogToFile(PathLogInfo, message);   
                    break;
                default:
                    break;
            }
        }
        public static string FormatMessage(Log log)
        {
            return $"{log.date.ToString("dd/MM/yyyy HH:mm:ss")} - {log.Level}: {log.Message} - ";
        }
        public static void WriteLogToFile(string path, string message)
        {
            path = DateTime.Now.ToString("dd-MM-yyyy") + path;
            using (StreamWriter stream = new StreamWriter(path, true))
            {
                stream.WriteLine(message);
            }
        }
    }
}
