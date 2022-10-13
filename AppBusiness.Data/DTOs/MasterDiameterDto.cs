using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterDiameterDto : ICloneable
    {
        public int? IdPdam { get; set; }
        public int? IdDiameter { get; set; }
        public string KodeDiameter { get; set; }
        public string NamaDiameter { get; set; }
        public int? PeriodeMulaiBerlaku { get; set; }
        public decimal? Administrasi { get; set; } = 0;
        public decimal? Pemeliharaan { get; set; } = 0;
        public decimal? Pelayanan { get; set; } = 0;
        public decimal? Retribusi { get; set; } = 0;
        public decimal? DendaPakai0 { get; set; } = 0;
        public decimal? AirLimbah { get; set; } = 0;
        public string NomorSK { get; set; }
        public bool? Status { get; set; } = false;
        public DateTime? WaktuUpdate { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
