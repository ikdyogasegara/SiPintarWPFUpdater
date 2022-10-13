using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;

namespace SiPintar.Converters
{
    public class GroupTagihanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ReadOnlyObservableCollection<object>)
            {
                var items = (ReadOnlyObservableCollection<dynamic>)value;

                decimal result;
                if (items?.Count > 0)
                {
                    var total = 0;

                    foreach (var i in items)
                    {
                        if (i.IsSelected)
                        {
                            total += i.TotalWpf;
                        }
                    }

                    result = total;
                }
                else
                {
                    result = 0;
                }

                return result;
            }

            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
