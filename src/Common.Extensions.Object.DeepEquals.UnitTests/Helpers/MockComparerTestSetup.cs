using System;
using Common.Extensions.Object.DeepEquals.Models;
using Common.Extensions.Object.DeepEquals.Options;
using Common.Extensions.Object.DeepEquals.Ports;
using Moq;

namespace Common.Extensions.Object.DeepEquals.UnitTests.Helpers
{
    public static class MockComparerTestSetup
    {
        public static IDeepEqualityComparer SetupComparer(IDeepEqualityComparer comparer, Func<object, object, DeepComparisonOptions, bool> comparisonFunction)
        {
            return SetupComparer(comparer, comparisonFunction, out _, out _);
        }

        public static IDeepEqualityComparer SetupComparer(IDeepEqualityComparer comparer, Func<object, object, DeepComparisonOptions, bool> comparisonFunction, out Mock<IObjectCache> mockObjectCache, out Mock<ICircularReferenceMonitor> mockCircularRefMonitor)
        {
            var mockComparisonService = new Mock<IDeepComparisonService>();
            mockComparisonService.Setup(
                    m => m.AreDeepEqual(It.IsAny<object>(), It.IsAny<object>(), It.IsAny<DeepComparisonOptions>()))
                .Returns(comparisonFunction);
            var mockPropertyCache = new Mock<IPropertyCache>();
            mockPropertyCache.Setup(m => m.GetPropertyInfos(It.IsAny<Type>(), It.IsAny<PropertyComparison>()))
                .Returns((Type type, PropertyComparison _) => type.GetProperties());

            mockObjectCache = new Mock<IObjectCache>();
            mockCircularRefMonitor = new Mock<ICircularReferenceMonitor>();

            comparer.SetConfiguration(new ComparerConfiguration()
            {
                DeepComparisonService = mockComparisonService.Object,
                PropertyCache = mockPropertyCache.Object,
                CircularReferenceMonitor = mockCircularRefMonitor.Object,
                ObjectCache = mockObjectCache.Object
            });

            return comparer;
        }

    }
}
