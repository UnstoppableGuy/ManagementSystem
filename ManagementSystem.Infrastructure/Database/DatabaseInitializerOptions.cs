namespace ManagementSystem.Infrastructure.Database
{
    public class DatabaseInitializerOptions
    {
        public string DatabaseName { get; set; } = "EmployeeDB";
        public string CreateScriptResourceName { get; set; } = "ManagementSystem.Infrastructure.Scripts.CreateDatabase.sql";
        public int InitialSize { get; set; } = 10;
        public int MaxSize { get; set; } = 100;
        public int FileGrowth { get; set; } = 5;
        public int LogInitialSize { get; set; } = 1;
        public int LogMaxSize { get; set; } = 10;
        public int LogFileGrowth { get; set; } = 1;
    }
}