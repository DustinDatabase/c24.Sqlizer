using c24.Sqlizer.DirectoryValidation;
using c24.Sqlizer.Infrastructure.Logging;
using c24.Sqlizer.PrerequisitesValidation;
using c24.Sqlizer.ScriptsExecution.Executors;

namespace c24.Sqlizer
{
    public static class ApplicationConfigurator
    {
        public static Sqlizer GetSqlizerInstance(
            string serverName,
            string databaseName,
            string login,
            string password,
            string fileNamesPattern,
            string logDirectory)
        {            
            var logger = new SimpleLogger(logDirectory);

            var directoryValidationRules = new DirectoryValidationRulesProvider(fileNamesPattern)
                .GetDirectoryValidationRules();

            var prerequisitesValidationRules = new PrerequisitesValidationRulesProvider()
                .GetPrerequisiteValidationRules();

            var executor = new SqlCmdScriptExecutor(serverName, databaseName, login, password, logger);

            var sqlizer = new Sqlizer(directoryValidationRules, prerequisitesValidationRules, executor, logger);

            return sqlizer;
        }
    }
}