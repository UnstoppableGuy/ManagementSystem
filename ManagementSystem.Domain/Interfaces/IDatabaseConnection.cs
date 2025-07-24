using System.Data.SqlClient;

namespace ManagementSystem.Domain.Interfaces
{
    public interface IDatabaseConnection
    {
        string GetConnectionString();
        SqlConnection CreateConnection();
        bool TestConnection();
    }
}