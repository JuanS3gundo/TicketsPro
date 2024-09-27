using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DomainModel
{
    public class Log
    {
        public Log(DateTime date, string message, TraceLevel level)
        {
            this.date = date;
            Message = message;
            Level = level;
        }

        public DateTime date { get; set; }  
        public string Message { get; set; } 
        public TraceLevel Level { get; set; }   

    }
}
