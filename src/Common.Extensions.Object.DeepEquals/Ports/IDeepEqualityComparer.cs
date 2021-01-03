using System;
using Common.Extensions.Object.DeepEquals.Models;
using Common.Extensions.Object.DeepEquals.Options;

namespace Common.Extensions.Object.DeepEquals.Ports
{
    /// <summary>
    /// A comparer capable of performing a deep comparison between objects
    /// </summary>
    // Primary Port: Implemented Internally
    public interface IDeepEqualityComparer
    {
        void SetConfiguration(ComparerConfiguration configuration);

        /// <summary>
        /// Determines whether the comparer can handle comparing objects of a given type
        /// </summary>
        /// <param name="typeToCompare">The type of object that is expected to be used when calling <see cref="AreDeepEqual"/></param>
        /// <returns>True/False</returns>
        bool CanCompare(Type typeToCompare);

        /// <summary>
        /// Performs a deep comparison on the given objects. Objects passed are expected to comply with the types the comparer <see cref="CanCompare"/>
        /// </summary>
        /// <param name="a">The first object to compare</param>
        /// <param name="b">The second object to compare against <see cref="a"/></param>
        /// <param name="deepComparisonOptions">The options to use for specific type comparisons</param>
        /// <returns>True/False for whether a deeply equals b</returns>
        bool AreDeepEqual(object a, object b, DeepComparisonOptions deepComparisonOptions);
    }
}
