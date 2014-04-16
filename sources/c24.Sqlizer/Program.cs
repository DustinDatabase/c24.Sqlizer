using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using c24.Sqlizer.Infrastructure.Logging;
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
        public static void Run([Required(Description = "Directory with *.sql scripts")] string scriptsDirectory,
            [Required(Description = "SQL server and instance name")] string server,
            [Required(Description = "Database name")] string databaseName,
            [Optional(null, Description = "Login for sql server")] string login,
            [Optional(null, Description = "Password for sql server")] string password,
            [Optional(null, Description = "Directory for log file")]string logDirectory)
        {
            var sqlizer = ApplicationConfigurator.GetSqlizerInstance(scriptsDirectory,
                                                                    server,
                                                                    databaseName,
                                                                    login,
                                                                    password,
                                                                    logDirectory);

            sqlizer.RunDatabaseScripts(scriptsDirectory);
        }
    }
}
