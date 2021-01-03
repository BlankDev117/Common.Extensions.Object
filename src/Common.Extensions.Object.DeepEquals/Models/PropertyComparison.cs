using System;

namespace Common.Extensions.Object.DeepEquals.Models
{
    /// <summary>
    /// The property comparison rules to use when comparing object properties. Public properties will always be compared.
    /// </summary>
    [Flags]
    public enum PropertyComparison
    {
        /// <summary>
        /// The default property comparison will only check for public properties
        /// </summary>
        Default = 0,

        /// <summary>
        /// If included, public static properties will be included when comparing object properties
        /// </summary>
        IncludeStatic = 1,

        /// <summary>
        /// If included, non public properties will be included when comparing objects
        /// </summary>
        IncludeNonPublic = 2
    }
}
