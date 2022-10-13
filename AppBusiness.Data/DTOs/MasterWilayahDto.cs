using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterWilayahDto
    {
        public int? IdPdam { get; set; }
        public int? IdWilayah { get; set; }
        public string KodeWilayah { get; set; }
        public string NamaWilayah { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
