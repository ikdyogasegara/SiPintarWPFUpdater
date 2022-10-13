using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterLoketDto
    {
        public int? IdPdam { get; set; }
        public int? IdLoket { get; set; }
        public string KodeLoket { get; set; }
        public string NamaLoket { get; set; }
        public int? IdWilayah { get; set; }
        public bool? FlagMitra { get; set; } = false;
        public bool? Status { get; set; } = false;
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterLoketFilterDto : ParamMasterLoketDto
    {
        public string KodeWilayah { get; set; }
        public string NamaWilayah { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
