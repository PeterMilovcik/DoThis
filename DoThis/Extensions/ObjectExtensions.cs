using System;

namespace Beeffective.Extensions
{
    static class ObjectExtensions
    {
        public static object IfNotNull(this object obj, Action action)
        {
            if (obj != null) action();
            return obj;
        }
    }
}
