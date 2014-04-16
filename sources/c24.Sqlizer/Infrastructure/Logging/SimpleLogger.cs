using System;
using System.IO;

namespace c24.Sqlizer.Infrastructure.Logging
{
    public class SimpleLogger : ILogger
    {        
        private readonly string _loggerPath;

        public SimpleLogger(string logsDirectory)
        {
            logsDirectory = logsDirectory ?? string.Empty;

            CreateLoggerDirectoryIfNeeded(logsDirectory);

            _loggerPath = Path.Combine(logsDirectory, "log.txt");            
        }        

        public void Log(string text)
        {
            using (var writer = new StreamWriter(_loggerPath, append: true))
            {
                writer.WriteLine("{0} - {1}", DateTime.Now, text);
            }
        }

        public void Log(string text, params object[] args)
        {
            var logMessage = string.Format(text, args);

            this.Log(logMessage);
        }

        private void CreateLoggerDirectoryIfNeeded(string logsDirectory)
        {
            if (string.IsNullOrWhiteSpace(logsDirectory))
            {
                return;
            }

            if (!Directory.Exists(logsDirectory))
            {
                Directory.CreateDirectory(logsDirectory);
            }
        }
    }
}