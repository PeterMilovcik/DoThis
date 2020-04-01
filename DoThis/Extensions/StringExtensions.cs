using System.Collections.Generic;
using System.Linq;

namespace Beeffective.Extensions
{
    static class StringExtensions
    {
        public static List<string> ParseCategories(this string text)
        {
            if (string.IsNullOrEmpty(text)) return new List<string>();
            return text.Split(" ")
                .Where(w => w.StartsWith("#") || w.StartsWith("@"))
                .Select(w => w.Replace("#", ""))
                .Select(w => w.Replace("@", "")).ToList();
        }
    }
}
