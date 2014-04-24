using System;
using System.Runtime.Serialization;

namespace c24.Sqlizer.Exceptions
{
    public class SqlCmdNotFoundException : Exception
    {
        public SqlCmdNotFoundException()
        {
        }

        public SqlCmdNotFoundException(string message) : base(message)
        {
        }

        public SqlCmdNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SqlCmdNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}