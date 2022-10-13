using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterSumberAirDto
    {
        public int? IdPdam { get; set; }
        public int? IdSumberAir { get; set; }
        public string KodeSumberAir { get; set; }
        public string NamaSumberAir { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterSumberAirFilterDto : ParamMasterSumberAirDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
