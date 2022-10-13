using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterDiameterDto : ICloneable
    {
        public int? IdPdam { get; set; }
        public int? IdDiameter { get; set; }
        public string KodeDiameter { get; set; }
        public string NamaDiameter { get; set; }
        public int? PeriodeMulaiBerlaku { get; set; }
        public decimal? Administrasi { get; set; }
        public decimal? Pemeliharaan { get; set; }
        public decimal? Pelayanan { get; set; }
        public decimal? Retribusi { get; set; }
        public decimal? DendaPakai0 { get; set; }
        public decimal? AirLimbah { get; set; }
        public string NomorSK { get; set; }
        public bool? Status { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class ParamMasterDiameterFilterDto : ParamMasterDiameterDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
