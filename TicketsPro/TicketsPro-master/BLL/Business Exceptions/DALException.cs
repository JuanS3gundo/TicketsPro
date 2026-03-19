using System;
using System.Data.SqlClient;
namespace BLL.Exceptions
{

    public class DALException : Exception
    {
        public int? SqlErrorNumber { get; }
        public byte? SqlErrorClass { get; }
        public byte? SqlErrorState { get; }
        public string SqlServer { get; }
        public DALException(string message, Exception innerException = null)
            : base(message, innerException)
        {
            if (innerException is SqlException sqlEx)
            {
                SqlErrorNumber = sqlEx.Number;
                SqlErrorClass = sqlEx.Class;
                SqlErrorState = sqlEx.State;
                SqlServer = sqlEx.Server ?? "Unknown";
            }
        }
    }
}
