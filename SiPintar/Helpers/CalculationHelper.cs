using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows.Data;

namespace SiPintar.Helpers
{
    // using multi value converter (in-app xaml code)
    public class CalculationHelper : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            decimal total = 0;

            try
            {
                foreach (var item in values)
                    total += decimal.Parse(item.ToString());
            }
            catch (Exception error)
            {
                Debug.WriteLine(error.Message);
            }

            return total;
        }

        [ExcludeFromCodeCoverage]
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
