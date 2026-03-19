using DAL.Contracts.Implementations.SqlServer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL.Contracts.UnitOfWork
{
    public static class UnitOfWorkFactory
    {
        private static readonly string _connectionString =
            ConfigurationManager.ConnectionStrings["ProyectoTicketsProDB"].ConnectionString;
        public static IUnitOfWork Current
        {
            get
            {
                return new UnitOfWorkSql(_connectionString);
            }
        }
    }
}
