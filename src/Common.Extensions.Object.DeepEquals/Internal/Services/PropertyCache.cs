using System;
using System.Collections.Generic;
using System.Reflection;
using Common.Extensions.Object.DeepEquals.Models;
using Common.Extensions.Object.DeepEquals.Ports;

namespace Common.Extensions.Object.DeepEquals.Internal.Services
{
    internal class PropertyCache: IPropertyCache
    {
        #region Variables

        private Dictionary<Type, IEnumerable<PropertyInfo>> _propertyCache;

        #endregion

        #region Constructors

        public PropertyCache()
        {
            _propertyCache = new Dictionary<Type, IEnumerable<PropertyInfo>>();
        }

        #endregion

        #region IPropertyCache

        public IEnumerable<PropertyInfo> GetPropertyInfos(Type type, PropertyComparison propertyComparison)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (_propertyCache.TryGetValue(type, out var propertyInfos))
            {
                return propertyInfos;
            }

            var bindingFlags = GetPropertyBindings(propertyComparison);
            propertyInfos = type.GetProperties(bindingFlags);

            _propertyCache[type] = propertyInfos;

            return propertyInfos;
        }

        #endregion

        #region Helpers

        private BindingFlags GetPropertyBindings(PropertyComparison propertyComparison)
        {
            var flags = BindingFlags.Instance | BindingFlags.Public;

            if (propertyComparison.HasFlag(PropertyComparison.IncludeStatic))
            {
                flags |= BindingFlags.Static;
            }
            if (propertyComparison.HasFlag(PropertyComparison.IncludeNonPublic))
            {
                flags |= BindingFlags.NonPublic;
            }

            return flags;
        }

        #endregion
    }
}
