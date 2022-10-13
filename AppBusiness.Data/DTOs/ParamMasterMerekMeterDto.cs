using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterMerekMeterDto
    {
        public int? IdPdam { get; set; }
        public int? IdMerekMeter { get; set; }
        public string KodeMerekMeter { get; set; }
        public string NamaMerekMeter { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterMerekMeterFilterDto : ParamMasterMerekMeterDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
