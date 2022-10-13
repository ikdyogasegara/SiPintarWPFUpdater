using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterMeteraiDto
    {
        public int? IdPdam { get; set; }
        public int? KodePeriodeMulaiBerlaku { get; set; }
        public decimal? Batas1 { get; set; } = 0;
        public decimal? Batas2 { get; set; } = 0;
        public decimal? Batas3 { get; set; } = 0;
        public decimal? Meterai1 { get; set; } = 0;
        public decimal? Meterai2 { get; set; } = 0;
        public decimal? Meterai3 { get; set; } = 0;
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterMeteraiFilterDto : ParamMasterMeteraiDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
