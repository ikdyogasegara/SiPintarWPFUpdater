using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterPekerjaanDto
    {
        public int? IdPdam { get; set; }
        public int? IdPekerjaan { get; set; }
        public string NamaPekerjaan { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterPekerjaanFilterDto : ParamMasterPekerjaanDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
