using Services.DomainModel;
using Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.BLL
{
    internal static class LoggerBLL
    {
        //aca pongo las politicas
        public static void WriteLog(Log log, Exception ex = null)
        {

            LoggerRepository.WriteLog(log, ex); 
            try
            {
                //ILogger logger = new Logger();
                //logger.WriteLog(log, ex);
            }
            catch (Exception)
            {
                //throw;
            }
        }   
    }
}
    