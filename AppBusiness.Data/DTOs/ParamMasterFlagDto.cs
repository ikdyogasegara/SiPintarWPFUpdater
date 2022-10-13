using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterFlagDto
    {
        public int? IdPdam { get; set; }
        public int? IdFlag { get; set; }
        public string NamaFlag { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterFlagFilterDto : ParamMasterFlagDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
