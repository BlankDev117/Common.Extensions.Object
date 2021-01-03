using System;
using Common.Extensions.Object.DeepEquals.Abstracts;

namespace Common.Extensions.Object.DeepEquals.Internal.Comparers
{
    internal class StringComparer: TypedDeepEqualityComparer<string>
    {
        #region TypedDeepEqualityComparer Overrides

        protected override bool AreDeepEqual(string a, string b)
        {
            var comparisonRule = DeepComparisonOptions.IgnoreCaseSensitivity
                ? StringComparison.InvariantCultureIgnoreCase
                : StringComparison.InvariantCulture;

            return a.Equals(b, comparisonRule);
        }

        #endregion
    }
}
