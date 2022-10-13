using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterTarifTangkiDto
    {
        public int? IdPdam { get; set; }
        public int? IdTarifTangki { get; set; }
        public string KodeTarifTangki { get; set; }
        public string NamaTarifTangki { get; set; }
        public string Satuan { get; set; }
        public string Kategori { get; set; }
        public int? IdJenisNonAir { get; set; }
        public string KodeJenisNonAir { get; set; }
        public string NamaJenisNonAir { get; set; }
        public decimal? BiayaAir { get; set; } = 0;
        public decimal? BiayaAdministrasi { get; set; } = 0;
        public decimal? BiayaOperasional { get; set; } = 0;
        public decimal? BiayaTransport { get; set; } = 0;
        public decimal? Ppn { get; set; } = 0;
        public string ProgresifDari { get; set; } = "jumlahm3";
        public decimal? Bb1 { get; set; } = 0;
        public decimal? Bb2 { get; set; } = 0;
        public decimal? Bb3 { get; set; } = 0;
        public decimal? Bb4 { get; set; } = 0;
        public decimal? Bb5 { get; set; } = 0;
        public decimal? Ba1 { get; set; } = 0;
        public decimal? Ba2 { get; set; } = 0;
        public decimal? Ba3 { get; set; } = 0;
        public decimal? Ba4 { get; set; } = 0;
        public decimal? Ba5 { get; set; } = 0;
        public decimal? T1 { get; set; } = 0;
        public decimal? T2 { get; set; } = 0;
        public decimal? T3 { get; set; } = 0;
        public decimal? T4 { get; set; } = 0;
        public decimal? T5 { get; set; } = 0;
        public DateTime? WaktuUpdate { get; set; }
    }
}
