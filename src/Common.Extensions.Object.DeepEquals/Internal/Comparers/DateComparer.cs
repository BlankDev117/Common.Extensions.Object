using System;
using Common.Extensions.Object.DeepEquals.Abstracts;

namespace Common.Extensions.Object.DeepEquals.Internal.Comparers
{
    internal class DateComparer: DeepEqualityComparer
    {
        #region DeepEqualityComparer

        protected override bool IsComparerType(Type type)
        {
            return type == typeof(DateTime);
        }

        protected override bool AreDeepEqual(object a, object b)
        {
            return a.Equals(b);
        }

        #endregion
    }
}
