using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterStatusSegelDto
    {
        public int? IdPdam { get; set; }
        public int? IdStatusSegel { get; set; }
        public string StatusSegel { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterStatusSegelFilterDto : ParamMasterStatusSegelDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
