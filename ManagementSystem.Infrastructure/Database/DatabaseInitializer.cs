using ManagementSystem.Domain.Exceptions;
using ManagementSystem.Domain.Interfaces;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace ManagementSystem.Infrastructure.Database
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly ILogger _logger;
        private readonly IDatabaseConnection _databaseConnection;
        private readonly DatabaseInitializerOptions _options;

        public DatabaseInitializer(
            ILogger logger,
            IDatabaseConnection databaseConnection,
            DatabaseInitializerOptions options = null)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _databaseConnection = databaseConnection ?? throw new ArgumentNullException(nameof(databaseConnection));
            _options = options ?? new DatabaseInitializerOptions();
        }

        public void Initialize()
        {
            InitializeAsync().GetAwaiter().GetResult();
        }

        public async Task InitializeAsync()
        {
            _logger.Info("Начало инициализации базы данных...");

            try
            {
                await CreateDatabaseIfNotExistsAsync();
                await CreateTablesAndProceduresAsync();
                _logger.Info("База данных инициализирована успешно.");
            }
            catch (SqlException ex) when (IsDatabaseAttachError(ex))
            {
                var message = "Не удалось прикрепить базу данных. " +
                             "Попробуйте удалить базу 'EmployeeDB' из LocalDB через sqlcmd или SSMS";
                _logger.Error(message, ex);
                throw new DatabaseInitializationException(message, ex);
            }
            catch (Exception ex)
            {
                _logger.Error("Неизвестная ошибка при инициализации БД", ex);
                throw;
            }
        }

        private async Task CreateDatabaseIfNotExistsAsync()
        {
            var masterConnectionString = GetMasterConnectionString();

            using var connection = new SqlConnection(masterConnectionString);
            await connection.OpenAsync();

            var dbExists = await CheckDatabaseExistsAsync(connection);

            if (!dbExists)
            {
                _logger.Info($"База данных {_options.DatabaseName} не существует. Создаю новую...");
                await CreateDatabaseAsync(connection);
                _logger.Info($"База данных {_options.DatabaseName} создана успешно");
            }
            else
            {
                _logger.Info($"База данных {_options.DatabaseName} уже существует.");
            }
        }

        private async Task<bool> CheckDatabaseExistsAsync(SqlConnection connection)
        {
            const string checkQuery = "SELECT COUNT(*) FROM sys.databases WHERE name = @DatabaseName";

            using var command = new SqlCommand(checkQuery, connection);
            command.Parameters.AddWithValue("@DatabaseName", _options.DatabaseName);

            var count = (int)await command.ExecuteScalarAsync();
            return count > 0;
        }

        private async Task CreateDatabaseAsync(SqlConnection connection)
        {
            var dataDirectory = GetDataDirectory();
            var mdfPath = Path.Combine(dataDirectory, $"{_options.DatabaseName}.mdf");
            var ldfPath = Path.Combine(dataDirectory, $"{_options.DatabaseName}_Log.ldf");

            Directory.CreateDirectory(dataDirectory);

            var createDbScript = BuildCreateDatabaseScript(mdfPath, ldfPath);

            using var command = new SqlCommand(createDbScript, connection);
            await command.ExecuteNonQueryAsync();
        }

        private string BuildCreateDatabaseScript(string mdfPath, string ldfPath)
        {
            return $@"
                CREATE DATABASE [{_options.DatabaseName}]
                ON (NAME = '{_options.DatabaseName}', 
                    FILENAME = '{mdfPath}', 
                    SIZE = {_options.InitialSize}MB, 
                    MAXSIZE = {_options.MaxSize}MB, 
                    FILEGROWTH = {_options.FileGrowth}MB)
                LOG ON (NAME = '{_options.DatabaseName}_Log', 
                        FILENAME = '{ldfPath}', 
                        SIZE = {_options.LogInitialSize}MB, 
                        MAXSIZE = {_options.LogMaxSize}MB, 
                        FILEGROWTH = {_options.LogFileGrowth}MB)";
        }

        private async Task CreateTablesAndProceduresAsync()
        {
            using var connection = _databaseConnection.CreateConnection();
            await connection.OpenAsync();

            var tablesExist = await CheckTablesExistAsync(connection);
            if (tablesExist)
            {
                _logger.Info("Таблицы уже существуют. Создание таблиц пропущено.");
                return;
            }

            _logger.Info("Создаю таблицы и процедуры...");

            var script = await LoadScriptFromResourceAsync();
            await ExecuteScriptBatchesAsync(script, connection);

            _logger.Info("Таблицы и процедуры созданы успешно.");
        }

        private async Task<bool> CheckTablesExistAsync(SqlConnection connection)
        {
            const string checkQuery = @"
                SELECT COUNT(*) 
                FROM INFORMATION_SCHEMA.TABLES 
                WHERE TABLE_NAME = 'Employees'";

            using var command = new SqlCommand(checkQuery, connection);
            var result = await command.ExecuteScalarAsync();
            return result != null && (int)result == 1;
        }

        private async Task ExecuteScriptBatchesAsync(string script, SqlConnection connection)
        {
            var batches = script.Split(
                new[] { "\nGO", "\rGO", "\r\nGO" },
                StringSplitOptions.RemoveEmptyEntries);

            foreach (var batch in batches)
            {
                var trimmedBatch = batch.Trim();
                if (string.IsNullOrEmpty(trimmedBatch))
                    continue;

                try
                {
                    using var command = new SqlCommand(trimmedBatch, connection);
                    await command.ExecuteNonQueryAsync();

                    var preview = trimmedBatch.Length > 50
                        ? trimmedBatch.Substring(0, 50) + "..."
                        : trimmedBatch;
                    _logger.Info($"Выполнен батч: {preview}");
                }
                catch (SqlException ex)
                {
                    var preview = trimmedBatch.Length > 100
                        ? trimmedBatch.Substring(0, 100)
                        : trimmedBatch;
                    _logger.Error($"Ошибка при выполнении батча: {preview}", ex);
                    throw;
                }
            }
        }

        private string GetMasterConnectionString()
        {
            var originalConnectionString = _databaseConnection.GetConnectionString();
            var builder = new SqlConnectionStringBuilder(originalConnectionString)
            {
                InitialCatalog = "master"
            };

            return builder.ConnectionString;
        }

        private string GetDataDirectory()
        {
            var appDomain = AppDomain.CurrentDomain;
            var dataDirectory = appDomain.GetData("DataDirectory")?.ToString();

            if (string.IsNullOrEmpty(dataDirectory))
            {
                dataDirectory = Path.Combine(appDomain.BaseDirectory, "App_Data");
            }

            return dataDirectory;
        }

        private async Task<string> LoadScriptFromResourceAsync()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = _options.CreateScriptResourceName;

            using var stream = assembly.GetManifestResourceStream(resourceName)
                ?? throw new InvalidOperationException($"Ресурс '{resourceName}' не найден.");

            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync();
        }

        private static bool IsDatabaseAttachError(SqlException ex)
        {
            return ex.Message.Contains("attach") ||
                   ex.Message.Contains("database");
        }
    }
}