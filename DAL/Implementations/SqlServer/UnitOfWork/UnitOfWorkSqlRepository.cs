using DAL.Contracts;
using DAL.Contracts.Implementations.SqlServer.Repositories;
using DAL.Contracts.UnitOfWork;
using Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations.SqlServer.UnitOfWork
{
    public class UnitOfWorkSqlRepository : IUnitOfWorkRepository
    {
        public IGenericRepository<Ticket> TicketRepository { get; private set; }



        public UnitOfWorkSqlRepository(SqlConnection connection, SqlTransaction transaction)
        {
            TicketRepository = new TicketRepository(connection, transaction);
        }
    }
}
