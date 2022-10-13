using System;

namespace AppBusiness.Data.DTOs
{
    public class SummaryHistoriPembayaranDto
    {
        public int? IdPdam { get; set; }
        public DateTime? WaktuTransaksi { get; set; }
        public string Kategori { get; set; }
        public string NamaPeriode { get; set; }
        public decimal? Rekening { get; set; } = 0;
        public decimal? Denda { get; set; } = 0;
        public decimal? Total { get; set; } = 0;
        public string NamaUser { get; set; }
        public string NamaLoket { get; set; }
    }
}
