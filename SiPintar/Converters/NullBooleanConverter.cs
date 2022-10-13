using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows.Data;

namespace SiPintar.Converters
{
    public sealed class NullBooleanConverter : NullCheckConverter<bool>
    {
        public NullBooleanConverter() : base(true, false)
        { }
    }

    public class NullCheckConverter<T> : IValueConverter
    {
        public NullCheckConverter(T trueValue, T falseValue)
        {
            True = trueValue;
            False = falseValue;
        }

        public T True { get; set; }
        public T False { get; set; }

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                return True;
            return False;
        }

        [ExcludeFromCodeCoverage]
        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
