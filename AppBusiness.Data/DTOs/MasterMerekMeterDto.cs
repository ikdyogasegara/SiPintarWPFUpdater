using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterMerekMeterDto
    {
        public int? IdPdam { get; set; }
        public int? IdMerekMeter { get; set; }
        public string KodeMerekMeter { get; set; }
        public string NamaMerekMeter { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
