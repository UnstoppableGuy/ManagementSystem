using System;

namespace ManagementSystem.Domain.Exceptions
{
    public class DatabaseInitializationException : Exception
    {
        public DatabaseInitializationException(string message) : base(message) { }
        public DatabaseInitializationException(string message, Exception innerException) : base(message, innerException) { }
    }
}