using System;
using System.Linq;
using Common.Extensions.Object.DeepEquals.Abstracts;

namespace Common.Extensions.Object.DeepEquals.Internal.Comparers
{
    internal class PropertyComparer: DeepEqualityComparer
    {
        #region DeepEqualityComparer

        protected override bool IsComparerType(Type type)
        {
            return type.IsClass || (type.IsValueType && !type.IsPrimitive && type.Namespace != null && !type.Namespace.StartsWith("System."));
        }

        protected override bool AreDeepEqual(object a, object b)
        {
            ObjectCache.Add(a, b);

            var properties = PropertyCache.GetPropertyInfos(a.GetType(), DeepComparisonOptions.PropertyComparison);

            return properties.All(property =>
            {
                var valueA = property.GetValue(a);
                var valueB = property.GetValue(b);

                var isACircular = CircularReferenceMonitor.AddReference(a, valueA);
                var isBCircular = CircularReferenceMonitor.AddReference(b, valueB);

                if (isACircular && isBCircular)
                {
                    return ObjectCache.TryGet(valueA, out var originalB) && ReferenceEquals(originalB, valueB);
                }
                if (isACircular || isBCircular)
                {
                    return false;
                }

                return DeepComparisonService.AreDeepEqual(valueA, valueB, DeepComparisonOptions);
            });
        }

        #endregion
    }
}
