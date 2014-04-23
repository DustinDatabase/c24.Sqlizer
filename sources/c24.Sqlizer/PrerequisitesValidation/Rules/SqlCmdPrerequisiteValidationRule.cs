using System;
using System.IO;

using c24.Sqlizer.Exceptions;

using Microsoft.Win32;

namespace c24.Sqlizer.PrerequisitesValidation.Rules
{
    public class SqlCmdPrerequisiteValidationRule : IPrerequisiteValidationRule
    {
        public void Validate()
        {
            var clientSetupKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64)
                .OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server\100\Tools\ClientSetup");
            if (clientSetupKey == null)
            {
                throw new SqlCmdNotFoundException("MS SQL client tools are not registered");
            }
            var sqlClientFolderPath = (string)clientSetupKey.GetValue("Path");
            
            if (sqlClientFolderPath == null)
            {
                throw new SqlCmdNotFoundException("MS SQL client tools are not registered");
            }

            var sqlCmdPath = Path.Combine(sqlClientFolderPath, "sqlcmd.exe");

            if (!File.Exists(sqlCmdPath))
            {
                throw new SqlCmdNotFoundException("SQLCMD.EXE not found in the system");
            }
        }
    }
}