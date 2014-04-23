using System.Linq;

using c24.Sqlizer.DirectoryValidation;

using NUnit.Framework;

namespace c24.Sqlizer.Tests.DirectoryValidation
{
    [TestFixture]
    public class DirectoryValidationRulesProviderTests
    {
        [Test]
        public void Should_Return_All_Existing_Rules()
        {
            // arrange
            var expectedRuleTypes = typeof(IDirectoryValidationRule).Assembly
                .GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface && (typeof(IDirectoryValidationRule).IsAssignableFrom(t)))
                .ToList();

            // act
            var actualRuleTypes = new DirectoryValidationRulesProvider(@"fakeFile.fakeExtension")
                .GetDirectoryValidationRules()
                .Select(r => r.GetType())
                .ToList();

            // assert
            Assert.That(actualRuleTypes.Count, Is.EqualTo(expectedRuleTypes.Count));

            CollectionAssert.AreEquivalent(expectedRuleTypes, actualRuleTypes);
        }
    }
}