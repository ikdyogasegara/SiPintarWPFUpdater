using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows.Data;

namespace SiPintar.Converters
{
    [ExcludeFromCodeCoverage]
    public class BooleanToStringConverter : IValueConverter
    {
        public string True { get; set; }

        public string False { get; set; }


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool?)value == true ? True : False;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (string)value == True;
        }
    }
}
