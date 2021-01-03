using System.Collections.Generic;

namespace Common.Extensions.Object.DeepEquals.UnitTests.Helpers
{
    public struct MockStruct
    {
        public int TestInt { get; set; }

        public string TestString { get; set; }

        public List<long> TestLongs { get; set; }

        public TestClass TestClass { get; set; }
    }

}
