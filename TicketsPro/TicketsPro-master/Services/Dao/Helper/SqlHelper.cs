using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace Services.Dao.Helper
{
    public static class SqlHelper
    {
        private const string DefaultConnName = "ServicesPP";
        private static readonly string _defaultCs;
        static SqlHelper()
        {
            var cs = ConfigurationManager.ConnectionStrings[DefaultConnName];
            if (cs == null || string.IsNullOrWhiteSpace(cs.ConnectionString))
                throw new InvalidOperationException($"No se encontro la connectionString '{DefaultConnName}' en App.config.");
            _defaultCs = cs.ConnectionString;
        }
        private static string GetCs(string connName)
        {
            if (string.IsNullOrWhiteSpace(connName)) return _defaultCs;
            var cs = ConfigurationManager.ConnectionStrings[connName];
            if (cs == null || string.IsNullOrWhiteSpace(cs.ConnectionString))
                throw new InvalidOperationException($"No se encontro la connectionString '{connName}' en App.config.");
            return cs.ConnectionString;
        }
        private static void CheckNullables(SqlParameter[] parameters)
        {
            if (parameters == null) return;
            foreach (var p in parameters)
            {
                if (p == null) continue;
                if (p.SqlValue == null) p.SqlValue = DBNull.Value;
            }
        }
        public static int ExecuteNonQuery(string commandText, CommandType commandType, params SqlParameter[] parameters)
            => ExecuteNonQuery(null, commandText, commandType, parameters);
        public static object ExecuteScalar(string commandText, CommandType commandType, params SqlParameter[] parameters)
            => ExecuteScalar(null, commandText, commandType, parameters);
        public static SqlDataReader ExecuteReader(string commandText, CommandType commandType, params SqlParameter[] parameters)
            => ExecuteReader(null, commandText, commandType, parameters);
        public static int ExecuteNonQuery(string connName, string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            CheckNullables(parameters);
            using (var conn = new SqlConnection(GetCs(connName)))
            using (var cmd = new SqlCommand(commandText, conn) { CommandType = commandType })
            {
                if (parameters != null && parameters.Length > 0) cmd.Parameters.AddRange(parameters);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }
        public static object ExecuteScalar(string connName, string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            CheckNullables(parameters);
            using (var conn = new SqlConnection(GetCs(connName)))
            using (var cmd = new SqlCommand(commandText, conn) { CommandType = commandType })
            {
                if (parameters != null && parameters.Length > 0) cmd.Parameters.AddRange(parameters);
                conn.Open();
                return cmd.ExecuteScalar();
            }
        }
        public static SqlDataReader ExecuteReader(string connName, string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            CheckNullables(parameters);
            var conn = new SqlConnection(GetCs(connName));
            var cmd = new SqlCommand(commandText, conn) { CommandType = commandType };
            if (parameters != null && parameters.Length > 0) cmd.Parameters.AddRange(parameters);
            conn.Open();
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
    }
}
