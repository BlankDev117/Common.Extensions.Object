using System;
using System.Collections.Generic;

namespace Common.Extensions.Object.DeepEquals.Benchmark.Helpers
{
    public class BenchmarkTestClass
    {
        public int Int { get; set; }

        public string String { get; set; }

        public DateTime DateTime { get; set; }

        public BenchmarkEnum BenchmarkEnum { get; set; }

        public double? NullableDouble { get; set; }

        public List<long> Longs { get; set; }

        public List<string> Strings { get; set; }

        public Dictionary<string, int> StringIntLookup { get; set; }

        public BenchmarkTestClass NestedClass { get; set; }
    }
}
