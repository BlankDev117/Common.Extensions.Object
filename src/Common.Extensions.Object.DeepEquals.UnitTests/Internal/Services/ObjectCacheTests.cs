using System;
using Common.Extensions.Object.DeepEquals.Internal.Services;
using Common.Extensions.Object.DeepEquals.Ports;
using Xunit;

namespace Common.Extensions.Object.DeepEquals.UnitTests.Internal.Services
{
    public class ObjectCacheTests
    {
        #region Add

        [Fact]
        public void Add_NullKey_ThrowsArgumentNullException()
        {
            // Arrange
            var cache = CreateCache();

            // Act/Assert
            Assert.Throws<ArgumentNullException>(() => cache.Add(null, new object()));
        }

        [Fact]
        public void Add_ValidKey_KeyNotInDictionary_ReturnsSuccessfully()
        {
            // Arrange
            var cache = CreateCache();

            // Act
            cache.Add(new object(), new object());

            // Assert
            Assert.True(true);
        }

        [Fact]
        public void Add_ValidKey_KeyInDictionary_OverwritesOriginal_ReturnsSuccessfully()
        {
            // Arrange
            var cache = CreateCache();

            var key = new object();
            cache.Add(key, new object());

            // Act
            cache.Add(key, new object());

            // Assert
            Assert.True(true);
        }

        #endregion

        #region TryGet

        [Fact]
        public void TryGet_NullKey_ThrowsArgumentNullException()
        {
            // Arrange
            var cache = CreateCache();

            // Act/Assert
            Assert.Throws<ArgumentNullException>(() => cache.TryGet(null, out _));
        }

        [Fact]
        public void TryGet_ValidKey_KeyNotInDictionary_ReturnsFalseAndNullValue()
        {
            // Arrange
            var cache = CreateCache();
            var key = new object();

            // Act
            var result = cache.TryGet(key, out var cachedValue);

            // Assert
            Assert.False(result);
            Assert.Null(cachedValue);
        }

        [Fact]
        public void TryGet_ValidKey_KeyInDictionary_ReturnsTrueAndOriginalValue()
        {
            // Arrange
            var cache = CreateCache();
            var key = new object();
            var value = new object();

            cache.Add(key, value);

            // Act
            var result = cache.TryGet(key, out var cachedValue);

            // Assert
            Assert.True(result);
            Assert.NotNull(cachedValue);
            Assert.Equal(value, cachedValue);
        }

        #endregion

        #region Helpers

        private IObjectCache CreateCache()
        {
            return new ObjectCache();
        }

        #endregion
    }
}
