using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterAdministrasiLainDto
    {
        public int? IdPdam { get; set; }
        public int? IdAdministrasiLain { get; set; }
        public string KodeAdministrasiLain { get; set; }
        public string NamaAdministrasiLain { get; set; }
        public decimal? Administrasi { get; set; } = 0;
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterAdministrasiLainFilterDto : ParamMasterAdministrasiLainDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
