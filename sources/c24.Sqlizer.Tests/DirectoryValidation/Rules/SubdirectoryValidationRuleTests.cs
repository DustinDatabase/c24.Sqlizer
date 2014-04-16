using System;
using System.IO;
using c24.Sqlizer.DirectoryValidation.Rules;
using c24.Sqlizer.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace c24.Sqlizer.Tests.DirectoryValidation.Rules
{
    [TestFixture]
    public class SubdirectoryValidationRuleTests
    {               
        [Test]
        public void should_throw_exception_if_directory_has_subdirectories()
        {
            // arrange
            var workingDirectory = FileSystemHelper.CreateTempWorkingDirectory();

            FileSystemHelper.CreateSubdirectory(baseDirectory: workingDirectory, subdirectoryName: "subdir");

            var rule = new SubdirectoriesValidationRule();

            // act & assert
            Action action = () => rule.Validate(workingDirectory);

            action.ShouldThrow<DirectoryHasSubdirectoriesException>()
                .WithMessage("Directory with database scripts must have no subdirectories");
        }
    }
}