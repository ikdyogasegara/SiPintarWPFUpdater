using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterKondisiMeterDto
    {
        public int? IdPdam { get; set; }
        public int? IdKondisiMeter { get; set; }
        public string KodeKondisiMeter { get; set; }
        public string NamaKondisiMeter { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterKondisiMeterFilterDto : ParamMasterKondisiMeterDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
