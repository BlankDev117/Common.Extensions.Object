using System;
using Common.Extensions.Object.DeepEquals.Abstracts;

namespace Common.Extensions.Object.DeepEquals.Internal.Comparers
{
    internal class NumberComparer : DeepEqualityComparer
    {
        #region DeepEqualityComparer Overrides

        protected override bool IsComparerType(Type typeToCompare)
        {
            return IsNumericType(typeToCompare);
        }

        protected override bool AreDeepEqual(object a, object b)
        {
            return a.Equals(b);
        }

        #endregion

        #region Helpers

        private bool IsNumericType(Type type)
        {
            return type == typeof(byte) ||
                   type == typeof(sbyte) ||
                   type == typeof(char) ||
                   type == typeof(decimal) ||
                   type == typeof(double) ||
                   type == typeof(float) ||
                   type == typeof(int) ||
                   type == typeof(uint) ||
                   type == typeof(long) ||
                   type == typeof(ulong) ||
                   type == typeof(short) ||
                   type == typeof(ushort);
        }

        #endregion
    }
}
