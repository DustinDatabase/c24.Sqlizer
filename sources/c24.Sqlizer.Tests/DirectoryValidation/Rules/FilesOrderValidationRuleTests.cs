using System;
using c24.Sqlizer.DirectoryValidation.Rules;
using c24.Sqlizer.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace c24.Sqlizer.Tests.DirectoryValidation.Rules
{
    [TestFixture]
    public class FilesOrderValidationRuleTests
    {
        [Test]
        public void Should_Throw_Exception_If_Gap_Between_Files()
        {
            // arrange
            var workingDirectory = FileSystemHelper.CreateTempWorkingDirectory();

            FileSystemHelper.CreateFile(baseDirectory: workingDirectory, fileName: "0001_script.sql");            
            FileSystemHelper.CreateFile(baseDirectory: workingDirectory, fileName: "0003_script.sql");

            var rule = new FilesOrderValidationRule();
            
            // act & assert
            Action action = () => rule.Validate(workingDirectory);

            action.ShouldThrow<WrongOrderException>()
                .WithMessage("There is a gap between scripts. Files should be contiguous");
        }
    }
}