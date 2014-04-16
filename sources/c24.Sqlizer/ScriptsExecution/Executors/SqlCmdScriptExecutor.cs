using System;
using System.Collections.Generic;
using c24.Runman;
using c24.Sqlizer.Infrastructure.Logging;

namespace c24.Sqlizer.ScriptsExecution.Executors
{
    public class SqlCmdScriptExecutor : IScriptsExecutor
    {
        private readonly string _serverName;
        private readonly string _databaseName;
        private readonly string _login;
        private readonly string _password;
        private readonly ILogger _logger;

        public SqlCmdScriptExecutor(string serverName, string databaseName, string login, string password, ILogger logger)
        {
            _serverName = serverName;
            _databaseName = databaseName;
            _login = login;
            _password = password;
            _logger = logger;
        }

        public void Execute(string scriptFileName)
        {
            var args = GetArgs(scriptFileName);

            var startInfo = CommandLineProgram.Prepare()
                .WithFileName(@"sqlcmd.exe")
                .WithArguments(args);

            _logger.Log("Executing file: {0}", scriptFileName);

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
                _logger.Log(text);   
            }            
        }

        private string[] GetArgs(string scriptFileName)
        {
            var args = new List<string>();

            args.Add(string.Format("-S {0}", _serverName));

            if (!string.IsNullOrWhiteSpace(_login) && !string.IsNullOrWhiteSpace(_password))
            {
                args.Add(string.Format("-U {0}", _login));
                args.Add(string.Format("-P {0}", _password));
            }

            args.Add(string.Format("-d {0}", _databaseName));
            args.Add(string.Format("-i {0}", scriptFileName));
            args.Add("-b");

            return args.ToArray();
        }
    }
}