using System;
using System.IO;

using c24.Runman;
using c24.Sqlizer.Exceptions;

using Microsoft.Win32;

namespace c24.Sqlizer.PrerequisitesValidation.Rules
{
    public class SqlCmdPrerequisiteValidationRule : IPrerequisiteValidationRule
    {
        public void Validate()
        {
            var startInfo = CommandLineProgram.Prepare()
               .WithFileName(@"sqlcmdbb.exe")
               .WithArguments(new []{ "-?" });

            try
            {
                CommandLineProgram.Run(startInfo);
            }
            catch (System.ComponentModel.Win32Exception)
            {
                throw new SqlCmdNotFoundException("SQLCMD is not found in your system");
            }
        }
    }
}