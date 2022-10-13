using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterSatuanBarangDto
    {
        public int? IdPdam { get; set; }
        public int? IdSatuanBarang { get; set; }
        public string SatuanBarang { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
