using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterJenisPotonganDto
    {
        public int? IdPdam { get; set; }
        public int? IdJenisPotongan { get; set; }
        public string KodeJenisPotongan { get; set; }
        public string NamaJenisPotongan { get; set; }
        public int? Urutan { get; set; } = 0;
        public string Tipe { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
