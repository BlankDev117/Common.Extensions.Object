using Common.Extensions.Object.DeepEquals.Options;

namespace Common.Extensions.Object.DeepEquals.Ports
{
    // Primary Port: Implemented Internally
    public interface IDeepComparisonService
    {
        bool AreDeepEqual(object objA, object objB, DeepComparisonOptions options);
    }
}
