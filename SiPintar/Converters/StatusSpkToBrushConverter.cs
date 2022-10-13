using System;
using System.Globalization;
using System.Windows.Data;
using AppBusiness.Data.DTOs;

namespace SiPintar.Converters
{
    public class StatusSpkToBrushConverter : IValueConverter
    {
        private readonly string Aktif = "#FFFFFF";
        private readonly string SudahBA = "#96DAFF";
        private readonly string SudahRAB = "#DDCCF4";
        private readonly string Batal = "#FFCDCD";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PermohonanPelangganAirDto)
            {
                var Data = value as PermohonanPelangganAirDto;
                if (Data.BeritaAcara != null)
                {
                    return SudahBA;
                }
                if (Data.RAB != null)
                {
                    return SudahRAB;
                }
                if (Data.SPK != null)
                {
                    if (Data.SPK.FlagBatal.HasValue && Data.SPK.FlagBatal.Value)
                    {
                        return Batal;
                    }
                    else
                    {
                        return Aktif;
                    }
                }
            }
            if (value is PermohonanPelangganLimbahDto)
            {
                var Data = value as PermohonanPelangganLimbahDto;
                if (Data.BeritaAcara != null)
                {
                    return SudahBA;
                }
                if (Data.RAB != null)
                {
                    return SudahRAB;
                }
                if (Data.SPK != null)
                {
                    if (Data.SPK.FlagBatal.HasValue && Data.SPK.FlagBatal.Value)
                    {
                        return Batal;
                    }
                    else
                    {
                        return Aktif;
                    }
                }
            }

            if (value is PermohonanPelangganLlttDto)
            {
                var Data = value as PermohonanPelangganLlttDto;
                if (Data.BeritaAcara != null)
                {
                    return SudahBA;
                }
                if (Data.RAB != null)
                {
                    return SudahRAB;
                }
                if (Data.SPK != null)
                {
                    if (Data.SPK.FlagBatal.HasValue && Data.SPK.FlagBatal.Value)
                    {
                        return Batal;
                    }
                    else
                    {
                        return Aktif;
                    }
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
