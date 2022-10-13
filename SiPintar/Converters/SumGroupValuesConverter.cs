using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Data;

namespace SiPintar.Converters
{
    [ExcludeFromCodeCoverage]
    public class SumGroupValuesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ReadOnlyObservableCollection<object> items) || !(parameter is string field))
                return null;
            return items.Sum(v =>
            {
                var obj = v;
                foreach (var part in field.Split('.'))
                {
                    var type = obj?.GetType();
                    var info = type?.GetProperty(part);
                    obj = info?.GetValue(obj, null);
                }
                return obj as decimal?;
            });
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
