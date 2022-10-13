using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterPeruntukanDto
    {
        public int? IdPdam { get; set; }
        public int? IdPeruntukan { get; set; }
        public string NamaPeruntukan { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterPeruntukanFilterDto : ParamMasterPeruntukanDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
