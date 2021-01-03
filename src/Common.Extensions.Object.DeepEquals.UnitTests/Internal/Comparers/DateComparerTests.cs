using System;
using System.Collections;
using System.Collections.Generic;
using Common.Extensions.Object.DeepEquals.Internal.Comparers;
using Common.Extensions.Object.DeepEquals.Options;
using Common.Extensions.Object.DeepEquals.Ports;
using Xunit;

namespace Common.Extensions.Object.DeepEquals.UnitTests.Internal.Comparers
{
    public class DateComparerTests
    {
        #region IsComparerType

        [Theory]
        [InlineData(typeof(List<>), false)]
        [InlineData(typeof(ArrayList), false)]
        [InlineData(typeof(object[]), false)]
        [InlineData(typeof(int), false)]
        [InlineData(typeof(string), false)]
        [InlineData(typeof(DateTime), true)]
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
        [InlineData("2024-07-24T00:00:00Z", "2024-07-24T00:00:00Z", true)]
        [InlineData("2020-07-24T00:00:00Z", "2024-07-24T00:00:00Z", false)]
        public void AreDeepEqual_DateTimeVariations_ReturnsExpectedResult(string a, string b, bool expectedResult)
        {
            // Arrange
            var dateA = DateTime.Parse(a);
            var dateB = DateTime.Parse(b);
            
            var comparer = CreateComparer();

            // Act
            var result = comparer.AreDeepEqual(dateA, dateB, new DeepComparisonOptions());

            // Assert
            Assert.Equal(expectedResult, result);
        }

        #endregion

        #region Helpers

        private IDeepEqualityComparer CreateComparer()
        {
            return new DateComparer();
        }

        #endregion
    }
}
