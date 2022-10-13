using System;
using System.Collections.Generic;
using System.Text;

namespace AppBusiness.Data.DTOs
{
    public class ParamLaporanPerputaranUangDto
    {
        public int? IdPdam { get; set; }
        public int? IdLpu { get; set; }
        public int? IdPeriode { get; set; }
        public string KodeLpu { get; set; }
        public int? Tahun { get; set; }
        public string NomorLpu { get; set; }
        public string NomorLpu1 { get; set; }
        public string HeaderLpu { get; set; }
        public string HeaderLpu1 { get; set; }
        public string FooterLpu { get; set; }
        public string FooterLpu1 { get; set; }
        public string Uraian { get; set; }
        public decimal? RealisasiSekarang { get; set; }
        public decimal? AnggaranSekarang { get; set; }
        public decimal? SelisihSekarang { get; set; }
        public decimal? PersentaseSekarang { get; set; }
        public decimal? RealisasiTotal { get; set; }
        public decimal? AnggaranTotal { get; set; }
        public decimal? SelisihTotal { get; set; }
        public decimal? PersentaseTotal { get; set; }
        public bool? FlagHapus { get; set; } = false;
    }

    public class ParamLaporanPerputaranUangFilterDto : ParamLaporanPerputaranUangDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
