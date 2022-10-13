using System;
using System.Globalization;
using System.Windows.Data;
using AppBusiness.Data.DTOs;

namespace SiPintar.Converters
{
    public class StatusRabToBrushConvert : IValueConverter
    {
        private readonly string Aktif = "#FFFFFF";
        private readonly string SudahLunasRAB = "#FCE9ED";
        private readonly string SudahBA = "#96DAFF";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PermohonanPelangganAirDto)
            {
                var Data = value as PermohonanPelangganAirDto;
                if (Data.BeritaAcara != null)
                {
                    return SudahBA;
                }
                if (Data.RAB.NonAirRab.StatusTransaksi == true)
                {
                    return SudahLunasRAB;
                }
            }
            return Aktif;

        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
