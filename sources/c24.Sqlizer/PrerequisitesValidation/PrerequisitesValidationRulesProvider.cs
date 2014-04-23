using System;
using System.Collections.Generic;
using System.Diagnostics;

using c24.Sqlizer.PrerequisitesValidation.Rules;

namespace c24.Sqlizer.PrerequisitesValidation
{
    public class PrerequisitesValidationRulesProvider
    {
        public IEnumerable<IPrerequisiteValidationRule> GetPrerequisiteValidationRules()
        {
            yield return new SqlCmdPrerequisiteValidationRule();
        }
    }
}