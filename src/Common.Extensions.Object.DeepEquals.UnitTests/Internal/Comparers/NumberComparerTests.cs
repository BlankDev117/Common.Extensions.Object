using System;
using Common.Extensions.Object.DeepEquals.Internal.Comparers;
using Common.Extensions.Object.DeepEquals.Options;
using Common.Extensions.Object.DeepEquals.Ports;
using Common.Extensions.Object.DeepEquals.UnitTests.Helpers;
using Xunit;

namespace Common.Extensions.Object.DeepEquals.UnitTests.Internal.Comparers
{
    public class NumberComparerTests
    {
        #region IsComparerType

        [Theory]
        [InlineData(typeof(byte), true)]
        [InlineData(typeof(sbyte), true)]
        [InlineData(typeof(char), true)]
        [InlineData(typeof(decimal), true)]
        [InlineData(typeof(double), true)]
        [InlineData(typeof(float), true)]
        [InlineData(typeof(int), true)]
        [InlineData(typeof(uint), true)]
        [InlineData(typeof(long), true)]
        [InlineData(typeof(ulong), true)]
        [InlineData(typeof(short), true)]
        [InlineData(typeof(ushort), true)]
        [InlineData(typeof(string), false)]
        [InlineData(typeof(object), false)]
        [InlineData(typeof(MockEnum), false)]
        public void IsComparerType_DifferentTypes_ReturnsExpectedResult(Type type, bool shouldAccept)
        {
            // Arrange
            var comparer = CreateComparer();

            // Act
            var result = comparer.CanCompare(type);

            // Assert
            Assert.Equal(shouldAccept, result);
        }

        #endregion

        #region AreDeepEqual

        [Fact]
        public void AreDeepEqual_TypeIsNotNumeric_ThrowsInvalidOperationException()
        {
            // Arrange
            var comparer = CreateComparer();

            // Act/Assert
            Assert.Throws<InvalidOperationException>(() =>
                comparer.AreDeepEqual("Test", "test", new DeepComparisonOptions()));
        }

        [Theory]
        [InlineData(0, 1, false)]
        [InlineData(10, 10, true)]
        [InlineData(1d, 1d, true)]
        [InlineData(-2L, -1L, false)]
        [InlineData(20.12, 20.12, true)]
        [InlineData(20.12, 20.121, false)]
        public void AreDeepEqual_NumericVariations_ReturnsExpectedResult(object a, object b, bool expectedResult)
        {
            // Arrange
            var comparer = CreateComparer();

            // Act
            var result = comparer.AreDeepEqual(a, b, new DeepComparisonOptions());

            // Assert
            Assert.Equal(expectedResult, result);
        }

        #endregion

        #region Helpers

        private IDeepEqualityComparer CreateComparer()
        {
            return new NumberComparer();
        }

        #endregion
    }
}
