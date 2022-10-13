using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace SiPintar.Converters
{
    public class ComparisonToVisibilityConverter : IValueConverter
    {
        public string True { get; set; } = "visible";
        public string False { get; set; } = "collapsed";
        public string Default { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
            {
                if (string.IsNullOrWhiteSpace(Default))
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Default == "visible" ? Visibility.Visible : Visibility.Collapsed;
                }
            }

            string inputParameter = parameter?.ToString() ?? "";

            IEnumerable<string> paramList = inputParameter.Contains("||") ? inputParameter.Split(new[] { "||" }, StringSplitOptions.None) : new[] { inputParameter };

            if (True == "visible")
                return paramList.Any(param => string.Equals(value?.ToString(), param, StringComparison.Ordinal)) ? Visibility.Visible : Visibility.Collapsed;
            return paramList.Any(param => string.Equals(value?.ToString(), param, StringComparison.Ordinal)) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
