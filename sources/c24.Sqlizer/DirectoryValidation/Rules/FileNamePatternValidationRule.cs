using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using c24.Sqlizer.Exceptions;

namespace c24.Sqlizer.DirectoryValidation.Rules
{
    public class FileNamePatternValidationRule : IDirectoryValidationRule
    {
        private readonly string _pattern;

        public FileNamePatternValidationRule(string pattern)
        {
            _pattern = pattern;
        }

        public void Validate(string directoryPath)
        {
            var regex = new Regex(_pattern);

            var directory = new DirectoryInfo(directoryPath);

            var hasUnexpectedFiles = directory.GetFiles()
                .Any(f => !regex.IsMatch(f.Name));

            if (hasUnexpectedFiles)
            {
                throw new FileNameFormatException("File(s) with unexpected name format were found");
            }
        }
    }
}