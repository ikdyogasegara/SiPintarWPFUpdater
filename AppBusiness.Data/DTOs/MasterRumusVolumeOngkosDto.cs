using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterRumusVolumeOngkosDto
    {
        public int? IdPdam { get; set; }
        public int? IdRumusVolumeOngkos { get; set; }
        public int? IdOngkos { get; set; }
        public string KodeOngkos { get; set; }
        public string NamaOngkos { get; set; }
        public string Satuan { get; set; }
        public decimal? Bb1 { get; set; } = 0;
        public decimal? Ba1 { get; set; } = 0;
        public decimal? Bb2 { get; set; } = 0;
        public decimal? Ba2 { get; set; } = 0;
        public decimal? Bb3 { get; set; } = 0;
        public decimal? Ba3 { get; set; } = 0;
        public decimal? Bb4 { get; set; } = 0;
        public decimal? Ba4 { get; set; } = 0;
        public decimal? Bb5 { get; set; } = 0;
        public decimal? Ba5 { get; set; } = 0;
        public decimal? Volum1 { get; set; } = 0;
        public decimal? Volum2 { get; set; } = 0;
        public decimal? Volum3 { get; set; } = 0;
        public decimal? Volum4 { get; set; } = 0;
        public decimal? Volum5 { get; set; } = 0;
        public bool? FlagHapus { get; set; } = false;
        public DateTime? WaktuUpdate { get; set; }
    }
}
