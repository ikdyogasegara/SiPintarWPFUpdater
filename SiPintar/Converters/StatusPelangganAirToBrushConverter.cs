using System;
using System.Globalization;
using System.Windows.Data;

namespace SiPintar.Converters
{
    public class StatusPelangganAirToBrushConverter : IValueConverter
    {
        private readonly string Aktif = "#FFFFFF";
        private readonly string TutupSementara = "#E4E2E2";
        private readonly string Segel = "#FCDDD4";
        private readonly string TutupTotalNonAktif = "#FFCBD5";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var Data = value as string;
            return Data.ToLower() switch
            {
                "aktif" => Aktif,
                "tutup sementara" => TutupSementara,
                "segel" => Segel,
                "non aktif" => TutupTotalNonAktif,
                "tutup total" => TutupTotalNonAktif,
                _ => Aktif
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
