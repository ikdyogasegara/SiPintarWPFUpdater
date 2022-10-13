using System;
using System.Globalization;
using System.Windows.Data;

namespace SiPintar.Converters
{
    public class StringDefaultValueConverter : IValueConverter
    {
        public string Default { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (!string.IsNullOrWhiteSpace(value?.ToString()))
                ? value
                : Default;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (!string.IsNullOrEmpty(value?.ToString()))
                ? value
                : Default;
        }
    }
}
