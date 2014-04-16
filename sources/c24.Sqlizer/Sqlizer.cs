using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using c24.Sqlizer.DirectoryValidation;
using c24.Sqlizer.Infrastructure.Logging;
using c24.Sqlizer.ScriptsExecution;

namespace c24.Sqlizer
{
    public class Sqlizer
    {        
        private readonly IDirectoryValidator _directoryValidator;
        private readonly IScriptsExecutor _scriptsExecutor;
        private readonly ILogger _logger;

        public Sqlizer(IDirectoryValidator directoryValidator, IScriptsExecutor scriptsExecutor, ILogger logger)
        {
            _directoryValidator = directoryValidator;
            _scriptsExecutor = scriptsExecutor;
            _logger = logger;
        }

        public void RunDatabaseScripts(string scriptsDirectory)
        {
            try
            {
                _logger.Log("# Start application #");

                ValidateDirectoryPath(scriptsDirectory);

                ValidateDirectoryContent(scriptsDirectory);

                var scripts = GetFilesToExecute(scriptsDirectory);

                foreach (var script in scripts)
                {
                    _scriptsExecutor.Execute(script);
                }
            }
            catch (Exception e)
            {
                _logger.Log(e.Message);

                throw;
            }
            finally
            {
                _logger.Log("# Close application #\r\n\r\n");
            }
        }

        private IEnumerable<string> GetFilesToExecute(string scriptsDirectory)
        {
            return new DirectoryInfo(scriptsDirectory)
                .GetFiles()
                .Select(f => f.FullName)
                .ToList();
        }

        private void ValidateDirectoryContent(string scriptsDirectory)
        {
            _directoryValidator.Validate(scriptsDirectory);
        }

        private void ValidateDirectoryPath(string scriptsDirectory)
        {
            _logger.Log("Check if scipts directory exists: {0}", scriptsDirectory);

            var directory = new DirectoryInfo(scriptsDirectory);

            if (!directory.Exists)
            {
                throw new ArgumentException(string.Format("Directory {0} does not exist", scriptsDirectory));
            }

            _logger.Log("Directory {0} exists", scriptsDirectory);
        }
    }
}