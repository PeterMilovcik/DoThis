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

        public static object IfNotNull<T>(this T obj, Action<T> action)
        {
            if (obj != null) action(obj);
            return obj;
        }
    }
}
