using System;

namespace Beeffective.Extensions
{
    static class BooleanExtensions
    {
        public static bool Call(this bool value, Action action)
        {
            if (value) action();
            return value;
        }
    }
}
