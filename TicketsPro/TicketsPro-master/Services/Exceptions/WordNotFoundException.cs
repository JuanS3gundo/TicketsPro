using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Services.Facade.ExceptionsManager
{
    public class WordNotFoundException : Exception
    {
        public WordNotFoundException(string Key)
            : base($"la palabra '{Key}' no fue encontrada en el repositorio.") { }
    }
}
