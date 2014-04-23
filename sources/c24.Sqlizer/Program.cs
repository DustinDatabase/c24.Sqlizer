using System;

using NConsoler;

namespace c24.Sqlizer
{
    class Program
    {        
        public static void Main(string[] args)
        {            
            Consolery.Run(typeof(Program), args);
        }
        

        [Action]
        // ReSharper disable once UnusedMember.Global
        public static void Run([Required(Description = "Directory with *.sql scripts")] string scriptsDirectory,
            [Required(Description = "SQL server and instance name")] string server,
            [Required(Description = "Database name")] string databaseName,
            [Optional(null, Description = "Login for sql server")] string login,
            [Optional(null, Description = "Password for sql server")] string password,
            [Optional(null, Description = "Regular expression for files name validation")]string fileNamesPattern,
            [Optional(null, Description = "Directory for log file")]string logDirectory)
        {
            var sqlizer = ApplicationConfigurator.GetSqlizerInstance(server,
                                                                    databaseName,
                                                                    login,
                                                                    password,
                                                                    fileNamesPattern,
                                                                    logDirectory);

            var sucess = sqlizer.RunDatabaseScripts(scriptsDirectory);

            if (!sucess)
            {
                Environment.Exit(1);
            }
        }
    }
}
