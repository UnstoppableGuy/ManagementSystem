using ManagementSystem.Domain.Interfaces;
using ManagementSystem.Infrastructure.Database;
using ManagementSystem.Infrastructure.Logging;
using ManagementSystem.Infrastructure.Repositories;
using ManagementSystem.UI.Forms;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;

namespace ManagementSystem.UI
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();
            ConfigureServices(services);

            var sp = services.BuildServiceProvider();

            try
            {
                var logger = sp.GetRequiredService<ILogger>();
                var databaseInitializer = sp.GetRequiredService<IDatabaseInitializer>();
                databaseInitializer.Initialize();
                Application.Run(sp.GetRequiredService<FormEmployees>());
            }
            catch (Exception ex)
            {
                var logger = sp.GetService<ILogger>();
                logger?.Error("Критическая ошибка", ex);
                MessageBox.Show(
                    "Ошибка запуска. См. логи.\n" + ex.Message,
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sp?.Dispose();
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ILogger, FileLogger>();
            services.AddSingleton<IDatabaseConnection, DatabaseConnection>();

            services.AddSingleton<IDatabaseInitializer, DatabaseInitializer>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            services.AddTransient<FormEmployees>();
            services.AddTransient<FormEmployeeEdit>();
            services.AddTransient<FormReport>();
        }
    }
}
