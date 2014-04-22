using System.Collections.Generic;

namespace c24.Sqlizer.DirectoryValidation.Validators
{
    public class DirectoryValidator : IDirectoryValidator
    {
        private readonly IEnumerable<IDirectoryValidationRule> directoryValidationRules;

        public DirectoryValidator(IEnumerable<IDirectoryValidationRule> directoryValidationRules)
        {
            this.directoryValidationRules = directoryValidationRules;
        }

        public void Validate(string directoryPath)
        {
            foreach (var directoryValidationRule in this.directoryValidationRules)
            {
                directoryValidationRule.Validate(directoryPath);
            }
        }
    }
}