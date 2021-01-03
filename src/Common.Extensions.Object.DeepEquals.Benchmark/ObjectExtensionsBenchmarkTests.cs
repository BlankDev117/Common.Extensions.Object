using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Common.Extensions.Object.DeepEquals.Benchmark.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace Common.Extensions.Object.DeepEquals.Benchmark
{
    public class ObjectExtensionsBenchmarkTests
    {
        #region Variables

        private readonly ITestOutputHelper _outputHelper;

        #endregion

        #region Constructors

        public ObjectExtensionsBenchmarkTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        #endregion

        #region AreDeepEqual

        [Fact]
        public void AreDeepEqual_BenchmarkTesting()
        {
            // Arrange
            var stopWatch = new Stopwatch();
            var testCount = 1000;
            var iterationsPerTest = 1000;
            var testFailureEveryXTimes = 2;

            var now = DateTime.Now;
            var utcNow = DateTime.UtcNow;

            var original = new BenchmarkTestClass()
            {
                Int = 117,
                String = "Test",
                DateTime = now,
                NullableDouble = null,
                BenchmarkEnum = BenchmarkEnum.Great,
                Longs = new List<long>()
                {
                    9,
                    7,
                    99,
                    0
                },
                Strings = new List<string>()
                {
                    "A",
                    "Wonderful",
                    "Life",
                    "For",
                    "Me"
                },
                StringIntLookup = new Dictionary<string, int>()
                {
                    { "A", 1 },
                    { "C", 7 },
                    { "F", 38 },
                    { "I", 88 }
                },
                NestedClass = new BenchmarkTestClass()
                {
                    Int = 117,
                    String = "Test",
                    DateTime = utcNow,
                    NullableDouble = 6,
                    BenchmarkEnum = BenchmarkEnum.Amazing,
                    Longs = new List<long>()
                    {
                        9,
                        7,
                        99,
                        0
                    },
                    Strings = new List<string>()
                    {
                        "A",
                        "Wonderful",
                        "Life",
                        "For",
                        "Me"
                    },
                    StringIntLookup = new Dictionary<string, int>()
                    {
                        { "A", 1 },
                        { "C", 7 },
                        { "F", 38 },
                        { "I", 88 }
                    }
                }
            };
            original.NestedClass.NestedClass = original;

            var testSuccess = new BenchmarkTestClass()
            {
                Int = 117,
                String = "Test",
                DateTime = now,
                NullableDouble = null,
                BenchmarkEnum = BenchmarkEnum.Great,
                Longs = new List<long>()
                {
                    9,
                    7,
                    99,
                    0
                },
                Strings = new List<string>()
                {
                    "A",
                    "Wonderful",
                    "Life",
                    "For",
                    "Me"
                },
                StringIntLookup = new Dictionary<string, int>()
                {
                    { "A", 1 },
                    { "C", 7 },
                    { "F", 38 },
                    { "I", 88 }
                },
                NestedClass = new BenchmarkTestClass()
                {
                    Int = 117,
                    String = "Test",
                    DateTime = utcNow,
                    NullableDouble = 6,
                    BenchmarkEnum = BenchmarkEnum.Amazing,
                    Longs = new List<long>()
                    {
                        9,
                        7,
                        99,
                        0
                    },
                    Strings = new List<string>()
                    {
                        "A",
                        "Wonderful",
                        "Life",
                        "For",
                        "Me"
                    },
                    StringIntLookup = new Dictionary<string, int>()
                    {
                        { "A", 1 },
                        { "C", 7 },
                        { "F", 38 },
                        { "I", 88 }
                    }
                }
            };
            testSuccess.NestedClass.NestedClass = testSuccess;

            var testFailure = new BenchmarkTestClass()
            {
                Int = 117,
                String = "Test",
                DateTime = now,
                NullableDouble = null,
                BenchmarkEnum = BenchmarkEnum.Great,
                Longs = new List<long>()
                {
                    9,
                    7,
                    99,
                    0
                },
                Strings = new List<string>()
                {
                    "A",
                    "Wonderful",
                    "Life",
                    "For",
                    "Me"
                },
                StringIntLookup = new Dictionary<string, int>()
                {
                    { "A", 1 },
                    { "C", 7 },
                    { "F", 38 },
                    { "I", 88 }
                },
                NestedClass = new BenchmarkTestClass()
                {
                    Int = 117,
                    String = "Test",
                    DateTime = utcNow,
                    NullableDouble = 6,
                    BenchmarkEnum = BenchmarkEnum.Amazing,
                    Longs = new List<long>()
                    {
                        9,
                        7,
                        99,
                        0
                    },
                    Strings = new List<string>()
                    {
                        "A",
                        "Wonderful",
                        "Life",
                        "For",
                        "Me"
                    },
                    StringIntLookup = new Dictionary<string, int>()
                    {
                        { "A", 1 },
                        { "C", 7 },
                        { "F", 38 },
                        { "BadKey", 88 }
                    }
                }
            };

            var testTimes = new List<long>();
            var passes = 0;
            var failures = 0;

            // Act
            for (var currentTest = 0; currentTest < testCount; currentTest++)
            {
                stopWatch.Restart();

                for (var currentIteration = 0; currentIteration < iterationsPerTest; currentIteration++)
                {
                    // Assert
                    if (currentIteration % testFailureEveryXTimes == 0)
                    {
                        Assert.False(original.DeepEquals(testFailure));
                        failures++;
                        continue;
                    }

                    Assert.True(original.DeepEquals(testSuccess));
                    passes++;
                }

                stopWatch.Stop();
                testTimes.Add(stopWatch.ElapsedMilliseconds);
            }

            var fastestTestTime = testTimes.Min();
            var slowestTestTime = testTimes.Max();
            var averageMilliseconds = testTimes.Average();
            var totalTime = testTimes.Sum();

            _outputHelper.WriteLine($"{testCount} tests with each test running {iterationsPerTest} iterations.");
            _outputHelper.WriteLine($"{testCount * iterationsPerTest} total tests ran.");
            _outputHelper.WriteLine($"{passes} were expected to be equal, {failures} were expected to be unequal.");
            _outputHelper.WriteLine($"Total runtime: {totalTime}ms. Average runtime, for each test, was {averageMilliseconds}ms.");
            _outputHelper.WriteLine($"The fastest run time was: {fastestTestTime}ms. The slowest run time: {slowestTestTime}ms.");
        }

        #endregion
    }
}
