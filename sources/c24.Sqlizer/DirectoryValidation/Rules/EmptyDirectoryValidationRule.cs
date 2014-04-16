using System;
using System.IO;
using System.Linq;
using c24.Sqlizer.Exceptions;

namespace c24.Sqlizer.DirectoryValidation.Rules
{
    public class EmptyDirectoryValidationRule : IDirectoryValidationRule
    {
        public void Validate(string directoryPath)
        {
            var directory = new DirectoryInfo(directoryPath);

            var hasFiles = directory.GetFiles().Any();

            if (!hasFiles)
            {
                throw new EmptyDirectoryException("Directory is empty. It should have script files");
            }
        }
    }
}