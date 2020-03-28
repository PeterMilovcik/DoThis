using System;

namespace Beeffective.Extensions
{
    static class BooleanExtensions
    {
        public static bool IfTrue(this bool value, Action action)
        {
            if (value) action();
            return value;
        }

        public static bool IfFalse(this bool value, Action action)
        {
            if (!value) action();
            return value;
        }

        public static bool Call(this bool value, Action action)
        {
            if (value) action();
            return value;
        }
    }
}
