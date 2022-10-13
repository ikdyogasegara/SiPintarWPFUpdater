using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterKategoriBarangKeluarDto
    {
        public int? IdPdam { get; set; }
        public int? IdKategoriBarangKeluar { get; set; }
        public string Kategori { get; set; }
        public string KodeNomor { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
