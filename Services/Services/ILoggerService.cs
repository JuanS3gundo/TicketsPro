using Services.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public interface ILoggerService 
    {
        void WriteLog(Log log, Exception ex = null);

    }
}
