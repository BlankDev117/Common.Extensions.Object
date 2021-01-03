using Common.Extensions.Object.DeepEquals.Models;
using Common.Extensions.Object.DeepEquals.Options;

namespace Common.Extensions.Object.DeepEquals.Ports
{
    // Primary Port: Implemented Internally
    internal interface IComparisonScopeProvider
    {
        ScopedComparison CreateScope(object objA, object objB, DeepComparisonOptions options);
    }
}
