using Common.Extensions.Object.DeepEquals.Ports;

namespace Common.Extensions.Object.DeepEquals.Models
{
    public class ComparerConfiguration
    {
        internal ICircularReferenceMonitor CircularReferenceMonitor { get; set; }

        internal IObjectCache ObjectCache { get; set; }

        internal IPropertyCache PropertyCache { get; set; }

        internal IDeepComparisonService DeepComparisonService { get; set; }
    }
}
