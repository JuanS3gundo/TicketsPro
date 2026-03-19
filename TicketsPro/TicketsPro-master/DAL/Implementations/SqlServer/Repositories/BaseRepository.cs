using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace DAL.Implementations.SqlServer.Repositories
{
    public abstract class BaseRepository<T>
    {
        protected readonly SqlConnection _connection;
        protected readonly SqlTransaction _transaction;
        protected BaseRepository(SqlConnection connection, SqlTransaction transaction)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _transaction = transaction;
        }
        protected SqlCommand CreateCommand(string sql)
        {
            var cmd = _connection.CreateCommand();
            cmd.Transaction = _transaction;
            cmd.CommandText = sql;
            return cmd;
        }
        protected void AddParam(SqlCommand cmd, string name, object value)
        {
            cmd.Parameters.AddWithValue(name, value ?? DBNull.Value);
        }
        protected T QuerySingle(string sql, Action<SqlCommand> addParams, Func<SqlDataReader, T> map)
        {
            using (var cmd = CreateCommand(sql))
            {
                addParams?.Invoke(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    return reader.Read() ? map(reader) : default;
                }
            }
        }
        protected List<T> QueryList(string sql, Action<SqlCommand> addParams, Func<SqlDataReader, T> map)
        {
            var list = new List<T>();
            using (var cmd = CreateCommand(sql))
            {
                addParams?.Invoke(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        list.Add(map(reader));
                }
            }
            return list;
        }
        protected List<T> QueryList(string sql, Func<SqlDataReader, T> map)
        {
            return QueryList(sql, null, map);
        }
        protected int ExecuteNonQuery(string sql, Action<SqlCommand> addParams)
        {
            using (var cmd = CreateCommand(sql))
            {
                addParams?.Invoke(cmd);
                return cmd.ExecuteNonQuery();
            }
        }
        protected object ExecuteScalar(string sql, Action<SqlCommand> addParams)
        {
            using (var cmd = CreateCommand(sql))
            {
                addParams?.Invoke(cmd);
                return cmd.ExecuteScalar();
            }
        }
        
        protected static Guid? ReadNullableGuid(SqlDataReader r, string col)
        {
            int ord = r.GetOrdinal(col);
            return r.IsDBNull(ord) ? (Guid?)null : r.GetGuid(ord);
        }
        protected static int? ReadNullableInt(SqlDataReader r, string col)
        {
            int ord = r.GetOrdinal(col);
            return r.IsDBNull(ord) ? (int?)null : r.GetInt32(ord);
        }
        protected static DateTime? ReadNullableDateTime(SqlDataReader r, string col)
        {
            int ord = r.GetOrdinal(col);
            return r.IsDBNull(ord) ? (DateTime?)null : r.GetDateTime(ord);
        }
        protected static string ReadString(SqlDataReader r, string col, string defaultValue = "")
        {
            int ord = r.GetOrdinal(col);
            return r.IsDBNull(ord) ? defaultValue : r.GetString(ord);
        }
        protected static decimal ReadDecimal(SqlDataReader r, string col, decimal defaultValue = 0)
        {
            int ord = r.GetOrdinal(col);
            return r.IsDBNull(ord) ? defaultValue : r.GetDecimal(ord);
        }
    }
}
