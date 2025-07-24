using System;

namespace ManagementSystem.Domain.Exceptions
{
    public class DuplicateEmployeeException : RepositoryException
    {
        public DuplicateEmployeeException(string message) : base(message) { }
        public DuplicateEmployeeException(string message, Exception innerException) : base(message, innerException) { }
    }
}