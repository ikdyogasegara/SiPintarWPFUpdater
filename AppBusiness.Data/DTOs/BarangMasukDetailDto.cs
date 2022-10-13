using System;

namespace AppBusiness.Data.DTOs
{
    public class BarangMasukDetailDto
    {
        public int? IdPdam { get; set; }
        public int? IdBarangMasuk { get; set; }
        public int? IdBarang { get; set; }
        public string KodeBarang { get; set; }
        public string NamaBarang { get; set; }
        public string SatuanBarang { get; set; }
        public int? IdGudang { get; set; }
        public string KodeGudang { get; set; }
        public string NamaGudang { get; set; }
        public int? IdKategoriBarangMasuk { get; set; }
        public string Kategori { get; set; }
        public decimal? Ppn { get; set; }
        public decimal? Qty { get; set; }
        public decimal? Qty_Stock { get; set; }
        public decimal? Harga { get; set; }
        public decimal? Jumlah { get; set; }
        public decimal? Harga_Tanpa_Ppn { get; set; }
        public decimal? Jumlah_Tanpa_Ppn { get; set; }
        public DateTime? WaktuDiterima { get; set; }
        public bool? FlagHibah { get; set; }
        public DateTime? WaktuTransaksi { get; set; }
        public string NomorTransaksi { get; set; }
        public string NomorPengajuanPembelian { get; set; }
        public int? IdUser { get; set; }
        public string NamaUser { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
