using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using c24.Sqlizer.DirectoryValidation;
using c24.Sqlizer.Infrastructure.Logging;
using c24.Sqlizer.PrerequisitesValidation;
using c24.Sqlizer.ScriptsExecution;

namespace c24.Sqlizer
{
    public class Sqlizer
    {
        private readonly IEnumerable<IDirectoryValidationRule> directoryValidationRules;
        private readonly IEnumerable<IPrerequisiteValidationRule> prerequisiteValidationRules;
        private readonly IScriptsExecutor scriptsExecutor;
        private readonly ILogger logger;

        public Sqlizer(IEnumerable<IDirectoryValidationRule> directoryValidationRules,
            IEnumerable<IPrerequisiteValidationRule> prerequisiteValidationRules,
            IScriptsExecutor scriptsExecutor,
            ILogger logger)
        {            
            this.directoryValidationRules = directoryValidationRules;
            this.prerequisiteValidationRules = prerequisiteValidationRules;
            this.scriptsExecutor = scriptsExecutor;
            this.logger = logger;
        }

        public bool RunDatabaseScripts(string scriptsDirectory)
        {
            try
            {
                this.logger.Log("# Start application #");

                ValidatePrerequisites();

                ValidateDirectoryPath(scriptsDirectory);

                ValidateDirectoryContent(scriptsDirectory);

                var scripts = GetFilesToExecute(scriptsDirectory);

                foreach (var script in scripts)
                {
                    this.scriptsExecutor.Execute(script);
                }

                return true;
            }
            catch (Exception e)
            {
                this.logger.Log(e);

                return false;
            }
            finally
            {
                this.logger.Log("# Close application #\r\n\r\n");
            }
        }

        private IEnumerable<string> GetFilesToExecute(string scriptsDirectory)
        {
            return new DirectoryInfo(scriptsDirectory)
                .GetFiles()
                .Select(f => f.FullName)
                .ToList();
        }

        private void ValidatePrerequisites()
        {
            foreach (var prerequisiteValidationRule in prerequisiteValidationRules)
            {
                prerequisiteValidationRule.Validate();
            }
        }

        private void ValidateDirectoryContent(string scriptsDirectory)
        {
            foreach (var directoryValidationRule in directoryValidationRules)
            {
                directoryValidationRule.Validate(scriptsDirectory);
            }
        }

        private void ValidateDirectoryPath(string scriptsDirectory)
        {
            this.logger.Log("Check if scripts directory exists: {0}", scriptsDirectory);

            var directory = new DirectoryInfo(scriptsDirectory);

            if (!directory.Exists)
            {
                throw new ArgumentException(string.Format("Directory {0} does not exist", scriptsDirectory));
            }

            this.logger.Log("Directory {0} exists", scriptsDirectory);
        }
    }
}