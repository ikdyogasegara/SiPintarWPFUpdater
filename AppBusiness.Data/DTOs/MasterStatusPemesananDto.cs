using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterStatusPemesananDto
    {
        public int? IdPdam { get; set; }
        public int? IdStatusPemesanan { get; set; }
        public string StatusPemesanan { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
