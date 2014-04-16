using System.Security.Cryptography.X509Certificates;

namespace c24.Sqlizer.ScriptsExecution
{
    public interface IScriptsExecutor
    {
        void Execute(string scriptFileName);
    }
}