using System;
using System.Globalization;
using System.Windows.Data;

namespace SiPintar.Converters
{
    public class StatusPermohonanToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var Data = value as int?;
            return Data switch
            {
                3 => "B.A",
                2 => "RAB",
                1 => "SPK",
                _ => "Aktif"
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
