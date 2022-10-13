using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterAlasanTutupDto
    {
        public int? IdPdam { get; set; }
        public int? IdAlasanTutup { get; set; }
        public string AlasanTutup { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterAlasanTutupFilterDto : ParamMasterAlasanTutupDto
    {
        public DateTime? WaktuUpdate { get; set; }

    }
}
