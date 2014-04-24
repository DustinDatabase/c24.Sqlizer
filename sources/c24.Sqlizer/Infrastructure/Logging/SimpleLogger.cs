using System;
using System.IO;

namespace c24.Sqlizer.Infrastructure.Logging
{
    public class SimpleLogger : ILogger
    {        
        private readonly string loggerPath;

        public SimpleLogger(string logsDirectory)
        {
            logsDirectory = logsDirectory ?? string.Empty;

            CreateLoggerDirectoryIfNeeded(logsDirectory);

            this.loggerPath = Path.Combine(logsDirectory, "log.txt");            
        }        

        public void Log(string text)
        {
            var formatedText = GetFormatedText(text);

            Log2Console(formatedText);

            Log2File(formatedText);
        }

        public void Log(string text, params object[] args)
        {
            var logMessage = string.Format(text, args);

            this.Log(logMessage);
        }

        public void Log(Exception e)
        {
            var consoleText = GetFormatedText(e.Message);

            Log2Console(consoleText);

            var fileText = GetFormatedText(string.Format("{0}\r\n{1}", e.Message, e.StackTrace));

            Log2File(fileText);

        }

        private string GetFormatedText(string text)
        {
            return string.Format("{0} - {1}", DateTime.Now, text);
        }

        private void Log2File(string text)
        {
            using (var writer = new StreamWriter(this.loggerPath, append: true))
            {
                writer.WriteLine(text);
            }   
        }

        private void Log2Console(string text)
        {
            Console.WriteLine(text);
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