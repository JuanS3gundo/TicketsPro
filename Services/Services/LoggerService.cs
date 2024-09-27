using Services.Dao;
using Services.DomainModel;
using Services.Implementations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class LoggerService
    {
        public static void WriteLog(Log log, Exception ex = null)
        {
            BLL.LoggerBLL.WriteLog(log, ex);    
        }
    }
}
