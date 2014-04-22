using System;
using System.Collections.Generic;
using c24.Runman;
using c24.Sqlizer.Infrastructure.Logging;

namespace c24.Sqlizer.ScriptsExecution.Executors
{
    public class SqlCmdScriptExecutor : IScriptsExecutor
    {
        private readonly string serverName;
        private readonly string databaseName;
        private readonly string login;
        private readonly string password;
        private readonly ILogger logger;

        public SqlCmdScriptExecutor(string serverName, string databaseName, string login, string password, ILogger logger)
        {
            this.serverName = serverName;
            this.databaseName = databaseName;
            this.login = login;
            this.password = password;
            this.logger = logger;
        }

        public void Execute(string scriptFileName)
        {
            var args = GetArgs(scriptFileName);

            var startInfo = CommandLineProgram.Prepare()
                .WithFileName(@"sqlcmd.exe")
                .WithArguments(args);

            this.logger.Log("Executing file: {0}", scriptFileName);

            var result = CommandLineProgram.Run(startInfo);

            if (result.ExitCode == 0)
            {                
                LogIfHasSomething(result.Output);
            }
            else
            {
                LogIfHasSomething(result.Error);

                throw new InvalidOperationException(
                    string.Format(
                        "Executing {0} finished with the exit code: {1}. Please check log file for more details.",
                        scriptFileName, result.ExitCode));
            }
        }

        private void LogIfHasSomething(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                this.logger.Log(text);   
            }            
        }

        private string[] GetArgs(string scriptFileName)
        {
            var args = new List<string>();

            args.Add(string.Format("-S {0}", this.serverName));

            if (!string.IsNullOrWhiteSpace(this.login) && !string.IsNullOrWhiteSpace(this.password))
            {
                args.Add(string.Format("-U {0}", this.login));
                args.Add(string.Format("-P {0}", this.password));
            }

            args.Add(string.Format("-d {0}", this.databaseName));
            args.Add(string.Format("-i {0}", scriptFileName));
            args.Add("-b");

            return args.ToArray();
        }
    }
}