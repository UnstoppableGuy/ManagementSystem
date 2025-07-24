using ManagementSystem.Domain.Interfaces;
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace ManagementSystem.Infrastructure.Database
{
    public class DatabaseConnection : IDatabaseConnection
    {
        private readonly string _connectionString;
        private readonly string _connectionStringName;

        public DatabaseConnection(string connectionStringName = "EmployeeDBConnection")
        {
            _connectionStringName = connectionStringName ?? throw new ArgumentNullException(nameof(connectionStringName));
            _connectionString = GetConnectionStringFromConfig();
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public bool TestConnection()
        {
            try
            {
                using var connection = CreateConnection();
                connection.Open();
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        private string GetConnectionStringFromConfig()
        {
            var connectionStringSettings = ConfigurationManager.ConnectionStrings[_connectionStringName];
            
            if (connectionStringSettings?.ConnectionString == null)
            {
                throw new InvalidOperationException(
                    $"Строка подключения '{_connectionStringName}' не найдена в файле конфигурации. " +
                    "Убедитесь, что строка подключения корректно настроена в App.config или Web.config.");
            }

            return connectionStringSettings.ConnectionString;
        }
    }
}