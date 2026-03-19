using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace DAL.Helpers
{
    public static class SqlHelper
    {
        public readonly static string conString;
        static SqlHelper()
        {
            conString = ConfigurationManager.ConnectionStrings["ProyectoTicketsProDB"].ConnectionString;
        }
        public static int ExecuteNonQuery(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            CheckNullables(parameters);
            using (SqlConnection conn = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand(commandText, conn))
            {
                cmd.CommandType = commandType;
                cmd.Parameters.AddRange(parameters);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }
        public static int ExecuteNonQuery(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] parameters)
        {
            CheckNullables(parameters);
            using (SqlCommand cmd = new SqlCommand(commandText, transaction.Connection, transaction))
            {
                cmd.CommandType = commandType;
                cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteNonQuery();
            }
        }
        public static object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] parameters)
        {
            CheckNullables(parameters);
            using (SqlCommand cmd = new SqlCommand(commandText, transaction.Connection, transaction))
            {
                cmd.CommandType = commandType;
                cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteScalar();
            }
        }
        public static SqlDataReader ExecuteReader(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] parameters)
        {
            CheckNullables(parameters);
            SqlCommand cmd = new SqlCommand(commandText, transaction.Connection, transaction);
            cmd.CommandType = commandType;
            cmd.Parameters.AddRange(parameters);
            return cmd.ExecuteReader();
        }
        private static void CheckNullables(SqlParameter[] parameters)
        {
            foreach (var p in parameters)
            {
                if (p.SqlValue == null)
                    p.SqlValue = DBNull.Value;
            }
        }
    }
}
