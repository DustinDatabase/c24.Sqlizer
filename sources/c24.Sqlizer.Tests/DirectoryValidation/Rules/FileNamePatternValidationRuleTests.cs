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
        public void Should_Throw_Exception_If_File_Name_Does_Not_Match_Pattern()
        {
            // arrange
            var workingDirectory = TestFileSystem.CreateTempWorkingDirectory();

            TestFileSystem.CreateFile(workingDirectory, "Script_01.sql");
            TestFileSystem.CreateFile(workingDirectory, "Script_02.sql");
            TestFileSystem.CreateFile(workingDirectory, "someWrongName.sql");

            var rule = new FileNamePatternValidationRule(@"^[a-zA-Z]+_\d\d\.sql$");
            
            // act & assert
            Action action = () => rule.Validate(workingDirectory);

            action.ShouldThrow<FileNameFormatException>()
                .WithMessage("File(s) with unexpected name format were found");
        }

        [Test]
        public void Should_Not_Throw_Exception_If_Pattern_Is_Null_Or_Empty()
        {
            // arrange
            var workingDirectory = TestFileSystem.CreateTempWorkingDirectory();

            TestFileSystem.CreateFile(workingDirectory, "Script_01.sql");
            TestFileSystem.CreateFile(workingDirectory, "Script_02.sql");

            var rule = new FileNamePatternValidationRule(pattern: null);
            // act & assert
            Action action = () => rule.Validate(workingDirectory);

            action.ShouldNotThrow();
        }
    }
}