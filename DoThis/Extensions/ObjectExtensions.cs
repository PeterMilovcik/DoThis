using System;

namespace DoThis.Extensions
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
