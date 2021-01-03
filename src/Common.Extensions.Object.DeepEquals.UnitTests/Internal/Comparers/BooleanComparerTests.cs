using System;
using System.Collections;
using System.Collections.Generic;
using Common.Extensions.Object.DeepEquals.Internal.Comparers;
using Common.Extensions.Object.DeepEquals.Options;
using Common.Extensions.Object.DeepEquals.Ports;
using Xunit;

namespace Common.Extensions.Object.DeepEquals.UnitTests.Internal.Comparers
{
    public class BooleanComparerTests
    {
        [Theory]
        [InlineData(typeof(List<>), false)]
        [InlineData(typeof(ArrayList), false)]
        [InlineData(typeof(object[]), false)]
        [InlineData(typeof(int), false)]
        [InlineData(typeof(string), false)]
        [InlineData(typeof(DateTime), false)]
        [InlineData(typeof(bool), true)]
        public void IsComparerType_TypeVariations_ReturnsExpectedResult(Type type, bool expectedResult)
        {
            // Arrange
            var comparer = CreateComparer();

            // Act
            var result = comparer.CanCompare(type);

            // Assert
            Assert.Equal(expectedResult, result);
        }


        #region AreDeepEqual

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, false)]
        [InlineData(false, true)]
        [InlineData(true, true)]
        public void AreDeepEqual_BooleanVariations_ReturnsExpectedResult(bool a, bool b)
        {
            // Arrange
            var comparer = CreateComparer();

            // Act
            var result = comparer.AreDeepEqual(a, b, new DeepComparisonOptions());

            // Assert
            Assert.Equal(a == b, result);
        }

        #endregion

        #region Helpers

        private IDeepEqualityComparer CreateComparer()
        {
            return new BooleanComparer();
        }

        #endregion
    }
}
