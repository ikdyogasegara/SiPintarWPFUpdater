using System;
using System.Globalization;
using System.Windows.Data;

namespace SiPintar.Converters
{
    public class StatusPermohonanNextToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var Data = value as int?;
            return Data switch
            {
                3 => "",
                2 => "Proses BA",
                1 => "Proses RAB",
                _ => "Proses SPK"
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
