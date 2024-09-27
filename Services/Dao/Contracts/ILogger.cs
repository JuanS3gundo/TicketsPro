using Services.DomainModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dao.Contracts
{
    internal interface ILogger
    {
       void WriteLog(Log log, Exception ex = null);
    }
}
