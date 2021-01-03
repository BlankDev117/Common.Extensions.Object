using System;
using System.Collections.Generic;
using System.Reflection;
using Common.Extensions.Object.DeepEquals.Models;

namespace Common.Extensions.Object.DeepEquals.Ports
{
    // Primary Port: Implemented Internally
    public interface IPropertyCache
    {
        IEnumerable<PropertyInfo> GetPropertyInfos(Type type, PropertyComparison propertyComparison);
    }
}
