using System.Collections.Generic;
using System.ComponentModel;
using Common.Extensions.Object.DeepEquals.Internal.Comparers;
using Common.Extensions.Object.DeepEquals.Models;
using Common.Extensions.Object.DeepEquals.Ports;

namespace Common.Extensions.Object.DeepEquals.Options
{
    public class DeepComparisonOptions
    {
        #region Defaults

        /// <summary>
        /// The default comparers include basic implementations for the following types:
        ///  Numeric,
        ///  Boolean,
        ///  String,
        ///  DateTime,
        ///  IDictionary,
        ///  IEnumerable,
        ///  Property objects (i.e classes, custom structs, etc.)
        /// </summary>
        public static IEnumerable<IDeepEqualityComparer> DefaultComparers =>
            new List<IDeepEqualityComparer>()
            {
                new NumberComparer(),
                new BooleanComparer(),
                new StringComparer(),
                new EnumComparer(),
                new DateComparer(),
                new DictionaryComparer(),
                new EnumerableComparer(),
                new PropertyComparer()
            };

        #endregion

        #region Options

        /// <summary>
        /// Sets whether case is ignored when comparing string types.
        ///
        /// Defaults to false.
        /// </summary>
        [DefaultValue(false)]
        public bool IgnoreCaseSensitivity { get; set; } = false;

        /// <summary>
        /// Sets whether enumerable order will fail an enumerable comparison.
        ///
        /// Defaults to true.
        /// <remarks>Slower performance may occur if enumerable order is not enforced.</remarks>
        /// </summary>
        public bool EnforceEnumerableOrdering { get; set; } = true;

        /// <summary>
        /// The set of equality comparers to use when comparing objects.
        /// Ordering of comparers matters; comparers are used on a first come first used basis when a comparer that can compare a type is found.
        ///
        /// Defaults to using <see cref="DefaultComparers"/>.
        /// </summary>
        public IEnumerable<IDeepEqualityComparer> DeepEqualityComparers { get; set; } = DefaultComparers;

        /// <summary>
        /// The <see cref="Models.PropertyComparison"/> rules to use when comparing properties.
        ///
        /// Defaults to <see cref="Models.PropertyComparison.Default"/>
        /// </summary>
        public PropertyComparison PropertyComparison { get; set; } = PropertyComparison.Default;

        #endregion
    }
}
