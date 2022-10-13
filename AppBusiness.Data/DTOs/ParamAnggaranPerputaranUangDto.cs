using System;
using System.Collections.Generic;
using System.Text;

namespace AppBusiness.Data.DTOs
{
    public class ParamAnggaranPerputaranUangDto
    {
        public int? IdPdam { get; set; }
        public int? IdAnggaranPu { get; set; }
        public int? IdJenis { get; set; }
        public int? IdPerkiraan { get; set; }
        public int? IdWilayah { get; set; }
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
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamAnggaranPerputaranUangFilterDto : ParamAnggaranPerputaranUangDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
