using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common.Extensions.Object.DeepEquals.Abstracts;

namespace Common.Extensions.Object.DeepEquals.Internal.Comparers
{
    internal class EnumerableComparer: TypedDeepEqualityComparer<IEnumerable>
    {
        #region TypedDeepEqualityComparer
        
        protected override bool IsComparerType(Type type)
        {
            return typeof(IEnumerable).IsAssignableFrom(type);
        }

        protected override bool AreDeepEqual(IEnumerable a, IEnumerable b)
        {
            return DeepComparisonOptions.EnforceEnumerableOrdering ? CompareOrder(a, b) : CompareItems(a, b);
        }

        #endregion

        #region Helpers

        private bool CompareOrder(IEnumerable a, IEnumerable b)
        {
            var enumeratorA = a.GetEnumerator();
            var enumeratorB = b.GetEnumerator();

            var aHasValue = enumeratorA.MoveNext();
            var bHasValue = enumeratorB.MoveNext();
            var areEqual = (aHasValue && bHasValue) || (!aHasValue && !bHasValue);

            while (areEqual && aHasValue)
            {
                areEqual = DeepComparisonService.AreDeepEqual(enumeratorA.Current, enumeratorB.Current, DeepComparisonOptions);

                if (areEqual)
                {
                    aHasValue = enumeratorA.MoveNext();
                    bHasValue = enumeratorB.MoveNext();
                    areEqual = (aHasValue && bHasValue) || (!aHasValue && !bHasValue);
                    continue;
                }

                break;
            }

            return areEqual;
        }

        private bool CompareItems(IEnumerable a, IEnumerable b)
        {
            var enumeratorA = a.GetEnumerator();

            var tempList = new List<object>();

            while (enumeratorA.MoveNext())
            {
                tempList.Add(enumeratorA.Current);
            }

            var enumeratorB = b.GetEnumerator();
            var bSize = 0;

            while (enumeratorB.MoveNext())
            {
                bSize++;

                if (tempList.Any(item => DeepComparisonService.AreDeepEqual(item, enumeratorB.Current, DeepComparisonOptions)))
                {
                    continue;
                }

                return false;
            }

            return tempList.Count == bSize;
        }

        #endregion
    }
}
