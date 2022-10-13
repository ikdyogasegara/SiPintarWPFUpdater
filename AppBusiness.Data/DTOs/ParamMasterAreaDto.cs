using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterAreaDto
    {
        public int? IdPdam { get; set; }
        public int? IdArea { get; set; }
        public string KodeArea { get; set; }
        public string NamaArea { get; set; }
        public int? IdWilayah { get; set; }
        public string KodeWilayah { get; set; }
        public string NamaWilayah { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterAreaFilterDto : ParamMasterAreaDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
