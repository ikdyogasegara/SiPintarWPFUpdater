using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterKategoriBarangMasukDto
    {
        public int? IdPdam { get; set; }
        public int? IdKategoriBarangMasuk { get; set; }
        public string Kategori { get; set; }
        public decimal? Ppn { get; set; }
        public bool? Flag { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
