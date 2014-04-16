using System.Configuration;
using c24.Sqlizer.DirectoryValidation;
using c24.Sqlizer.DirectoryValidation.Rules;
using c24.Sqlizer.DirectoryValidation.Validators;
using c24.Sqlizer.Infrastructure.Logging;
using c24.Sqlizer.ScriptsExecution.Executors;

namespace c24.Sqlizer
{
    public class ApplicationConfigurator
    {
        public static Sqlizer GetSqlizerInstance(string scriptsDirectory,
            string serverName,
            string databaseName,
            string login,
            string password,
            string logDirectory)
        {            
            var logger = new SimpleLogger(logDirectory);

            var filesPattern = ConfigurationManager.AppSettings["filesPattern"];            

            var validator = new DirectoryValidator(new IDirectoryValidationRule[]
            {
                new EmptyDirectoryValidationRule(), 
                new SubdirectoriesValidationRule(), 
                new FileNamePatternValidationRule(filesPattern), 
                new FilesExtensionValidationRule(), 
            });

            var executor = new SqlCmdScriptExecutor(serverName, databaseName, login, password, logger);

            var sqlizer = new Sqlizer(validator, executor, logger);

            return sqlizer;
        }
    }
}