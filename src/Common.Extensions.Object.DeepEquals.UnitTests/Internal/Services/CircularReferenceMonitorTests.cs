using System.Collections.Generic;
using Common.Extensions.Object.DeepEquals.Internal.Services;
using Common.Extensions.Object.DeepEquals.Ports;
using Common.Extensions.Object.DeepEquals.UnitTests.Helpers;
using Xunit;

namespace Common.Extensions.Object.DeepEquals.UnitTests.Internal.Services
{
    public class CircularReferenceMonitorTests
    {
        #region AddReference

        [Fact]
        public void AddReference_NullParent_ReturnsFalse()
        {
            // Arrange
            var monitor = CreateMonitor();

            // Act
            var result = monitor.AddReference(null, new object());

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AddReference_NullChild_ReturnsFalse()
        {
            // Arrange
            var monitor = CreateMonitor();

            // Act
            var result = monitor.AddReference(new object(), null);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("Hello", "Hello")]
        [InlineData(1, "Hello")]
        [InlineData(12, 12)]
        [InlineData(MockEnum.AwesomeTest, MockEnum.AwesomeTest)]
        public void AddReference_DifferingNonCircularReferenceTypes_ReturnsFalse(object a, object b)
        {
            // Arrange
            var monitor = CreateMonitor();

            // Act
            var result = monitor.AddReference(a, b);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AddReference_NonCircularReferences_ReturnsFalse()
        {
            // Arrange
            var monitor = CreateMonitor();

            var dictionary = new Dictionary<string, string>();
            var obj = new TestClass();

            // Act
            var result = monitor.AddReference(obj, dictionary);
            var result2 = monitor.AddReference(obj, dictionary);

            // Assert
            Assert.False(result);
            Assert.False(result2);
        }

        [Fact]
        public void AddReference_SameParentAndChild_CircularReference_ReturnsTrue()
        {
            // Arrange
            var monitor = CreateMonitor();
            var test = new TestClass();

            // Act
            var result = monitor.AddReference(test, test);

            // Assert
            Assert.True(result);
        }

        [Fact] public void AddReference_DifferentParentAndChild_ChildAlreadyAdded_CircularReference_ReturnsTrue()
        {
            // Arrange
            var monitor = CreateMonitor();
            var test = new TestClass();
            var other = new TestClass();
            var circular = new TestClass();

            // Act
            var isNotCircular = monitor.AddReference(test, other);
            var isNotCircular2 = monitor.AddReference(other, circular);
            var isCircular = monitor.AddReference(circular, test);

            // Assert
            Assert.False(isNotCircular);
            Assert.False(isNotCircular2);
            Assert.True(isCircular);
        }

        #endregion

        #region Helpers

        private ICircularReferenceMonitor CreateMonitor()
        {
            return new CircularReferenceMonitor();
        }

        #endregion
    }
}
