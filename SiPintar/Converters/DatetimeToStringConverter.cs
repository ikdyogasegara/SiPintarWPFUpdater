using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows.Data;

namespace SiPintar.Converters
{
    [ExcludeFromCodeCoverage]
    public class DatetimeToStringConverter : IValueConverter
    {
        public bool TimeInclude { get; set; } = true;
        public string Format { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is DateTime))
                return value;

            if (Format != null)
            {
                return ((DateTime)value).ToString(Format);
            }

            var date = (DateTime)value;

            var monthNames = new CultureInfo("id-ID").DateTimeFormat.MonthNames;

            var isIncludeTime = true;
            if (parameter != null)
            {
                var param = parameter as string;
                var option = param.Split("||");

                foreach (var opt in option)
                {
                    if (opt == "short")
                        monthNames = new CultureInfo("id-ID").DateTimeFormat.AbbreviatedMonthNames;
                    if (opt == "notime")
                        isIncludeTime = false;
                }
            }

            var result = date.Day + " " + monthNames[date.Month - 1] + " " + date.Year;
            if (isIncludeTime && TimeInclude)
                result += " " + date.Hour + ":" + date.Minute + ":" + date.Second;

            return result;
        }

        [ExcludeFromCodeCoverage]
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
