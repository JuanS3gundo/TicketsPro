using BLL;
using Entity;
using Services;
using Services.DomainModel;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class TicketController
    {
        private readonly ITicketService _ticketService;
        private readonly ILoggerService _loggerService;

        public TicketController(ITicketService ticketService, ILoggerService loggerService)
        {
            _ticketService = ticketService;
            _loggerService = loggerService; // Inyección de LoggerService

        }
        public List<Ticket> ObtenerTodosLosTickets()
        {
            return _ticketService.ObtenerTodosLosTickets();
        }
        public void CrearTicket(Ticket ticket)
        {
            try
            {
                _ticketService.CrearTicket(ticket);
                Log log = new Log(DateTime.Now, "Ticket creado exitosamente", TraceLevel.Info);
                _loggerService.WriteLog(log);    

            }
            catch(Exception ex)
            {
                Log log = new Log(DateTime.Now, $"Error al crear el ticket: {ex.Message}", TraceLevel.Error);
                _loggerService.WriteLog(log);
            }
        }
        public List<Ticket> ObtenerTicketsPorEstado(Enums.Estado estado)
        {
            try
            {
                var tickets = _ticketService.ObtenerTicketsPorEstado(estado);   
                Log log = new Log(DateTime.Now, "Tickets obtenidos correctamente", TraceLevel.Info);
                _loggerService.WriteLog(log);

                return tickets.ToList();    
            }
            catch (Exception ex)
            {
                Log log = new Log(DateTime.Now, $"Error al obtener los tickets por estado: {ex.Message}", TraceLevel.Error);
                _loggerService.WriteLog(log);
                return null;
            }
        }   
    }
}

