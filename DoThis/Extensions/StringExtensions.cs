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

        public static double ToCellFontSize(this string title)
        {
            if (string.IsNullOrWhiteSpace(title)) return 12;
            var length = title.Length;
            if (length > 60) return 12;
            if (length > 50) return 13;
            if (length > 40) return 14;
            if (length > 30) return 15;
            if (length > 20) return 16;
            if (length > 10) return 17;
            if (length > 0) return 18;
            return 12;
        }
    }
}
