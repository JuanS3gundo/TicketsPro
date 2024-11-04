using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicketPro.Infraestructure;

namespace TicketPro
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Configura el contenedor de inyección de dependencias
            var serviceProvider = DependencyInjectionConfig.Configure();
            var crearTicketForm = serviceProvider.GetRequiredService<CrearTicket>();


            Application.Run(crearTicketForm);
        }
    }
}
