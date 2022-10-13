using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterBlokDto
    {
        public int? IdPdam { get; set; }
        public int? IdBlok { get; set; }
        public string KodeBlok { get; set; }
        public string NamaBlok { get; set; }
        public int? IdRayon { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterBlokFilterDto : ParamMasterBlokDto
    {
        public string KodeRayon { get; set; }
        public string NamaRayon { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
