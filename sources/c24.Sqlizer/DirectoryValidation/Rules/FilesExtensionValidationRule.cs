using System;
using System.IO;
using System.Linq;
using c24.Sqlizer.Exceptions;

namespace c24.Sqlizer.DirectoryValidation.Rules
{
    public class FilesExtensionValidationRule : IDirectoryValidationRule
    {
        private const string AllowedExtension = ".sql";

        public void Validate(string directoryPath)
        {
            var directory = new DirectoryInfo(directoryPath);

            var hasForbiddenFiles = directory.GetFiles()
                .Any(f => !f.Name.EndsWith(AllowedExtension));

            if (hasForbiddenFiles)
            {
                throw new UnsupportedFileExtensionException(string.Format("Only *.{0} files are allowed", AllowedExtension));
            }
        }
    }
}