using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterJenisBangunanDto
    {
        public int? IdPdam { get; set; }
        public int? IdJenisBangunan { get; set; }
        public string NamaJenisBangunan { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterJenisBangunanFilterDto : ParamMasterJenisBangunanDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
