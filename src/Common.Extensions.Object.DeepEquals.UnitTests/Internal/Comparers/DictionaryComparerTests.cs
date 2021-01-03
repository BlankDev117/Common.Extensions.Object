using System;
using System.Collections.Generic;
using Common.Extensions.Object.DeepEquals.Internal.Comparers;
using Common.Extensions.Object.DeepEquals.Options;
using Common.Extensions.Object.DeepEquals.Ports;
using Common.Extensions.Object.DeepEquals.UnitTests.Helpers;
using Xunit;

namespace Common.Extensions.Object.DeepEquals.UnitTests.Internal.Comparers
{
    public class DictionaryComparerTests
    {
        #region IsComparerType

        [Theory]
        [InlineData(typeof(Dictionary<string, string>), true)]
        [InlineData(typeof(Dictionary<IEnumerable<long>, Dictionary<long, string>>), true)]
        [InlineData(typeof(List<>), false)]
        [InlineData(typeof(int), false)]
        public void IsComparerType_TypeVariations_ReturnsExpectedResult(Type type, bool expectedResult)
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

        [Fact]
        public void AreDeepEqual_DictionariesHaveDifferentKeyCount_ReturnsFalse()
        {
            // Arrange
            var comparer = CreateComparer();

            var dictionaryA = new Dictionary<string, string>()
            {
                { "Key1", "Value1" }
            };
            var dictionaryB = new Dictionary<string, string>()
            {
                { "Key1", "Value1" },
                { "Key2", "Value2" }
            };

            MockComparerTestSetup.SetupComparer(comparer, (objA, objB, _) =>
            {
                var s1 = (string)objA;
                var s2 = (string)objB;

                return s1.Equals(s2, StringComparison.OrdinalIgnoreCase);
            });

            // Act
            var result = comparer.AreDeepEqual(dictionaryA, dictionaryB, new DeepComparisonOptions());

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AreDeepEqual_DictionariesHaveDifferentKeys_ReturnsFalse()
        {
            // Arrange
            var comparer = CreateComparer();

            var dictionaryA = new Dictionary<string, string>()
            {
                { "Key1", null },
                { "Key2", "Value2" }
            };
            var dictionaryB = new Dictionary<string, string>()
            {
                { "KeyA", "Value1" },
                { "KeyB", "Value2" }
            };

            MockComparerTestSetup.SetupComparer(comparer, (objA, objB, _) =>
            {
                var s1 = (string)objA;
                var s2 = (string)objB;

                return s1.Equals(s2, StringComparison.OrdinalIgnoreCase);
            });

            // Act
            var result = comparer.AreDeepEqual(dictionaryA, dictionaryB, new DeepComparisonOptions());

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AreDeepEqual_DictionariesHaveDifferentValues_ReturnsFalse()
        {
            // Arrange
            var comparer = CreateComparer();

            var dictionaryA = new Dictionary<string, string>()
            {
                { "Key1", "Value1" },
                { "Key2", "Value2" }
            };
            var dictionaryB = new Dictionary<string, string>()
            {
                { "Key1", "ValueA" },
                { "Key2", "ValueB" }
            };

            MockComparerTestSetup.SetupComparer(comparer,(objA, objB, _) =>
            {
                var s1 = (string)objA;
                var s2 = (string)objB;

                return s1.Equals(s2, StringComparison.OrdinalIgnoreCase);
            });

            // Act
            var result = comparer.AreDeepEqual(dictionaryA, dictionaryB, new DeepComparisonOptions());

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AreDeepEqual_DictionariesHaveSameKeysAndValues_ReturnsTrue()
        {
            // Arrange
            var comparer = CreateComparer();

            var dictionaryA = new Dictionary<string, string>()
            {
                { "Key1", "Value1" },
                { "Key2", "Value2" }
            };
            var dictionaryB = new Dictionary<string, string>()
            {
                { "Key1", "Value1" },
                { "Key2", "Value2" }
            };

            MockComparerTestSetup.SetupComparer(comparer, (objA, objB, _) =>
            {
                var s1 = (string)objA;
                var s2 = (string)objB;

                return s1.Equals(s2, StringComparison.OrdinalIgnoreCase);
            });

            // Act
            var result = comparer.AreDeepEqual(dictionaryA, dictionaryB, new DeepComparisonOptions());

            // Assert
            Assert.True(result);
        }

        #endregion

        #region Helpers

        private IDeepEqualityComparer CreateComparer()
        {
            return new DictionaryComparer();
        }

        #endregion
    }
}
