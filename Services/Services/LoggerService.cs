using Services.Dao;
using Services.DomainModel;
using Services.Implementations;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LoggerService : ILoggerService
    {
        public void WriteLog(Log log, Exception ex = null)
        {
            BLL.LoggerBLL.WriteLog(log, ex);
        }
    }
}
