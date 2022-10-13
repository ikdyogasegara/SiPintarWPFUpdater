using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace SiPintar.Converters
{
    [ExcludeFromCodeCoverage]
    public class SumWidthConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var sumActualWidths = values.Sum(v => System.Convert.ToDouble(v));
            return targetType == typeof(GridLength) ? new GridLength(sumActualWidths) : System.Convert.ChangeType(sumActualWidths, targetType);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
