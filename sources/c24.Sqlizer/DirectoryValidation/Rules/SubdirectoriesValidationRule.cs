using System.IO;
using System.Linq;
using c24.Sqlizer.Exceptions;

namespace c24.Sqlizer.DirectoryValidation.Rules
{
    public class SubdirectoriesValidationRule : IDirectoryValidationRule
    {
        public void Validate(string directoryPath)
        {
            var directory = new DirectoryInfo(directoryPath);

            var hasSubdirectories = directory.GetDirectories().Any();

            if (hasSubdirectories)
            {
                throw new DirectoryHasSubdirectoriesException("Directory with database scripts must have no subdirectories");
            }
        }
    }
}