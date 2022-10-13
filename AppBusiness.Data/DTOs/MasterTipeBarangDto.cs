using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterTipeBarangDto
    {
        public int? IdPdam { get; set; }
        public int? IdTipeBarang { get; set; }
        public string NamaTipeBarang { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
