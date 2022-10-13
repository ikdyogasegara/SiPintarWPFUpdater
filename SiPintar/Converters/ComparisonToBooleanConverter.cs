using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace SiPintar.Converters
{
    public class ComparisonToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && parameter != null)
                return parameter.ToString().Split("||").Contains(value.ToString());
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}
