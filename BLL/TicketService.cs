using DAL.Contracts;
using DAL.Contracts.Implementations.SqlServer.UnitOfWork;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration; 

namespace BLL
{
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;

        // Singleton instance con configuración de connectionString
        private static readonly Lazy<TicketService> _instance = new Lazy<TicketService>(() =>
        {
            // Obtener el connectionString desde el archivo de configuración
            string connectionString = ConfigurationManager.ConnectionStrings["ProyectoTicketsProDB"].ConnectionString;
            return new TicketService(new UnitOfWorkSql(connectionString)); // Pasar el connectionString al constructor
        });

        // Propiedad para acceder a la instancia Singleton de TicketService
        public static TicketService Instance => _instance.Value;

        // Constructor privado para evitar instanciación externa
        private TicketService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        void ITicketService.ActualizarTicket(Ticket ticket)
        {
            throw new NotImplementedException();
        }

        void ITicketService.AsignarTecnico(Guid ticketId, int tecnicoId)
        {
            throw new NotImplementedException();
        }

        void ITicketService.CambiarEstado(Guid ticketId, Enums.Estado nuevoEstado)
        {
            throw new NotImplementedException();
        }

        void ITicketService.CrearTicket(Ticket ticket)
        {
            if (string.IsNullOrWhiteSpace(ticket.Titulo))
                throw new ArgumentException("El título del ticket es obligatorio.");

            using (var uow = _unitOfWork.Create())
            {
                uow.Repositories.TicketRepository.Add(ticket);
                uow.SaveChanges();
            }
        }

        Ticket ITicketService.ObtenerTicketPorId(Guid id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Ticket> ITicketService.ObtenerTicketsPorEstado(Enums.Estado estado)
        {
            throw new NotImplementedException();
        }

    }
}
