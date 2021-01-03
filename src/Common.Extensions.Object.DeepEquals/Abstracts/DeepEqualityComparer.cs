using System;
using Common.Extensions.Object.DeepEquals.Models;
using Common.Extensions.Object.DeepEquals.Options;
using Common.Extensions.Object.DeepEquals.Ports;

namespace Common.Extensions.Object.DeepEquals.Abstracts
{
    /// <summary>
    /// A base class implementation for <see cref="IDeepEqualityComparer"/> that performs basic parameter validation for child classes
    /// </summary>
    public abstract class DeepEqualityComparer: IDeepEqualityComparer
    {
        #region Variables

        protected DeepComparisonOptions DeepComparisonOptions { get; private set; }
        protected IDeepComparisonService DeepComparisonService { get; private set; }
        protected IPropertyCache PropertyCache { get; private set; }
        protected IObjectCache ObjectCache { get; private set; }
        protected ICircularReferenceMonitor CircularReferenceMonitor { get; private set; }

        #endregion

        #region IDeepEqualityComparer

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException"></exception>
        public void SetConfiguration(ComparerConfiguration comparerConfiguration)
        {
            if (comparerConfiguration == null)
            {
                throw new ArgumentNullException(nameof(comparerConfiguration));
            }

            DeepComparisonService = comparerConfiguration.DeepComparisonService ?? throw new InvalidOperationException($"Unable to set configuration without a {nameof(IDeepComparisonService)}.");
            PropertyCache = comparerConfiguration.PropertyCache ?? throw new InvalidOperationException($"Unable to set a configuration without a {nameof(IPropertyCache)}.");
            ObjectCache = comparerConfiguration.ObjectCache ?? throw new InvalidOperationException($"Unable to set a configuration without a {nameof(IObjectCache)}.");
            CircularReferenceMonitor = comparerConfiguration.CircularReferenceMonitor ?? throw new InvalidOperationException($"Unable to set a configuration without a {nameof(ICircularReferenceMonitor)}.");
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException"></exception>
        public bool CanCompare(Type typeToCompare)
        {
            if (typeToCompare == null)
            {
                throw new ArgumentNullException(nameof(typeToCompare));
            }

            return IsComparerType(typeToCompare);
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public bool AreDeepEqual(object a, object b, DeepComparisonOptions deepComparisonOptions)
        {
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b == null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            if (a.GetType() != b.GetType())
            {
                throw new InvalidOperationException($"Both objects must be the same type. First object is of type {a.GetType().FullName}. Second object is of type {b.GetType().FullName}.");
            }
            if (!CanCompare(a.GetType()))
            {
                throw new InvalidOperationException($"Comparer, of type {GetType().FullName}, is unable to compare objects of type {a.GetType().FullName}.");
            }

            DeepComparisonOptions = deepComparisonOptions ?? throw new ArgumentNullException(nameof(deepComparisonOptions));
            return AreDeepEqual(a, b);
        }

        #endregion

        #region Abstracts

        protected abstract bool IsComparerType(Type type);
        protected abstract bool AreDeepEqual(object a, object b);

        #endregion
    }
}
