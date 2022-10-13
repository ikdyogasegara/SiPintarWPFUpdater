using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;

namespace SiPintar.Converters
{
    public class GroupPaketToNamaConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ReadOnlyObservableCollection<object>)
            {
                var items = (ReadOnlyObservableCollection<dynamic>)value;
                return items?.Count > 0 ? items[0].NamaPaketBarang : "-";
            }
            return "Error";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
