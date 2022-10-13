using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows.Data;

namespace SiPintar.Converters
{
    public class PeriodeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var YearMonth = value as string;

            if (YearMonth == null)
                YearMonth = value.ToString();

            string Year = YearMonth.Substring(0, 4);
            int Month = int.Parse(YearMonth.Substring(4));

            string[] MonthNames = new CultureInfo("id-ID").DateTimeFormat.MonthNames;

            return MonthNames[Month - 1] + " " + Year;
        }

        [ExcludeFromCodeCoverage]
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
