using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SiPintar.Converters
{
    public class ComparisonToStrikethroughTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string compared;
            if (value is bool)
                compared = value.ToString();
            else
                compared = (string)value;

            return (parameter != null && (string)parameter == compared) ? TextDecorations.Strikethrough : null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
