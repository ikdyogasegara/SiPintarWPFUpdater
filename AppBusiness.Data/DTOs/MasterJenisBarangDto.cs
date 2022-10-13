using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterJenisBarangDto
    {
        public int? IdPdam { get; set; }
        public int? IdJenisBarang { get; set; }
        public string KodeJenisBarang { get; set; }
        public string NamaJenisBarang { get; set; }
        public int? IdTipeBarang { get; set; }
        public string NamaTipeBarang { get; set; }
        public bool? FlagHapus { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
