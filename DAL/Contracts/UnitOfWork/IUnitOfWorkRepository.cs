using DAL.Contracts.Repositories;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts.UnitOfWork
{
    public interface IUnitOfWorkRepository
    {
        IGenericRepository<Ticket> TicketRepository { get; }

        //agregar mas repositorios q vaya creando

    }
}       