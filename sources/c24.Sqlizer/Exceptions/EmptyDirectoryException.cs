using System;
using System.Runtime.Serialization;

namespace c24.Sqlizer.Exceptions
{
    public class EmptyDirectoryException : Exception
    {
        public EmptyDirectoryException()
        {
        }

        public EmptyDirectoryException(string message) : base(message)
        {
        }

        public EmptyDirectoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EmptyDirectoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}