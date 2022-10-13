using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace SiPintar.Converters
{
    public class ListToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var data = (List<dynamic>)value;

            var sb = new StringBuilder();
            int counter = 0;
            foreach (var item in data)
            {
                if (counter > 0 && counter < data.Count)
                    sb.Append(", ");
                sb.Append(item);
                counter++;
            }
            return sb.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
