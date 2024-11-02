using DAL.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts.UnitOfWork
{
    public interface IUnitOfWorkRepository
    {
        ITicketRepository TicketRepository { get; }

        //agregar mas repositorios q vaya creando

    }
}       