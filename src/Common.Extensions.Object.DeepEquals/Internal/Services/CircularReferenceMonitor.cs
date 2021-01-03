using System;
using System.Collections;
using System.Collections.Generic;
using Common.Extensions.Object.DeepEquals.Ports;

namespace Common.Extensions.Object.DeepEquals.Internal.Services
{
    internal class CircularReferenceMonitor: ICircularReferenceMonitor
    {
        #region Variables

        private readonly Dictionary<object, HashSet<object>> _references;
        private readonly Type _enumerableType = typeof(IEnumerable);

        #endregion

        #region Constructors

        public CircularReferenceMonitor()
        {
            _references = new Dictionary<object, HashSet<object>>();
        }

        #endregion

        #region ICircularReferenceMonitor

        public bool AddReference(object parent, object child)
        {
            if (parent == null || child == null)
            {
                return false;
            }
            if (IsNonCircularType(parent) || IsNonCircularType(child))
            {
                return false;
            }
            if (ReferenceEquals(parent, child))
            {
                return true;
            }

            if (!_references.TryGetValue(parent, out var referenceHistory))
            {
                referenceHistory = new HashSet<object>()
                {
                    parent,
                    child
                };

                _references[parent] = referenceHistory;
                _references[child] = referenceHistory;
                return false;
            }

            if (referenceHistory.Contains(child))
            {
                return true;
            }

            referenceHistory.Add(child);
            _references[child] = referenceHistory;

            return false;
        }

        #endregion

        #region Helpers

        private bool IsNonCircularType(object obj)
        {
            var objType = obj.GetType();

            return !objType.IsClass || _enumerableType.IsAssignableFrom(objType);
        }

        #endregion
    }
}
