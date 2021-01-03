using System;
using Common.Extensions.Object.DeepEquals.Models;
using Common.Extensions.Object.DeepEquals.Options;
using Common.Extensions.Object.DeepEquals.Ports;
using Moq;
using Xunit;

namespace Common.Extensions.Object.DeepEquals.UnitTests.Models
{
    public class ScopedComparisonTests
    {
        #region Compare

        [Fact]
        public void Compare_NullDeepComparisonService_ThrowsArgumentNullException()
        {
            // Arrange
            var scopedComparison = new ScopedComparison()
            {
                DeepComparisonService = null
            };

            // Act/Assert
            Assert.Throws<ArgumentNullException>(() => scopedComparison.Compare());
        }

        [Fact]
        public void Compare_ValidDeepComparisonService_ReturnsResult()
        {
            // Arrange
            var mockComparisonService = new Mock<IDeepComparisonService>();
            mockComparisonService.Setup(m =>
                    m.AreDeepEqual(It.IsAny<object>(), It.IsAny<object>(), It.IsAny<DeepComparisonOptions>()))
                .Returns(true);

            var scopedComparison = new ScopedComparison()
            {
                DeepComparisonService = mockComparisonService.Object
            };

            // Act
            var result = scopedComparison.Compare();

            // Assert
            Assert.True(result);
        }

        #endregion
    }
}
