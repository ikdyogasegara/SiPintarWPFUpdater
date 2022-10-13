using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterPeruntukanDto
    {
        public int? IdPdam { get; set; }
        public int? IdPeruntukan { get; set; }
        public string NamaPeruntukan { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
