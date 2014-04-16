﻿using System;
using c24.Sqlizer.DirectoryValidation.Rules;
using c24.Sqlizer.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace c24.Sqlizer.Tests.DirectoryValidation.Rules
{
    [TestFixture]
    public class EmptyDirectoryValidationRuleTests 
    {
        [Test]
        public void should_throw_exception_if_directory_is_empty()
        {
            // arrange
            var workingDirectory = FileSystemHelper.CreateTempWorkingDirectory();

            var rule = new EmptyDirectoryValidationRule();

            // act & assert
            Action action = () => rule.Validate(workingDirectory);

            action.ShouldThrow<EmptyDirectoryException>()
                .WithMessage("Directory is empty. It should have script files");
        }        
    }
}