using DAL.Contracts.Repositories;
using DAL.Contracts.UnitOfWork;
using DAL.Implementations.SqlServer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL.Contracts.Implementations.SqlServer.UnitOfWork
{
    public class UnitOfWorkSql : IUnitOfWork
    {
            private readonly string _connectionString;
            public UnitOfWorkSql(string connectionString)
            {
                _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            }
            public IUnitOfWorkAdapter Create()
            {
                return new UnitOfWorkSqlAdapter(_connectionString);
            }
        }
    }
