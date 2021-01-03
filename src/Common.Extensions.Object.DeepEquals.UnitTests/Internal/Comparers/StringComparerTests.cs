using System;
using System.Collections;
using System.Collections.Generic;
using Common.Extensions.Object.DeepEquals.Options;
using Common.Extensions.Object.DeepEquals.Ports;
using Xunit;
using StringComparer = Common.Extensions.Object.DeepEquals.Internal.Comparers.StringComparer;

namespace Common.Extensions.Object.DeepEquals.UnitTests.Internal.Comparers
{
    public class StringComparerTests
    {
        #region IsComparerType

        [Theory]
        [InlineData(typeof(List<>), false)]
        [InlineData(typeof(ArrayList), false)]
        [InlineData(typeof(object[]), false)]
        [InlineData(typeof(int), false)]
        [InlineData(typeof(string), true)]
        public void IsComparerType_TypeVariations_ReturnsExpectedResult(Type type, bool expectedResult)
        {
            // Arrange
            var comparer = CreateComparer();

            // Act
            var result = comparer.CanCompare(type);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        #endregion

        #region AreDeepEqual

        [Theory]
        [InlineData("Test", "Test", true)]
        [InlineData("", "", true)]
        [InlineData("  Test  ", "  Test  ", true)]
        [InlineData("TEst", "Test", false)]
        [InlineData("  A w E SO M3 tEsT  ", "  a W e so m3 TeSt  ", false)]
        [InlineData("Unknown", "Test", false)]
        public void AreDeepEqual_CaseSensitive_StringVariations_ReturnsExpectedResult(string a, string b, bool expectedResult)
        {
            // Arrange
            var comparer = CreateComparer();
            var options = new DeepComparisonOptions()
            {
                IgnoreCaseSensitivity = false
            };

            // Act
            var result = comparer.AreDeepEqual(a, b, options);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("Test", "Test", true)]
        [InlineData("", "", true)]
        [InlineData("  Test  ", "  Test  ", true)]
        [InlineData("TEst", "Test", true)]
        [InlineData("  A w E SO M3 tEsT  ", "  a W e so m3 TeSt  ", true)]
        [InlineData("Unknown", "Test", false)]
        public void AreDeepEqual_CaseInsensitive_StringVariations_ReturnsExpectedResult(string a, string b, bool expectedResult)
        {
            // Arrange
            var comparer = CreateComparer();
            var options = new DeepComparisonOptions()
            {
                IgnoreCaseSensitivity = true
            };

            // Act
            var result = comparer.AreDeepEqual(a, b, options);

            // Assert
            Assert.Equal(expectedResult, result);
        }


        #endregion

        #region Helpers

        private IDeepEqualityComparer CreateComparer()
        {
            return new StringComparer();
        }

        #endregion
    }
}
