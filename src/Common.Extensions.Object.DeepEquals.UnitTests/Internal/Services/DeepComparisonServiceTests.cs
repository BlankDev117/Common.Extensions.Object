using System;
using System.Collections.Generic;
using Common.Extensions.Object.DeepEquals.Internal.Services;
using Common.Extensions.Object.DeepEquals.Options;
using Common.Extensions.Object.DeepEquals.Ports;
using Common.Extensions.Object.DeepEquals.UnitTests.Helpers;
using Moq;
using Xunit;

namespace Common.Extensions.Object.DeepEquals.UnitTests.Internal.Services
{
    public class DeepComparisonServiceTests
    {
        #region AreDeepEqual

        [Fact]
        public void AreDeepEqual_NullObjects_ReturnsTrue()
        {
            // Arrange
            var service = CreateService();

            // Act
            var result = service.AreDeepEqual(null, null, null);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AreDeepEqual_OneNullOneNonNullObject_ReturnsFalse()
        {
            // Arrange
            var service = CreateService();

            // Act
            var result = service.AreDeepEqual(new object(), null, null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AreDeepEqual_SameObject_ShortCircuits_ReturnsTrue()
        {
            // Arrange
            var service = CreateService();
            var objA = new object();

            // Act
            var result = service.AreDeepEqual(objA, objA, null);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AreDeepEqual_DifferentObjectTypes_ReturnsFalse()
        {
            // Arrange
            var service = CreateService();
            var objA = new object();
            var objB = new MockStruct();

            // Act
            var result = service.AreDeepEqual(objA, objB, null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AreDeepEqual_SameObjectTypes_DifferentObjects_NullComparisonOptions_ThrowsArgumentNullException()
        {
            // Arrange
            var service = CreateService();
            var objA = new MockStruct();
            var objB = new MockStruct();

            // Act/Assert
            Assert.Throws<ArgumentNullException>(() => service.AreDeepEqual(objA, objB, null));
        }

        [Fact]
        public void AreDeepEqual_SameObjectTypes_DifferentObjects_ComparisonOptionsNullComparers_ThrowsInvalidOperationException()
        {
            // Arrange
            var service = CreateService();
            var objA = new MockStruct();
            var objB = new MockStruct();
            var options = new DeepComparisonOptions()
            {
                DeepEqualityComparers = null
            };

            // Act/Assert
            Assert.Throws<InvalidOperationException>(() => service.AreDeepEqual(objA, objB, options));
        }

        [Fact]
        public void AreDeepEqual_SameObjectTypes_DifferentObjects_ComparisonOptionsEmptyComparers_ThrowsInvalidOperationException()
        {
            // Arrange
            var service = CreateService();
            var objA = new MockStruct();
            var objB = new MockStruct();
            var options = new DeepComparisonOptions()
            {
                DeepEqualityComparers = new List<IDeepEqualityComparer>()
            };

            // Act/Assert
            Assert.Throws<InvalidOperationException>(() => service.AreDeepEqual(objA, objB, options));
        }

        [Fact]
        public void AreDeepEqual_SameObjectTypes_DifferentObjects_NoDeepEqualityComparersOfTheGivenObjectType_ThrowsInvalidOperationException()
        {
            // Arrange
            var service = CreateService();
            var objA = new MockStruct();
            var objB = new MockStruct();

            var mockComparer = new Mock<IDeepEqualityComparer>();
            mockComparer.Setup(m => m.CanCompare(It.IsAny<Type>()))
                .Returns(false);

            var options = new DeepComparisonOptions()
            {
                DeepEqualityComparers = new List<IDeepEqualityComparer>()
                {
                    mockComparer.Object
                }
            };

            // Act/Assert
            Assert.Throws<InvalidOperationException>(() => service.AreDeepEqual(objA, objB, options));
        }

        [Fact]
        public void AreDeepEqual_SameObjectTypes_DifferentObjects_DeepEqualityComparerAvailable_ReturnsTrue()
        {
            // Arrange
            var service = CreateService();
            var objA = new MockStruct();
            var objB = new MockStruct();

            var mockComparer = new Mock<IDeepEqualityComparer>();
            mockComparer.Setup(m => m.CanCompare(It.IsAny<Type>()))
                .Returns(true);
            mockComparer.Setup(m =>
                    m.AreDeepEqual(It.IsAny<object>(), It.IsAny<object>(), It.IsAny<DeepComparisonOptions>()))
                .Returns(true);

            var options = new DeepComparisonOptions()
            {
                DeepEqualityComparers = new List<IDeepEqualityComparer>()
                {
                    mockComparer.Object
                }
            };

            // Act
            var result = service.AreDeepEqual(objA, objB, options);

            // Assert
            Assert.True(result);
        }

        #endregion

        #region Helpers

        private IDeepComparisonService CreateService()
        {
            return new DeepComparisonService();
        }

        #endregion
    }
}
