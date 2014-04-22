using System;
using System.Runtime.Serialization;

namespace c24.Sqlizer.Exceptions
{
    public class WrongOrderException : Exception
    {
        public WrongOrderException()
        {
        }

        public WrongOrderException(string message) : base(message)
        {
        }

        public WrongOrderException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WrongOrderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}