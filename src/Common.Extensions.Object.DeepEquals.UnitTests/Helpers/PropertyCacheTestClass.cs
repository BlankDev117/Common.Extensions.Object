using System.Collections.Generic;

namespace Common.Extensions.Object.DeepEquals.UnitTests.Helpers
{
    public class PropertyCacheTestClass
    {
        public int A { get; set; }

        public int B { get; set; }

        public string C { get; set; }

        public static long D { get; set; }

        public static TestClass E { get; set; }

        protected Dictionary<string, string> F { get; set; }

        internal List<long> G { get; set; }
    }
}
