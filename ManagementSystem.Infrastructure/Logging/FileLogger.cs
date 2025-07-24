using ManagementSystem.Domain.Interfaces;
using System;
using System.Data.SqlClient;
using System.IO;

namespace ManagementSystem.Infrastructure.Logging
{
    public class FileLogger : ILogger
    {
        private readonly string _logFilePath;
        public FileLogger()
        {
            string logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            Directory.CreateDirectory(logDirectory);

            string date = DateTime.Now.ToString("yyyy-MM-dd");
            _logFilePath = Path.Combine(logDirectory, $"log_{date}.txt");
        }

        public void Info(string message) => Log("INFO", message);
        public void Warning(string message) => Log("WARN", message);
        public void Error(string message, Exception ex = null)
        {
            string fullMessage = message;
            if (ex != null)
            {
                fullMessage += $"\nEXCEPTION: {ex.Message}\nSTACK: {ex.StackTrace}";
            }
            Log("ERROR", fullMessage);
        }
        public void Debug(string message) => Log("DEBUG", message);

        private void Log(string level, string message)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string logEntry = $"[{timestamp}] [{level}] {message}{Environment.NewLine}";

            try
            {
                File.AppendAllText(_logFilePath, logEntry, System.Text.Encoding.UTF8);
            }
            catch { }
        }
    }
}