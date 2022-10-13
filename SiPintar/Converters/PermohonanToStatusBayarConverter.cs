using System;
using System.Globalization;
using System.Windows.Data;
using AppBusiness.Data.DTOs;

namespace SiPintar.Converters
{
    public class PermohonanToStatusBayarConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is RekeningNonAirViewDto)
            {
                var bs = value as RekeningNonAirViewDto;
                if (bs.IdUserTransaksi.HasValue && bs.StatusTransaksi.HasValue && bs.StatusTransaksi.Value)
                {
                    return "LUNAS";
                }
            }
            return "BELUM";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
