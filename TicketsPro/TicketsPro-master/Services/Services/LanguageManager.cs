using Services.Implementations;
using System.Globalization;
using System.Threading;
namespace Services.Services
{
    public static class LanguageManager
    {
        public static string CurrentLanguage =>
            Thread.CurrentThread.CurrentUICulture.Name;
        public static string LastLanguage { get; private set; } = "es-ES";
        public static void SetLanguage(string lang)
        {
            LastLanguage = lang;
            Thread.CurrentThread.CurrentUICulture =
                new System.Globalization.CultureInfo(lang);
        }
        public static string Translate(string key)
        {
            return LanguageRepository.Translate(key);
        }
    }
}
