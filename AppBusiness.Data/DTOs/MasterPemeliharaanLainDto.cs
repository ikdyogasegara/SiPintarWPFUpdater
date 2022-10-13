using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterPemeliharaanLainDto
    {
        public int? IdPdam { get; set; }
        public int? IdPemeliharaanLain { get; set; }
        public string KodePemeliharaanLain { get; set; }
        public string NamaPemeliharaanLain { get; set; }
        public decimal? Pemeliharaan { get; set; } = 0;
        public DateTime? WaktuUpdate { get; set; }
    }
}
