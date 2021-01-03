using Common.Extensions.Object.DeepEquals.Abstracts;

namespace Common.Extensions.Object.DeepEquals.Internal.Comparers
{
    internal class BooleanComparer: TypedDeepEqualityComparer<bool>
    {
        #region TypedDeepEqualityComparer Overrides

        protected override bool AreDeepEqual(bool a, bool b)
        {
            return a == b;
        }

        #endregion
    }
}
