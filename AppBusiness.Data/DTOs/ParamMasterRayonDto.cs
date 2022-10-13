using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterRayonDto
    {
        public int? IdPdam { get; set; }
        public int? IdRayon { get; set; }
        public string KodeRayon { get; set; }
        public string NamaRayon { get; set; }
        public int? IdArea { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterRayonFilterDto : ParamMasterRayonDto
    {
        public DateTime? WaktuUpdate { get; set; }

    }
}
