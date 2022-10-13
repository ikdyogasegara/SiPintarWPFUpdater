using System;
using System.Globalization;
using System.Windows.Data;

namespace SiPintar.Converters
{
    public class StringToBooleanConverter : IValueConverter
    {
        public string True { get; set; }

        public string False { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString() switch
            {
                string t when True != null && True == t => true,
                string f when False != null && False == f => false,
                string f when True == null && False != null && False != f => true,
                _ => false
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
