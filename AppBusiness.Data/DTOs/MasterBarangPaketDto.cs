using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterBarangPaketDto
    {
        public int? IdPdam { get; set; }
        public int? IdPaketBarang { get; set; }
        public string NamaPaketBarang { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }


    public class MasterBarangPaketDetailDto
    {
        public int? IdPdam { get; set; }
        public int? IdPaketBarang { get; set; }
        public string NamaPaketBarang { get; set; }
        public int? IdBarang { get; set; }
        public string KodeBarang { get; set; }
        public string NamaBarang { get; set; }
        public int? IdSatuanBarang { get; set; }
        public string SatuanBarang { get; set; }
        public decimal? Qty { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
