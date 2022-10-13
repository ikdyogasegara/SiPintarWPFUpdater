using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterKondisiMeterDto
    {
        public int? IdPdam { get; set; }
        public int? IdKondisiMeter { get; set; }
        public string KodeKondisiMeter { get; set; }
        public string NamaKondisiMeter { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
