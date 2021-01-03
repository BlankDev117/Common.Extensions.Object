using System;
using Common.Extensions.Object.DeepEquals.Models;
using Common.Extensions.Object.DeepEquals.Options;
using Common.Extensions.Object.DeepEquals.Ports;
using Common.Extensions.Object.DeepEquals.UnitTests.Helpers;
using Moq;
using Xunit;

namespace Common.Extensions.Object.DeepEquals.UnitTests.Abstracts
{
    public class DeepEqualityComparerTests
    {
        #region SetConfiguration

        [Fact]
        public void SetConfiguration_NullConfiguration_ThrowsArgumentNullException()
        {
            // Arrange
            var comparer = CreateComparer();

            // Act/Assert
            Assert.Throws<ArgumentNullException>(() => comparer.SetConfiguration(null));
        }

        [Fact]
        public void SetConfiguration_NullIDeepComparisonService_ThrowsInvalidOperationExceptionException()
        {
            // Arrange
            var comparer = CreateComparer();
            var configuration = new ComparerConfiguration()
            {
                DeepComparisonService = null,
                PropertyCache = new Mock<IPropertyCache>().Object,
                CircularReferenceMonitor = new Mock<ICircularReferenceMonitor>().Object,
                ObjectCache = new Mock<IObjectCache>().Object
            };

            // Act/Assert
            Assert.Throws<InvalidOperationException>(() => comparer.SetConfiguration(configuration));
        }

        [Fact]
        public void SetConfiguration_NullIPropertyCache_ThrowsInvalidOperationExceptionException()
        {
            // Arrange
            var comparer = CreateComparer();
            var configuration = new ComparerConfiguration()
            {
                DeepComparisonService = new Mock<IDeepComparisonService>().Object,
                PropertyCache = null,
                CircularReferenceMonitor = new Mock<ICircularReferenceMonitor>().Object,
                ObjectCache = new Mock<IObjectCache>().Object
            };

            // Act/Assert
            Assert.Throws<InvalidOperationException>(() => comparer.SetConfiguration(configuration));
        }

        [Fact]
        public void SetConfiguration_NullICircularReferenceMonitor_ThrowsInvalidOperationExceptionException()
        {
            // Arrange
            var comparer = CreateComparer();
            var configuration = new ComparerConfiguration()
            {
                DeepComparisonService = new Mock<IDeepComparisonService>().Object,
                PropertyCache = new Mock<IPropertyCache>().Object,
                CircularReferenceMonitor = null,
                ObjectCache = new Mock<IObjectCache>().Object
            };

            // Act/Assert
            Assert.Throws<InvalidOperationException>(() => comparer.SetConfiguration(configuration));
        }

        [Fact]
        public void SetConfiguration_NullIObjectCache_ThrowsInvalidOperationExceptionException()
        {
            // Arrange
            var comparer = CreateComparer();
            var configuration = new ComparerConfiguration()
            {
                DeepComparisonService = new Mock<IDeepComparisonService>().Object,
                PropertyCache = new Mock<IPropertyCache>().Object,
                CircularReferenceMonitor = new Mock<ICircularReferenceMonitor>().Object,
                ObjectCache = null
            };

            // Act/Assert
            Assert.Throws<InvalidOperationException>(() => comparer.SetConfiguration(configuration));
        }

        [Fact]
        public void SetConfiguration_ValidConfiguration_ReturnsSuccessfully()
        {
            // Arrange
            var comparer = CreateComparer();
            var configuration = new ComparerConfiguration()
            {
                DeepComparisonService = new Mock<IDeepComparisonService>().Object,
                PropertyCache = new Mock<IPropertyCache>().Object,
                CircularReferenceMonitor = new Mock<ICircularReferenceMonitor>().Object,
                ObjectCache = new Mock<IObjectCache>().Object
            };

            // Act
            comparer.SetConfiguration(configuration);

            // Assert
            Assert.True(true);
        }

        #endregion

        #region CanCompare

        [Fact]
        public void CanCompare_NullType_ThrowsArgumentNullException()
        {
            // Arrange
            var comparer = CreateComparer();

            // Act/Assert
            Assert.Throws<ArgumentNullException>(() => comparer.CanCompare(null));
        }

        #endregion

        #region AreDeepEqual

        [Fact]
        public void DeepEqual_NullObjectA_ThrowsArgumentNullException()
        {
            // Arrange
            var comparer = CreateComparer();

            // Act/Assert
            Assert.Throws<ArgumentNullException>(() =>
                comparer.AreDeepEqual(null, new object(), new DeepComparisonOptions()));
        }

        [Fact]
        public void DeepEqual_NullObjectB_ThrowsArgumentNullException()
        {
            // Arrange
            var comparer = CreateComparer();

            // Act/Assert
            Assert.Throws<ArgumentNullException>(() =>
                comparer.AreDeepEqual(new object(), null, new DeepComparisonOptions()));
        }

        [Fact]
        public void DeepEqual_NullComparisonOptions_ThrowsArgumentNullException()
        {
            // Arrange
            var comparer = CreateComparer();

            // Act/Assert
            Assert.Throws<ArgumentNullException>(() =>
                comparer.AreDeepEqual(new object(), new object(), null));
        }

        [Fact]
        public void DeepEqual_ComparerCannotCompareType_ThrowsInvalidOperationException()
        {
            // Arrange
            var comparer = CreateComparer(false);

            // Act/Assert
            Assert.Throws<InvalidOperationException>(() =>
                comparer.AreDeepEqual(new object(), 1, new DeepComparisonOptions()));
        }

        [Fact]
        public void DeepEqual_ObjectsDifferentTypes_ThrowsInvalidOperationException()
        {
            // Arrange
            var comparer = CreateComparer();

            // Act/Assert
            Assert.Throws<InvalidOperationException>(() =>
                comparer.AreDeepEqual(new object(), 1, new DeepComparisonOptions()));
        }

        [Fact]
        public void DeepEqual_ObjectsSameTypes_ChecksForEquality()
        {
            // Arrange
            var comparer = CreateComparer();

            // Act
            comparer.AreDeepEqual(new object(), new object(), new DeepComparisonOptions());

            // Assert
            Assert.True(true);
        }

        #endregion

        #region Helpers

        private IDeepEqualityComparer CreateComparer(bool shouldAllowCompare = true)
        {
            return new MockComparer()
            {
                CanCompareFunc = _ => shouldAllowCompare
            };
        }

        #endregion
    }
}
