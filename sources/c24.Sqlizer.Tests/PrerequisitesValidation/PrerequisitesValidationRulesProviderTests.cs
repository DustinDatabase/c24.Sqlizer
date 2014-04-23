using System.Linq;

using c24.Sqlizer.PrerequisitesValidation;

using NUnit.Framework;

namespace c24.Sqlizer.Tests.PrerequisitesValidation
{
    [TestFixture]
    public class PrerequisitesValidationRulesProviderTests
    {
        [Test]
        public void Should_Return_All_Existing_Rules()
        {
            // arrange
            var expectedRuleTypes = typeof(IPrerequisiteValidationRule).Assembly
                .GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface && (typeof(IPrerequisiteValidationRule).IsAssignableFrom(t)))
                .ToList();

            // act
            var actualRuleTypes = new PrerequisitesValidationRulesProvider()
                .GetPrerequisiteValidationRules()
                .Select(r => r.GetType())
                .ToList();

            // assert
            Assert.That(actualRuleTypes.Count, Is.EqualTo(expectedRuleTypes.Count));

            CollectionAssert.AreEquivalent(expectedRuleTypes, actualRuleTypes);
        }
    }
}