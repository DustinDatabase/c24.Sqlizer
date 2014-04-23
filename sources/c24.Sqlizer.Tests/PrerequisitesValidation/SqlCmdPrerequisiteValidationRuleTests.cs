using System;

using c24.Sqlizer.Exceptions;
using c24.Sqlizer.PrerequisitesValidation.Rules;

using FluentAssertions;

using NUnit.Framework;

namespace c24.Sqlizer.Tests.PrerequisitesValidation
{
    [TestFixture, Explicit]
    public class SqlCmdPrerequisiteValidationRuleTests
    {
        [Test, Explicit]
        public void Should_Throw_Exception_If_No_Sqlcmd_Found()
        {
            // arrange
            var rule = new SqlCmdPrerequisiteValidationRule();

            // act & assert
            Action action = rule.Validate;

            action.ShouldThrow<SqlCmdNotFoundException>();
        }
    }
}