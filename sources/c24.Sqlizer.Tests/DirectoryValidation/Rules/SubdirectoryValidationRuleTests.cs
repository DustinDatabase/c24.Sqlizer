using System;
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
        public void Should_Throw_Exception_If_Directory_Has_Subdirectories()
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