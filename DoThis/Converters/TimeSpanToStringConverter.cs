using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Beeffective.Converters
{
    class TimeSpanToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = string.Empty;
            if (value is TimeSpan timeSpan)
            {
                if (timeSpan.TotalDays > 1)
                {
                    result += $"{timeSpan.Days}d ";
                }
                if (timeSpan.TotalHours > 1)
                {
                    result += $"{timeSpan.Hours}h ";
                }
                if (timeSpan.TotalMinutes > 1)
                {
                    result += $"{timeSpan.Minutes}m ";
                }
                if (timeSpan.TotalSeconds > 1)
                {
                    result += $"{timeSpan.Seconds}s";
                }
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
