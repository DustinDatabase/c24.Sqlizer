using System;
using c24.Sqlizer.DirectoryValidation.Rules;
using c24.Sqlizer.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace c24.Sqlizer.Tests.DirectoryValidation.Rules
{
    [TestFixture]
    public class FileNamePatternValidationRuleTests
    {
        [Test]
        public void should_throw_exception_if_file_name_does_not_match_pattern()
        {
            // arrange
            var workingDirectory = FileSystemHelper.CreateTempWorkingDirectory();

            FileSystemHelper.CreateFile(workingDirectory, "Script_01.sql");
            FileSystemHelper.CreateFile(workingDirectory, "Script_02.sql");
            FileSystemHelper.CreateFile(workingDirectory, "someWrongName.sql");

            var rule = new FileNamePatternValidationRule(@"^[a-zA-Z]+_\d\d\.sql$");
            
            // act & assert
            Action action = () => rule.Validate(workingDirectory);

            action.ShouldThrow<FileNameFormatException>()
                .WithMessage("File(s) with unexpected name format were found");
        }
    }
}