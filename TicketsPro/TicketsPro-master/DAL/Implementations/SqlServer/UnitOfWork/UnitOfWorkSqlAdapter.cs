using DAL.Contracts.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace DAL.Implementations.SqlServer.UnitOfWork
{
    public class UnitOfWorkSqlAdapter : IUnitOfWorkAdapter
    {
        private readonly SqlConnection _connection;
        private SqlTransaction _transaction;
        public IUnitOfWorkRepository Repositories { get; private set; }
        public UnitOfWorkSqlAdapter(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
            Repositories = new UnitOfWorkSqlRepository(_connection, _transaction);
        }
        public void SaveChanges()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    _transaction = _connection.BeginTransaction();
                    Repositories = new UnitOfWorkSqlRepository(_connection, _transaction);
                }
            }
        }
        public void ExecuteCommand(string sql, Dictionary<string, object> parameters)
        {
            using (var cmd = _connection.CreateCommand())
            {
                cmd.Transaction = _transaction;
                cmd.CommandText = sql;
                if (parameters != null)
                {
                    foreach (var p in parameters)
                    {
                        cmd.Parameters.AddWithValue("@" + p.Key, p.Value ?? DBNull.Value);
                    }
                }
                cmd.ExecuteNonQuery();
            }
        }
        public void Dispose()
        {
            try
            {
                _transaction?.Dispose();
            }
            finally
            {
                _connection?.Close();
                _connection?.Dispose();
                Repositories = null;
            }
        }
    }
}
