using System;
using System.Collections;
using System.Collections.Generic;
using Common.Extensions.Object.DeepEquals.Internal.Comparers;
using Common.Extensions.Object.DeepEquals.Options;
using Common.Extensions.Object.DeepEquals.Ports;
using Common.Extensions.Object.DeepEquals.UnitTests.Helpers;
using Xunit;

namespace Common.Extensions.Object.DeepEquals.UnitTests.Internal.Comparers
{
    public class EnumerableComparerTests
    {
        #region IsComparerType

        [Theory]
        [InlineData(typeof(List<>), true)]
        [InlineData(typeof(ArrayList), true)]
        [InlineData(typeof(object[]), true)]
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
        public void AreDeepEqual_List_OrderEnforced_Ordered_SameValues_ReturnsTrue()
        {
            // Arrange
            var testSet1 = new List<string>()
            {
                "Test",
                "Hello",
                "Amazing",
                "World"
            };
            var testSet2 = new List<string>()
            {
                "Test",
                "Hello",
                "Amazing",
                "World"
            };
            var comparer = CreateComparer();

            MockComparerTestSetup.SetupComparer(comparer,(objA, objB, _) =>
            {
                var s1 = (string) objA;
                var s2 = (string) objB;

                return s1.Equals(s2, StringComparison.OrdinalIgnoreCase);
            });

            var options = new DeepComparisonOptions()
            {
                EnforceEnumerableOrdering = true
            };

            // Act
            var result = comparer.AreDeepEqual(testSet1, testSet2, options);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AreDeepEqual_List_OrderEnforced_Unordered_SameValues_ReturnsFalse()
        {
            // Arrange
            var testSet1 = new List<string>()
            {
                "Test",
                "Hello",
                "Amazing",
                "World"
            };
            var testSet2 = new List<string>()
            {
                "Test",
                "Hello",
                "World",
                "Amazing"
            };
            var comparer = CreateComparer();

            MockComparerTestSetup.SetupComparer(comparer, (objA, objB, _) =>
            {
                var s1 = (string)objA;
                var s2 = (string)objB;

                return s1.Equals(s2, StringComparison.OrdinalIgnoreCase);
            });

            var options = new DeepComparisonOptions()
            {
                EnforceEnumerableOrdering = true
            };

            // Act
            var result = comparer.AreDeepEqual(testSet1, testSet2, options);

            // Assert
            Assert.False(result);
        }
        
        [Fact]
        public void AreDeepEqual_Array_OrderEnforced_Ordered_SameValues_ReturnsTrue()
        {
            // Arrange
            var testSet1 = new[]
            {
                1,
                12,
                3,
                7
            };
            var testSet2 = new[]
            {
                1,
                12,
                3,
                7
            };
            var comparer = CreateComparer();

            MockComparerTestSetup.SetupComparer(comparer,(objA, objB, _) =>
            {
                var int1 = (int)objA;
                var int2 = (int)objB;

                return int1 == int2;
            });

            var options = new DeepComparisonOptions()
            {
                EnforceEnumerableOrdering = true
            };

            // Act
            var result = comparer.AreDeepEqual(testSet1, testSet2, options);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AreDeepEqual_Array_OrderEnforced_Unordered_DifferentValues_ReturnsFalse()
        {
            // Arrange
            var testSet1 = new[]
            {
                1,
                12,
                3,
                7
            };
            var testSet2 = new[]
            {
                1,
                12,
                7,
                3
            }; var comparer = CreateComparer();

            MockComparerTestSetup.SetupComparer(comparer, (objA, objB, _) =>
            {
                var int1 = (int)objA;
                var int2 = (int)objB;

                return int1 == int2;
            });

            var options = new DeepComparisonOptions()
            {
                EnforceEnumerableOrdering = true
            };

            // Act
            var result = comparer.AreDeepEqual(testSet1, testSet2, options);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AreDeepEqual_List_OrderNotEnforced_Ordered_SameValues_ReturnsTrue()
        {
            // Arrange
            var testSet1 = new List<string>()
            {
                "Test",
                "Hello",
                "Amazing",
                "World"
            };
            var testSet2 = new List<string>()
            {
                "Test",
                "Hello",
                "Amazing",
                "World"
            };
            var comparer = CreateComparer();

            MockComparerTestSetup.SetupComparer(comparer, (objA, objB, _) =>
            {
                var s1 = (string)objA;
                var s2 = (string)objB;

                return s1.Equals(s2, StringComparison.OrdinalIgnoreCase);
            });

            var options = new DeepComparisonOptions()
            {
                EnforceEnumerableOrdering = false
            };

            // Act
            var result = comparer.AreDeepEqual(testSet1, testSet2, options);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AreDeepEqual_List_OrderNotEnforced_Unordered_SameValues_ReturnsTrue()
        {
            // Arrange
            var testSet1 = new List<string>()
            {
                "Test",
                "Hello",
                "Amazing",
                "World"
            };
            var testSet2 = new List<string>()
            {
                "Hello",
                "Test",
                "World",
                "Amazing"
            };
            var comparer = CreateComparer();

            MockComparerTestSetup.SetupComparer(comparer, (objA, objB, _) =>
            {
                var s1 = (string)objA;
                var s2 = (string)objB;

                return s1.Equals(s2, StringComparison.OrdinalIgnoreCase);
            });

            var options = new DeepComparisonOptions()
            {
                EnforceEnumerableOrdering = false
            };

            // Act
            var result = comparer.AreDeepEqual(testSet1, testSet2, options);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AreDeepEqual_List_OrderNotEnforced_Unordered_DifferentValues_ReturnsFalse()
        {
            // Arrange
            var testSet1 = new List<string>()
            {
                "Test",
                "Hello",
                "Amazing",
                "World"
            };
            var testSet2 = new List<string>()
            {
                "A",
                "New",
                "Trick",
                "Awaits"
            };
            var comparer = CreateComparer();

            MockComparerTestSetup.SetupComparer(comparer, (objA, objB, _) =>
            {
                var s1 = (string)objA;
                var s2 = (string)objB;

                return s1.Equals(s2, StringComparison.OrdinalIgnoreCase);
            });

            var options = new DeepComparisonOptions()
            {
                EnforceEnumerableOrdering = false
            };

            // Act
            var result = comparer.AreDeepEqual(testSet1, testSet2, options);

            // Assert
            Assert.False(result);
        }


        [Fact]
        public void AreDeepEqual_Array_OrderNotEnforced_Ordered_SameValues_ReturnsTrue()
        {
            // Arrange
            var testSet1 = new[]
            {
                1,
                12,
                3,
                7
            };
            var testSet2 = new[]
            {
                1,
                12,
                3,
                7
            };
            var comparer = CreateComparer();

            MockComparerTestSetup.SetupComparer(comparer, (objA, objB, _) =>
            {
                var int1 = (int)objA;
                var int2 = (int)objB;

                return int1 == int2;
            });

            var options = new DeepComparisonOptions()
            {
                EnforceEnumerableOrdering = false
            };

            // Act
            var result = comparer.AreDeepEqual(testSet1, testSet2, options);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AreDeepEqual_Array_OrderNotEnforced_Unordered_DifferentValues_ReturnsTrue()
        {
            // Arrange
            var testSet1 = new[]
            {
                1,
                3,
                7,
                12
            };
            var testSet2 = new[]
            {
                1,
                12,
                7,
                3
            }; var comparer = CreateComparer();

            MockComparerTestSetup.SetupComparer(comparer, (objA, objB, _) =>
            {
                var int1 = (int)objA;
                var int2 = (int)objB;

                return int1 == int2;
            });

            var options = new DeepComparisonOptions()
            {
                EnforceEnumerableOrdering = false
            };

            // Act
            var result = comparer.AreDeepEqual(testSet1, testSet2, options);

            // Assert
            Assert.True(result);
        }


        #endregion

        #region Helpers

        private IDeepEqualityComparer CreateComparer()
        {
            return new EnumerableComparer();
        }

        #endregion
    }
}
