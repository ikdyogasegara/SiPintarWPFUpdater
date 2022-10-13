using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamJurnalUmumDto
    {
        public int? IdPdam { get; set; }
        public int? IdTransaksi { get; set; }
        public string NomorTransaksi { get; set; }
        public string Uraian { get; set; }
        public string Penjelasan { get; set; }
        public decimal? Jumlah { get; set; }
        public DateTime? TanggalTransaksi { get; set; }
        public string Kategori { get; set; }
        public bool? FlagPosting { get; set; }
        public bool? FlagHapus { get; set; }
        public List<ParamJurnalUmumDetailDto> JUDetail { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamJurnalUmumDetailDto
    {
        public int? IdDetailTransaksi { get; set; }
        public int? IdPerkiraan { get; set; }
        public int? IdWilayah { get; set; }
        public string Jenis { get; set; }
        public decimal? Debet { get; set; }
        public decimal? Kredit { get; set; }
        public decimal? Saldo { get; set; }
        public string SumberData { get; set; } = "JU";
        public string Operasi { get; set; } = "7";
    }

    public class ParamJurnalUmumFilterDto : ParamJurnalUmumDto
    {
        public int? IdPerkiraan { get; set; }
        public string KodePerkiraan { get; set; }
        public string NamaPerkiraan { get; set; }
        public int? IdWilayah { get; set; }
        public string KodeWilayah { get; set; }
        public string NamaWilayah { get; set; }
        public string Jenis { get; set; }
        public decimal? Debet { get; set; }
        public decimal? Kredit { get; set; }
        public decimal? Saldo { get; set; }
        public string SumberData { get; set; } = "JU";
        public string Operasi { get; set; } = "7";
    }

    public class ParamPostingJurnalUmumDto
    {
        public int? IdPdam { get; set; }
        public int? IdTransaksi { get; set; }
        public int? IdUserRequest { get; set; }
    }
}
