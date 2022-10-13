using System;
using System.Globalization;
using System.Windows.Data;

namespace SiPintar.Converters
{
    public class DatetimeToCurrentAgeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is DateTime))
                return value;

            var date = (DateTime)value;
            var currentDate = DateTime.Today;

            int age = 0;

            if (currentDate.Year > date.Year)
            {
                age = currentDate.Year - date.Year;
                if ((currentDate.Month < date.Month) || (currentDate.Month == date.Month && currentDate.Day < date.Day))
                {
                    age--;
                }
            }
            return age;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
