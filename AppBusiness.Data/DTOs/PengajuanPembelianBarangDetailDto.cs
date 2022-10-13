using System;

namespace AppBusiness.Data.DTOs
{
    public class PengajuanPembelianBarangDetailDto
    {
        public int? IdPdam { get; set; }
        public int? IdPengajuanPembelian { get; set; }
        public string NomorPengajuanPembelian { get; set; }
        public int? IdKategoriBarangMasuk { get; set; }
        public string Kategori { get; set; }
        public decimal? Ppn { get; set; }
        public int? IdBarang { get; set; }
        public string KodeBarang { get; set; }
        public string NamaBarang { get; set; }
        public string SatuanBarang { get; set; }
        public decimal? Qty { get; set; }
        public decimal? Qty_Sisa_Yang_Diajukan { get; set; }
        public decimal? Stock { get; set; }
        public decimal? Harga { get; set; }
        public decimal? Jumlah { get; set; }
        public decimal? Harga_Tanpa_Ppn { get; set; }
        public decimal? Jumlah_Tanpa_Ppn { get; set; }
        public bool? FlagTentukanHarga { get; set; }
        public bool? FlagVerifikasi { get; set; }
        public DateTime? TglPengajuan { get; set; }
        public int? IdUser { get; set; }
        public string NamaUser { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
