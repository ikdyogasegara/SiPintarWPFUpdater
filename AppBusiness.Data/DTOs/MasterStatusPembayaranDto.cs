using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterStatusPembayaranDto
    {
        public int? IdPdam { get; set; }
        public int? IdStatusPembayaran { get; set; }
        public string StatusPembayaran { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
