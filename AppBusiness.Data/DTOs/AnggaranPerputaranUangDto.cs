using System;

namespace AppBusiness.Data.DTOs
{
    public class AnggaranPerputaranUangDto
    {
        public int? IdPdam { get; set; }
        public int? IdAnggaranPu { get; set; }
        public int? IdJenis { get; set; }
        public string KodeJenis { get; set; }
        public string NamaJenis { get; set; }
        public string TipeJenis { get; set; }
        public int? IdPerkiraan { get; set; }
        public string KodePerkiraan { get; set; }
        public string NamaPerkiraan { get; set; }
        public int? IdWilayah { get; set; }
        public string KodeWilayah { get; set; }
        public string NamaWilayah { get; set; }
        public int? Tahun { get; set; }
        public decimal? Bln01 { get; set; }
        public decimal? Bln02 { get; set; }
        public decimal? Bln03 { get; set; }
        public decimal? Bln04 { get; set; }
        public decimal? Bln05 { get; set; }
        public decimal? Bln06 { get; set; }
        public decimal? Bln07 { get; set; }
        public decimal? Bln08 { get; set; }
        public decimal? Bln09 { get; set; }
        public decimal? Bln10 { get; set; }
        public decimal? Bln11 { get; set; }
        public decimal? Bln12 { get; set; }
        public bool? FlagRekap { get; set; }
        public bool? FlagHapus { get; set; }
        public DateTime? WaktuUpdate { get; set; }
        public decimal? TotalPenerimaan => TipeJenis == "penerimaan" ? Bln01 + Bln02 + Bln03 + Bln04 + Bln05 + Bln06 + Bln07 + Bln08 + Bln09 + Bln10 + Bln11 + Bln12 : 0;
        public decimal? TotalPengeluaran => TipeJenis == "pengeluaran" ? Bln01 + Bln02 + Bln03 + Bln04 + Bln05 + Bln06 + Bln07 + Bln08 + Bln09 + Bln10 + Bln11 + Bln12 : 0;
    }
}
