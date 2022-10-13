using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace SiPintar.Converters
{
    public class DecimalComparisonToBrushConverter : IValueConverter
    {
        public string TrueBrush { get; set; }
        public string FalseBrush { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && parameter != null)
                return System.Convert.ToDecimal(value) == System.Convert.ToDecimal(parameter) ? TrueBrush : FalseBrush;
            return FalseBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return FalseBrush;
        }
    }
}
