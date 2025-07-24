using ManagementSystem.Domain.Exceptions;
using ManagementSystem.Domain.Interfaces;
using ManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ManagementSystem.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ILogger _logger;
        private readonly IDatabaseConnection _databaseConnection;

        public EmployeeRepository(ILogger logger, IDatabaseConnection databaseConnection)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _databaseConnection = databaseConnection ?? throw new ArgumentNullException(nameof(databaseConnection));
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            var employees = new List<Employee>();

            try
            {
                using var connection = _databaseConnection.CreateConnection();
                using var command = new SqlCommand("GetEmployees", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                
                while (await reader.ReadAsync())
                {
                    employees.Add(MapEmployeeFromReader(reader));
                }

                _logger.Info($"Получено {employees.Count} сотрудников");
                return employees;
            }
            catch (SqlException ex)
            {
                _logger.Error("Ошибка при получении всех сотрудников", ex);
                throw new RepositoryException("Не удалось получить список сотрудников", ex);
            }
            catch (Exception ex)
            {
                _logger.Error("Неожиданная ошибка при получении всех сотрудников", ex);
                throw;
            }
        }

        public List<Employee> GetAllEmployees()
        {
            return GetAllEmployeesAsync().GetAwaiter().GetResult();
        }

        public async Task<List<Employee>> GetEmployeesByPositionAsync(string position)
        {
            if (string.IsNullOrWhiteSpace(position))
                throw new ArgumentException("Должность не может быть пустой", nameof(position));

            var employees = new List<Employee>();

            try
            {
                using var connection = _databaseConnection.CreateConnection();
                using var command = new SqlCommand("GetEmployeesByPosition", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                
                command.Parameters.Add(CreateParameter("@Position", SqlDbType.NVarChar, 100, position));

                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                
                while (await reader.ReadAsync())
                {
                    employees.Add(MapEmployeeFromReader(reader));
                }

                _logger.Info($"Получено {employees.Count} сотрудников для должности '{position}'");
                return employees;
            }
            catch (SqlException ex)
            {
                _logger.Error($"Ошибка при получении сотрудников по должности '{position}'", ex);
                throw new RepositoryException($"Не удалось получить сотрудников для должности '{position}'", ex);
            }
            catch (Exception ex)
            {
                _logger.Error($"Неожиданная ошибка при получении сотрудников по должности '{position}'", ex);
                throw;
            }
        }

        public List<Employee> GetEmployeesByPosition(string position)
        {
            return GetEmployeesByPositionAsync(position).GetAwaiter().GetResult();
        }

        public async Task<List<string>> GetAllPositionsAsync()
        {
            var positions = new List<string>();

            try
            {
                using var connection = _databaseConnection.CreateConnection();
                using var command = new SqlCommand("GetPositions", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                
                while (await reader.ReadAsync())
                {
                    positions.Add(reader.GetString(0));
                }

                _logger.Info($"Получено {positions.Count} должностей");
                return positions;
            }
            catch (SqlException ex)
            {
                _logger.Error("Ошибка при получении списка должностей", ex);
                throw new RepositoryException("Не удалось получить список должностей", ex);
            }
            catch (Exception ex)
            {
                _logger.Error("Неожиданная ошибка при получении списка должностей", ex);
                throw;
            }
        }

        public List<string> GetAllPositions()
        {
            return GetAllPositionsAsync().GetAwaiter().GetResult();
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            if (employee == null)
            {
                _logger.Error("AddEmployeeAsync вызван с employee == null");
                throw new ArgumentNullException(nameof(employee));
            }

            ValidateEmployee(employee);

            try
            {
                using var connection = _databaseConnection.CreateConnection();
                using var command = new SqlCommand("AddEmployee", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                AddEmployeeParameters(command, employee);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                _logger.Info($"Добавлен сотрудник: {employee.FirstName} {employee.LastName}");
            }
            catch (SqlException ex) when (ex.Number == SqlErrorCodes.UniqueConstraintViolation)
            {
                var message = "Сотрудник с такими данными уже существует ";
                _logger.Warning(message + ex.Message);
                throw new DuplicateEmployeeException(message, ex);
            }
            catch (SqlException ex) when (ex.Number == SqlErrorCodes.CheckConstraintViolation)
            {
                var message = "Данные сотрудника не соответствуют требованиям ";
                _logger.Warning(message + ex.Message);
                throw new ArgumentException(message, ex);
            }
            catch (SqlException ex)
            {
                _logger.Error("Ошибка при добавлении сотрудника", ex);
                throw new RepositoryException("Не удалось добавить сотрудника", ex);
            }
            catch (Exception ex)
            {
                _logger.Error("Неожиданная ошибка при добавлении сотрудника", ex);
                throw;
            }
        }
        public void AddEmployee(Employee employee)
        {
            AddEmployeeAsync(employee).GetAwaiter().GetResult();
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID сотрудника должен быть положительным числом", nameof(id));

            try
            {
                using var connection = _databaseConnection.CreateConnection();
                using var command = new SqlCommand("DeleteEmployee", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                
                command.Parameters.Add(CreateParameter("@Id", SqlDbType.Int, id));

                await connection.OpenAsync();
                var rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected == 0)
                {
                    var message = $"Сотрудник с ID {id} не найден";
                    _logger.Warning(message);
                    throw new EmployeeNotFoundException(message);
                }

                _logger.Info($"Удален сотрудник с ID: {id}");
            }
            catch (SqlException ex) when (!(ex is EmployeeNotFoundException))
            {
                _logger.Error($"Ошибка при удалении сотрудника с ID {id}", ex);
                throw new RepositoryException($"Не удалось удалить сотрудника с ID {id}", ex);
            }
            catch (Exception ex) when (!(ex is EmployeeNotFoundException))
            {
                _logger.Error($"Неожиданная ошибка при удалении сотрудника с ID {id}", ex);
                throw;
            }
        }

        public void DeleteEmployee(int id)
        {
            DeleteEmployeeAsync(id).GetAwaiter().GetResult();
        }

        public async Task<List<SalaryReport>> GetAverageSalaryByPositionAsync()
        {
            var reports = new List<SalaryReport>();

            try
            {
                using var connection = _databaseConnection.CreateConnection();
                using var command = new SqlCommand("GetAverageSalaryByPosition", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                
                while (await reader.ReadAsync())
                {
                    reports.Add(new SalaryReport
                    {
                        Position = reader.GetString(0),
                        AverageSalary = reader.GetDecimal(1)
                    });
                }

                _logger.Info($"Получено {reports.Count} записей отчета по зарплатам");
                return reports;
            }
            catch (SqlException ex)
            {
                _logger.Error("Ошибка при получении отчета по средним зарплатам", ex);
                throw new RepositoryException("Не удалось получить отчет по средним зарплатам", ex);
            }
            catch (Exception ex)
            {
                _logger.Error("Неожиданная ошибка при получении отчета по средним зарплатам", ex);
                throw;
            }
        }

        public List<SalaryReport> GetAverageSalaryByPosition()
        {
            return GetAverageSalaryByPositionAsync().GetAwaiter().GetResult();
        }

        private static Employee MapEmployeeFromReader(IDataReader reader)
        {
            return new Employee
            {
                Id = reader.GetInt32(0),
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2),
                Position = reader.GetString(3),
                BirthYear = reader.GetInt32(4),
                Salary = reader.GetDecimal(5)
            };
        }

        private static void ValidateEmployee(Employee employee)
        {
            if (string.IsNullOrWhiteSpace(employee.FirstName))
                throw new ArgumentException("Имя сотрудника не может быть пустым", nameof(employee.FirstName));

            if (string.IsNullOrWhiteSpace(employee.LastName))
                throw new ArgumentException("Фамилия сотрудника не может быть пустой", nameof(employee.LastName));

            if (string.IsNullOrWhiteSpace(employee.Position))
                throw new ArgumentException("Должность сотрудника не может быть пустой", nameof(employee.Position));

            if (employee.BirthYear < 1900 || employee.BirthYear > DateTime.Now.Year)
                throw new ArgumentException("Некорректный год рождения", nameof(employee.BirthYear));

            if (employee.Salary < 0)
                throw new ArgumentException("Зарплата не может быть отрицательной", nameof(employee.Salary));
        }

        private static void AddEmployeeParameters(SqlCommand command, Employee employee)
        {
            command.Parameters.Add(CreateParameter("@FirstName", SqlDbType.NVarChar, 50, employee.FirstName));
            command.Parameters.Add(CreateParameter("@LastName", SqlDbType.NVarChar, 50, employee.LastName));
            command.Parameters.Add(CreateParameter("@Position", SqlDbType.NVarChar, 100, employee.Position));
            command.Parameters.Add(CreateParameter("@BirthYear", SqlDbType.Int, employee.BirthYear));
            command.Parameters.Add(CreateParameter("@Salary", SqlDbType.Decimal, employee.Salary));
        }

        private static SqlParameter CreateParameter(string name, SqlDbType type, object value)
        {
            return new SqlParameter(name, type) { Value = value };
        }

        private static SqlParameter CreateParameter(string name, SqlDbType type, int size, object value)
        {
            return new SqlParameter(name, type, size) { Value = value };
        }
    }
}