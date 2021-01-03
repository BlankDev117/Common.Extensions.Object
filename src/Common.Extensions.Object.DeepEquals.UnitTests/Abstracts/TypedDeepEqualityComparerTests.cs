using System;
using Common.Extensions.Object.DeepEquals.Abstracts;
using Common.Extensions.Object.DeepEquals.Options;
using Moq;
using Xunit;

namespace Common.Extensions.Object.DeepEquals.UnitTests.Abstracts
{
    public class TypedDeepEqualityComparerTests
    {
        #region IsComparerType

        [Fact]
        public void IsComparerType_TypeNotOfTypedComparer_ReturnsFalse()
        {
            // Arrange
            var comparer = CreateComparer<int>();

            // Act
            var result = comparer.CanCompare(typeof(float));

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsComparerType_TypeSameAsTypedComparer_ReturnsTrue()
        {
            // Arrange
            var comparer = CreateComparer<int>();

            // Act
            var result = comparer.CanCompare(typeof(int));

            // Assert
            Assert.True(result);
        }

        #endregion

        #region AreDeepEqual

        [Fact]
        public void AreDeepEqual_ObjectsNotOfComparerType_ThrowsInvalidOperationException()
        {
            // Arrange
            var comparer = CreateComparer<int>();

            // Act/Assert
            Assert.Throws<InvalidOperationException>(() => comparer.AreDeepEqual("Test", "test", new DeepComparisonOptions()));
        }

        [Fact]
        public void AreDeepEqual_ObjectsComparerType_RunsSuccessfully()
        {
            // Arrange
            var comparer = CreateComparer<int>();

            // Act
            comparer.AreDeepEqual(1, 2, new DeepComparisonOptions());

            // Assert
            Assert.True(true);
        }

        #endregion

        #region Helpers

        private TypedDeepEqualityComparer<TType> CreateComparer<TType>()
        {
            var typedEqualityComparer = new Mock<TypedDeepEqualityComparer<TType>>()
            {
                CallBase = true
            };

            return typedEqualityComparer.Object;
        }

        #endregion
    }
}
