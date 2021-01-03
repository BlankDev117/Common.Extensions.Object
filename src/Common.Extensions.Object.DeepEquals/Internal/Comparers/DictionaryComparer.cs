using System;
using System.Collections;
using Common.Extensions.Object.DeepEquals.Abstracts;

namespace Common.Extensions.Object.DeepEquals.Internal.Comparers
{
    internal class DictionaryComparer: TypedDeepEqualityComparer<IDictionary>
    {
        #region TypedDeepEqualityComparer

        protected override bool IsComparerType(Type type)
        {
            return typeof(IDictionary).IsAssignableFrom(type);
        }

        protected override bool AreDeepEqual(IDictionary a, IDictionary b)
        {
            if (a.Keys.Count != b.Keys.Count)
            {
                return false;
            }

            foreach (var key in a.Keys)
            {
                if (b.Contains(key) && DeepComparisonService.AreDeepEqual(a[key], b[key], DeepComparisonOptions))
                {
                    continue;
                }

                return false;
            }

            return true;
        }

        #endregion

    }
}
