using BLL;
using BLL.Helpers;
using BLL.Implementations;
using BLL.Interfaces;
using DAL.Contracts;
using DAL.Contracts.Implementations.SqlServer.UnitOfWork;
using DAL.Contracts.Repositories;
using DAL.Implementations.SqlServer.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Implementations;
using Services.Services;
using System;
using System.Configuration;
using TicketPro.Inventario;
using TicketPro.Logins;
using TicketPro.Tecnico;
namespace TicketPro.Infraestructure
{
    public static class DependencyInjectionConfig
    {
        public static IServiceProvider Configure()
        {
            var services = new ServiceCollection();
            string connectionString = ConfigurationManager.ConnectionStrings["ProyectoTicketsProDB"].ConnectionString;
            services.AddScoped<IUnitOfWork>(provider => new UnitOfWorkSql(connectionString));
            services.AddScoped<ISolucionTicketRepository, SolucionTicketRepository>();
            services.AddScoped<ITicketBLL, TicketBLL>();
            services.AddScoped<IEquipoBLL, EquipoBLL>();
            services.AddScoped<ISolucionTicketBLL, SolucionTicketBLL>();
            services.AddScoped<IInventarioItemBLL, InventarioItemBLL>();
            services.AddSingleton<ILoggerService>(LoggerService.Instance);
            services.AddScoped<IExceptionService, ExceptionService>();
            services.AddScoped<AuditHelper>();
            services.AddScoped<LoginService>();
            services.AddScoped<Services.BLL.LoginBLL>();
            services.AddScoped<AnaliticasService>();
            services.AddScoped<IPrioridadBLL, PrioridadBLL>();
            services.AddTransient<BLL.Implementations.SLA.Strategies.Sla24x7Strategy>();
            services.AddTransient<BLL.Implementations.SLA.Strategies.SlaHorasLaboralesStrategy>();
            services.AddTransient<BLL.Interfaces.SLA.ISlaStrategyFactory, BLL.Implementations.SLA.SlaStrategyFactory>();
            services.AddScoped<ISLABLL, SLABLL>();
            services.AddScoped<IComentarioBLL, ComentarioBLL>();
            services.AddScoped<IAdjuntoBLL, AdjuntoBLL>();
            services.AddScoped<ICategoriaTicketBLL, CategoriaTicketBLL>();
            services.AddScoped<IEstadoTicketBLL, EstadoTicketBLL>();
            services.AddScoped<IUbicacionBLL, UbicacionBLL>();
            services.AddScoped<ITipoEquipoBLL, TipoEquipoBLL>();
            services.AddScoped<IUbicacionEquipoBLL, UbicacionEquipoBLL>();
            services.AddScoped<ICategoriaItemBLL, CategoriaItemBLL>();
            services.AddTransient<LogIn>();           
            services.AddTransient<Register>();        
            services.AddTransient<RecuperarPassword>(); 
            services.AddTransient<FormGestionAccesos>(); 
            services.AddTransient<FormGestionMaestros>(); 
            services.AddTransient<FormGestionSLA>();     
            services.AddTransient<VistaAnaliticas>();    
            services.AddTransient<VistaBitacora>();      
            services.AddTransient<FormGestionBackUp>();  
            services.AddTransient<FormGestionInventario>(); 
            services.AddTransient<FormNuevoEquipo>();        
            services.AddTransient<FormNuevoItem>();          
            return services.BuildServiceProvider();
        }
    }
}
