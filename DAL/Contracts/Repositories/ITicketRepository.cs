using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entity.Enums;

namespace DAL.Contracts.Repositories
{
    public interface ITicketRepository : IGenericRepository<Ticket> 
    {
        IEnumerable<Ticket> GetTicketsByEstado(Estado estado);
        IEnumerable<Ticket> GetTicketsByTecnico(int tecnicoId);
        IEnumerable<Ticket> GetTicketsByCategoria(Categoria categoria);
        IEnumerable<Ticket> GetTicketsByFecha(DateTime fechaApertura);
    }
}
