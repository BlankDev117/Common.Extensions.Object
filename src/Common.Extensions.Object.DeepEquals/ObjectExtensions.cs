using System;
using Common.Extensions.Object.DeepEquals.Internal.Services;
using Common.Extensions.Object.DeepEquals.Options;
using Common.Extensions.Object.DeepEquals.Ports;

namespace Common.Extensions.Object
{
    public static class ObjectExtensions
    {
        #region Variables

        internal static IComparisonScopeProvider ComparisonScopeProvider { get; set; } = null;

        #endregion

        /// <summary>
        /// Performs a deep comparison between <see cref="objA"/> and <see cref="objB"/> using the default <see cref="DeepComparisonOptions"/>
        /// </summary>
        /// <param name="objA">The first object to compare</param>
        /// <param name="objB">The second object to compare against <see cref="objA"/></param>
        /// <returns>True/False for whether the objects are deeply equal to each other</returns>
        public static bool DeepEquals(this object objA, object objB)
        {
            var options = new DeepComparisonOptions();

            return objA.DeepEquals(objB, options);
        }

        /// <summary>
        /// Performs a deep comparison between <see cref="objA"/> and <see cref="objB"/>.
        /// </summary>
        /// <param name="objA">The first object to compare</param>
        /// <param name="objB">The second object to compare against <see cref="objA"/></param>
        /// <param name="comparisonOptions">The specific comparison rules to use when comparing certain types of objects</param>
        /// <returns>True/False for whether the objects are deeply equal to each other</returns>
        public static bool DeepEquals(this object objA, object objB, DeepComparisonOptions comparisonOptions)
        {
            if (comparisonOptions == null)
            {
                throw new ArgumentNullException(nameof(comparisonOptions));
            }

            var scopeProvider = ComparisonScopeProvider ?? new ComparisonScopeProvider();
            var scopedComparison = scopeProvider.CreateScope(objA, objB, comparisonOptions);

            return scopedComparison.Compare();
        }
    }
}
