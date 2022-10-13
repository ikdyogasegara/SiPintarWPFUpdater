using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterWarnaGantiMeterDto
    {
        public int? IdPdam { get; set; }
        public int? IdWarnaGantiMeter { get; set; }
        public string KodeWarnaGantiMeter { get; set; }
        public string WarnaGantiMeter { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
