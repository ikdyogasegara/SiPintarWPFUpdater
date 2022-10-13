using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterSumberAirDto
    {
        public int? IdPdam { get; set; }
        public int? IdSumberAir { get; set; }
        public string KodeSumberAir { get; set; }
        public string NamaSumberAir { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
