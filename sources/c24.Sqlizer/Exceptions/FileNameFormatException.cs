using System;
using System.Runtime.Serialization;

namespace c24.Sqlizer.Exceptions
{
    public class FileNameFormatException : Exception
    {
        public FileNameFormatException()
        {
        }

        public FileNameFormatException(string message) : base(message)
        {
        }

        public FileNameFormatException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FileNameFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}