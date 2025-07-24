using System;

namespace ManagementSystem.Domain.Exceptions
{
    public class EmployeeNotFoundException : RepositoryException
    {
        public EmployeeNotFoundException(string message) : base(message) { }
        public EmployeeNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}