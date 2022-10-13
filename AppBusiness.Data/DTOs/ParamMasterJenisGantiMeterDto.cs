using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterJenisGantiMeterDto
    {
        public int? IdPdam { get; set; }
        public int? IdJenisGantiMeter { get; set; }
        public string JenisGantiMeter { get; set; }
        public string Kategori { get; set; }
        public int? IdJenisNonAir { get; set; }
        public int? IdWarnaGantiMeter { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterJenisGantiMeterFilterDto : ParamMasterJenisGantiMeterDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
