using System;

namespace Common.Extensions.Object.DeepEquals.Abstracts
{
    /// <summary>
    /// A base class that performs basic type casting for child classes.
    /// </summary>
    /// <typeparam name="TComparerType"></typeparam>
    public abstract class TypedDeepEqualityComparer<TComparerType>: DeepEqualityComparer
    {
        #region DeepEqualityComparer

        protected override bool IsComparerType(Type type)
        {
            return type == typeof(TComparerType);
        }

        protected override bool AreDeepEqual(object a, object b)
        {
            return AreDeepEqual((TComparerType) a, (TComparerType) b);
        }

        #endregion

        #region Abstracts

        protected abstract bool AreDeepEqual(TComparerType a, TComparerType b);

        #endregion
    }
}
