using ManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAllEmployees();

        List<Employee> GetEmployeesByPosition(string position);

        List<string> GetAllPositions();

        void AddEmployee(Employee employee);

        void DeleteEmployee(int id);

        List<SalaryReport> GetAverageSalaryByPosition();
    }
}
