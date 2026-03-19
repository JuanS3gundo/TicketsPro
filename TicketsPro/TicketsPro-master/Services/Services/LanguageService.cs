using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Services.Services
{
    public static class LanguageService
    {
        public static string T(string Key)
        {
            return LanguageManager.Translate(Key);
        }
    }
}
