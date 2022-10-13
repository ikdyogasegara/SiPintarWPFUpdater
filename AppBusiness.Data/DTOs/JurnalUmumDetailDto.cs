using System;

namespace AppBusiness.Data.DTOs
{
    public class JurnalUmumDetailDto : ICloneable
    {
        public int? IdDetailTransaksi { get; set; }
        public int? IdPdam { get; set; }
        public int? IdTransaksi { get; set; }
        public int? IdPerkiraan { get; set; }
        public int? IdWilayah { get; set; }
        public DateTime? TanggalTransaksi { get; set; }
        public string Jenis { get; set; }
        public decimal? Debet { get; set; } = 0;
        public decimal? Kredit { get; set; } = 0;
        public decimal? Saldo { get; set; } = 0;
        public string SumberData { get; set; }
        public string Operasi { get; set; }
        public string Uraian { get; set; }
        public string Penjelasan { get; set; }
        public decimal? Jumlah { get; set; }
        public string Kategori { get; set; }
        public bool? FlagPosting { get; set; } = false;
        public string KodePerkiraan { get; set; }
        public string NamaPerkiraan { get; set; }
        public string KodeWilayah { get; set; }
        public string NamaWilayah { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public DateTime? WaktuUpdate { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
