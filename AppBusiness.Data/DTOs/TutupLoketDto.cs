using System;

namespace AppBusiness.Data.DTOs
{
    public class TutupLoketDto
    {
        public int? IdPdam { get; set; }
        public int? IdTutupLoket { get; set; }
        public DateTime? TglPenerimaan { get; set; }
        public int? IdLoket { get; set; }
        public string KodeLoket { get; set; }
        public string NamaLoket { get; set; }
        public int? IdUser { get; set; }
        public string Nama { get; set; }
        public decimal? Penerimaan { get; set; }
        public decimal? UangKecil { get; set; }
        public string Foto { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }

    public class TutupLoketSummaryDto
    {
        public string Keterangan { get; set; }
        public decimal? Jumlah { get; set; }
    }
}
