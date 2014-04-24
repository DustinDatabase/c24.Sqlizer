using System.Collections;
using System.Collections.Generic;
using System.Linq;

using c24.Sqlizer.DirectoryValidation.Rules;

namespace c24.Sqlizer.DirectoryValidation
{
    public class DirectoryValidationRulesProvider
    {
        private readonly string fileNamesPattern;

        public DirectoryValidationRulesProvider(string fileNamesPattern)
        {
            this.fileNamesPattern = fileNamesPattern;
        }

        public IEnumerable<IDirectoryValidationRule> GetDirectoryValidationRules()
        {
            yield return new EmptyDirectoryValidationRule();
            yield return new SubdirectoriesValidationRule();
            yield return new FileNamePatternValidationRule(fileNamesPattern);
            yield return new FilesExtensionValidationRule();
            yield return new FilesOrderValidationRule();
        }
    }
}