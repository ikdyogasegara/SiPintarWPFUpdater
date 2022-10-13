using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SiPintar.Converters
{
    public sealed class NullVisibilityConverter : ZeroCountConverter<Visibility>
    {
        public NullVisibilityConverter() : base(Visibility.Visible, Visibility.Collapsed)
        { }
    }

    public class ZeroCountConverter<T> : IValueConverter
    {
        public ZeroCountConverter(T trueValue, T falseValue)
        {
            True = trueValue;
            False = falseValue;
        }

        public T True { get; set; }
        public T False { get; set; }

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? True : False;
        }

        [ExcludeFromCodeCoverage]
        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is T && EqualityComparer<T>.Default.Equals((T)value, True);
        }

    }
}
