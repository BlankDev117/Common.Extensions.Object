using System;
using System.Collections.Generic;
using System.Linq;
using Common.Extensions.Object.DeepEquals.Options;
using Common.Extensions.Object.DeepEquals.Ports;

namespace Common.Extensions.Object.DeepEquals.Internal.Services
{
    internal class DeepComparisonService: IDeepComparisonService
    {
        #region IDeepComparisonService

        public bool AreDeepEqual(object a, object b, DeepComparisonOptions comparisonOptions)
        {
            if (a == null && b == null)
            {
                return true;
            }
            if (a == null || b == null)
            {
                return false;
            }
            if (a.GetType() != b.GetType())
            {
                return false;
            }
            if (ReferenceEquals(a, b))
            {
                return true;
            }
            if (comparisonOptions == null)
            {
                throw new ArgumentNullException(nameof(comparisonOptions));
            }
            if (comparisonOptions.DeepEqualityComparers == null || !comparisonOptions.DeepEqualityComparers.Any())
            {
                throw new InvalidOperationException($"There were no comparers provided to use for comparing the objects.");
            }
            if (TryGetComparer(comparisonOptions.DeepEqualityComparers, a.GetType(), out var comparer))
            {
                return comparer.AreDeepEqual(a, b, comparisonOptions);
            }

            throw new InvalidOperationException($"There were no comparers able to compare objects of type {a.GetType().FullName}.");
        }

        #endregion

        #region Helpers

        private static bool TryGetComparer(IEnumerable<IDeepEqualityComparer> comparers, Type type,
            out IDeepEqualityComparer comparer)
        {
            comparer = comparers.FirstOrDefault(deepEqualityComparer => deepEqualityComparer.CanCompare(type));

            return comparer != null;
        }

        #endregion
    }
}
