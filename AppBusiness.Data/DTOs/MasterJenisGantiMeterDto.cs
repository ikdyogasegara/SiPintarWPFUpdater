using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterJenisGantiMeterDto
    {
        public int? IdPdam { get; set; }
        public int? IdJenisGantiMeter { get; set; }
        public string JenisGantiMeter { get; set; }
        public string Kategori { get; set; }
        public int? IdJenisNonAir { get; set; }
        public string KodeJenisNonAir { get; set; }
        public int? IdWarnaGantiMeter { get; set; }
        public string KodeWarnaGantiMeter { get; set; }
        public string WarnaGantiMeter { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
