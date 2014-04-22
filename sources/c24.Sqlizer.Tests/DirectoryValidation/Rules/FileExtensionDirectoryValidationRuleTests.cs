using System;
using c24.Sqlizer.DirectoryValidation.Rules;
using c24.Sqlizer.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace c24.Sqlizer.Tests.DirectoryValidation.Rules
{
    [TestFixture]
    public class FileExtensionDirectoryValidationRuleTests
    {
        [Test]
        public void Should_Throw_Exception_When_Forbidden_Files_Found()
        {
            // arrange
            var workingDirectory = FileSystemHelper.CreateTempWorkingDirectory();

            FileSystemHelper.CreateFile(baseDirectory: workingDirectory, fileName: "readme.txt");

            var rule = new FilesExtensionValidationRule();
           
            // act & assert
            Action action = () => rule.Validate(workingDirectory);

            action.ShouldThrow<UnsupportedFileExtensionException>()
                .WithMessage("Only *.sql files are allowed");
        }
    }
}