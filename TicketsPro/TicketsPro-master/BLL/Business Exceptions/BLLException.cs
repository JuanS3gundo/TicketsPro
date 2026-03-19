using System;
namespace BLL.Exceptions
{

    public class BLLException : Exception
    {
        public BLLException(string message)
            : base(message)
        {
        }
        public BLLException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
