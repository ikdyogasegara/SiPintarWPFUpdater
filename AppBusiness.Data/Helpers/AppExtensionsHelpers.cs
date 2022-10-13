using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AppBusiness.Data.DTOs;

namespace AppBusiness.Data.Helpers
{
    public static class AppExtensionsHelpers
    {
        public static string Left(this string input, int count)
        {
            return input.Substring(0, Math.Min(input.Length, count));
        }

        public static string Right(this string input, int count)
        {
            return input.Substring(Math.Max(input.Length - count, 0), Math.Min(count, input.Length));
        }

        public static string Mid(this string input, int startIndex, int count)
        {
            return input.Substring(startIndex, count);
        }

        public static string Bulan(string value)
        {
            return value switch
            {
                "01" => "Januari",
                "02" => "Februari",
                "03" => "Maret",
                "04" => "April",
                "05" => "Mei",
                "06" => "Juni",
                "07" => "Juli",
                "08" => "Agustus",
                "09" => "September",
                "10" => "Oktober",
                "11" => "Nopember",
                "12" => "Desember",
                _ => "-"
            };
        }

        public static string RomawiBulan(int value)
        {
            return value switch
            {
                1 => "I",
                2 => "II",
                3 => "III",
                4 => "IV",
                5 => "V",
                6 => "VI",
                7 => "VII",
                8 => "VIII",
                9 => "IX",
                10 => "X",
                11 => "XI",
                12 => "XII",
                _ => "-"
            };
        }

        public static dynamic DinamicValue(
            string tipedata = null,
            string valuestring = null,
            decimal? valuedecimal = null,
            int? valueinteger = null,
            DateTime? valuedate = null,
            bool? valuebool = null)
        {
            dynamic result = null;

            switch (tipedata)
            {
                case "string":
                    result = valuestring;
                    break;
                case "decimal":
                    result = valuedecimal;
                    break;
                case "int":
                    result = valueinteger;
                    break;
                case "date":
                    result = valuedate.HasValue ? valuedate.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty;
                    break;
                case "bool":
                    result = valuebool;
                    break;
            }

            return result;
        }

        public static string MetodeBaca(int? value)
        {
            return value switch
            {
                0 => "Manual",
                1 => "Step 1",
                2 => "Step 2",
                3 => "Step 3",
                4 => "Step 4",
                _ => null
            };
        }

        public static string RequestBacaUlang(int? value)
        {
            return value switch
            {
                1 => "Request Baca Ulang",
                2 => "Menunggu Konfirmasi Hasil",
                3 => "Hasil Baca Ulang Diterima",
                4 => "Hasil Baca Ulang Ditolak",
                _ => null
            };
        }

        public static dynamic GetPropertiClass(dynamic dto)
        {
            var result = new ObservableCollection<KeyValuePair<string, Type>>();
            foreach (var p in dto.GetProperties())
            {
                result.Add(new KeyValuePair<string, Type>(p.Name, p.PropertyType));
            }

            return result;
        }

        public static int MonthDifference(DateTime startDate, DateTime endDate)
        {
            return Math.Abs(startDate.Month - endDate.Month + (12 * (startDate.Year - endDate.Year)));
        }

        public static string GetStatusVerifikasiUsulanKoreksiText(PermohonanPelangganAirKoreksiRekening param)
        {
            if (param.StatusVerifikasiPusat == 1)
            {
                return "(Selesai) Sudah Verifikasi Pusat";
            }

            if (param.StatusVerifikasiPusat == 2)
            {
                return "(Selesai) Ditolak Pusat";
            }

            if (param.StatusVerifikasiLapangan == 2)
            {
                return "(Selesai) Ditolak Cabang/Lapangan";
            }

            if (param.StatusVerifikasiLapangan == 0 || param.StatusVerifikasiLapangan is null)
            {
                return "Menunggu Verifikasi Lapangan";
            }

            if (param.StatusVerifikasiLapangan == 1)
            {
                return "Menunggu Verifikasi Pusat";
            }

            return "-";
        }
    }
}
