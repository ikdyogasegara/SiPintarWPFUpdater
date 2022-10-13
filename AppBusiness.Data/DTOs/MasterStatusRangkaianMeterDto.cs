using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterStatusRangkaianMeterDto
    {
        public int? IdPdam { get; set; }
        public int? IdStatusRangkaianMeter { get; set; }
        public string StatusRangkaianMeter { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
