using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterWilayahDto
    {
        public int? IdPdam { get; set; }
        public int? IdWilayah { get; set; }
        public string KodeWilayah { get; set; }
        public string NamaWilayah { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterWilayahFilterDto : ParamMasterWilayahDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
