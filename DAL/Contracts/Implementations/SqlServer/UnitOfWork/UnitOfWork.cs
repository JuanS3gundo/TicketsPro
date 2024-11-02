using DAL.Contracts.Implementations.SqlServer.Repositories;
using DAL.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts.Implementations.SqlServer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;

        // Repositorios específicos inicializados bajo demanda (lazy loading)
        private TicketRepository _ticketRepository;


        public UnitOfWork(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public ITicketRepository TicketRepository => _ticketRepository ??= new TicketRepository(_connection, _transaction);


        public int SaveChanges()
        {
            try
            {
                _transaction.Commit();
                return 1; // Indica que los cambios fueron aplicados
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Close();
            _connection?.Dispose();
        }
    }
}

