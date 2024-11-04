using BLL;
using Controller;
using DAL.Contracts;
using DAL.Contracts.Implementations.SqlServer.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketPro.Infraestructure
{
    public static class DependencyInjectionConfig
    {
        public static IServiceProvider Configure()
        {

            //aca va la configuracion de la inyeccion de dependencias
            var services = new ServiceCollection();
            string connectionString = ConfigurationManager.ConnectionStrings["ProyectoTicketsProDB"].ConnectionString;

            // Registrar dependencias en el contenedor de DI
            services.AddSingleton<ILoggerService, LoggerService>(); // Usando LoggerService como un servicio estático
            services.AddScoped<IUnitOfWork>(provider =>
                new UnitOfWorkSql(ConfigurationManager.ConnectionStrings["ProyectoTicketsProDB"].ConnectionString)); services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<TicketController>();
            services.AddScoped<CrearTicket>();

            services.AddTransient<Form1>();

            return services.BuildServiceProvider();
        }
    }
}
