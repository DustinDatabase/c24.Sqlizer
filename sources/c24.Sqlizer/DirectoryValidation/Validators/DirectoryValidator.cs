using System.Collections.Generic;

namespace c24.Sqlizer.DirectoryValidation.Validators
{
    public class DirectoryValidator : IDirectoryValidator
    {
        private readonly IEnumerable<IDirectoryValidationRule> _directoryValidationRules;

        public DirectoryValidator(IEnumerable<IDirectoryValidationRule> directoryValidationRules)
        {
            _directoryValidationRules = directoryValidationRules;
        }

        public void Validate(string directoryPath)
        {
            foreach (var directoryValidationRule in _directoryValidationRules)
            {
                directoryValidationRule.Validate(directoryPath);
            }
        }
    }
}