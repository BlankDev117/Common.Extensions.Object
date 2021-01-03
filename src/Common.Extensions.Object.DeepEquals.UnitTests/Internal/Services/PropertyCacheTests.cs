using System;
using System.Linq;
using Common.Extensions.Object.DeepEquals.Internal.Services;
using Common.Extensions.Object.DeepEquals.Models;
using Common.Extensions.Object.DeepEquals.Ports;
using Common.Extensions.Object.DeepEquals.UnitTests.Helpers;
using Xunit;

namespace Common.Extensions.Object.DeepEquals.UnitTests.Internal.Services
{
    public class PropertyCacheTests
    {
        #region GetPropertyInfos

        [Fact]
        public void GetPropertyInfos_NullType_ThrowsArgumentNullException()
        {
            // Arrange
            var cache = CreateCache();
            
            // Act
            Assert.Throws<ArgumentNullException>(() => cache.GetPropertyInfos(null, PropertyComparison.Default));
        }

        [Fact]
        public void GetPropertyInfos_TypeNotInDictionary_CreatesNewEntryAndReturnsPropertyInfos()
        {
            // Arrange
            var cache = CreateCache();

            // Act
            var propertyInfos = cache.GetPropertyInfos(typeof(MockStruct), PropertyComparison.Default);

            // Assert
            Assert.Equal(4, propertyInfos.Count());
        }

        [Fact]
        public void GetPropertyInfos_TypeInDictionary_ReturnsCachedPropertyInfos()
        {
            // Arrange
            var cache = CreateCache();
            cache.GetPropertyInfos(typeof(MockStruct), PropertyComparison.Default);

            // Act
            var propertyInfos = cache.GetPropertyInfos(typeof(MockStruct), PropertyComparison.Default);

            // Assert
            Assert.Equal(4, propertyInfos.Count());
        }

        [Theory]
        [InlineData(PropertyComparison.Default, 3)]
        [InlineData(PropertyComparison.IncludeStatic, 5)]
        [InlineData(PropertyComparison.IncludeNonPublic, 5)]
        [InlineData(PropertyComparison.IncludeStatic | PropertyComparison.IncludeNonPublic, 7)]
        public void GetPropertyInfos_PropertyComparisons_ReturnsSpecifiedPropertyInfos(PropertyComparison rule, int expectedPropertyCount)
        {
            // Arrange
            var cache = CreateCache();

            // Act
            var propertyInfos = cache.GetPropertyInfos(typeof(PropertyCacheTestClass), rule);

            // Assert
            Assert.Equal(expectedPropertyCount, propertyInfos.Count());
        }

        #endregion

        #region Helpers

        private IPropertyCache CreateCache()
        {
            return new PropertyCache();
        }

        #endregion
    }
}
