using System;
using Common.Extensions.Object.DeepEquals.Internal.Comparers;
using Common.Extensions.Object.DeepEquals.Options;
using Common.Extensions.Object.DeepEquals.Ports;
using Common.Extensions.Object.DeepEquals.UnitTests.Helpers;
using Xunit;

namespace Common.Extensions.Object.DeepEquals.UnitTests.Internal.Comparers
{
    public class EnumComparerTests
    {
        #region IsComparerType

        [Theory]
        [InlineData(typeof(MockStruct), false)]
        [InlineData(typeof(int), false)]
        [InlineData(typeof(object), false)]
        [InlineData(typeof(string), false)]
        [InlineData(typeof(MockEnum), true)]
        public void IsComparerType_TypeVariations_ReturnsExpectedResults(Type type, bool expectedResult)
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
        [InlineData(MockEnum.AwesomeTest, MockEnum.EpicTest, false)]
        [InlineData(MockEnum.Test, MockEnum.AwesomeTest, false)]
        [InlineData(MockEnum.EpicTest, MockEnum.EpicTest, true)]
        [InlineData(MockEnum.Test, MockEnum.Test, true)]
        public void AreDeepEqual_EnumVariations_ReturnsExpectedResult(MockEnum valueA, MockEnum valueB,
            bool shouldEqual)
        {
            // Arrange
            var comparer = CreateComparer();

            // Act
            var result = comparer.AreDeepEqual(valueA, valueB, new DeepComparisonOptions());

            // Assert
            Assert.Equal(shouldEqual, result);
        }

        #endregion

        #region Helpers

        private IDeepEqualityComparer CreateComparer()
        {
            return new EnumComparer();
        }

        #endregion
    }
}
