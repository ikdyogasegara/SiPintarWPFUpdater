using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterStatusSegelDto
    {
        public int? IdPdam { get; set; }
        public int? IdStatusSegel { get; set; }
        public string StatusSegel { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
