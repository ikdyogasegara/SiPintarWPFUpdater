using System;
using System.Diagnostics.CodeAnalysis;

namespace SiPintar.Utilities
{
    [ExcludeFromCodeCoverage]
    public static class AppSetting
    {
        public static bool AksesFotoMeter { get; set; }
        public static string LokasiFotoMeter { get; set; } = "D:\\Foto";
        public static bool LockVerifikasiBacameter { get; set; } = true;
        public static bool PelangganLimbah { get; set; }
        public static bool PelangganLltt { get; set; }
        public static bool PremiumModule { get; set; }


        #region untuk cek tutup loket
        public static bool LoketTutup { get; set; }
        public static bool IsNamaRoleAdmin { get; set; }
        public static string ElasticSearchUrl { get; set; } = "http://103.135.24.13:9200";

        #endregion
    }
}
