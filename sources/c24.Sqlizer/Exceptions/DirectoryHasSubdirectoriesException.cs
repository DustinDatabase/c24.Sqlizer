using System;
using System.Runtime.Serialization;

namespace c24.Sqlizer.Exceptions
{
    public class DirectoryHasSubdirectoriesException : Exception
    {
        public DirectoryHasSubdirectoriesException()
        {
        }

        public DirectoryHasSubdirectoriesException(string message) : base(message)
        {
        }

        public DirectoryHasSubdirectoriesException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DirectoryHasSubdirectoriesException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}