using System;

namespace AppBusiness.Data.DTOs
{
    public class DistribusiBarangStockDetailDto
    {
        public int? IdPdam { get; set; }
        public int? IdBarangMasuk { get; set; }
        public int? IdBarang { get; set; }
        public string KodeBarang { get; set; }
        public string NamaBarang { get; set; }
        public string SatuanBarang { get; set; }
        public decimal? Qty_Stock { get; set; }
        public decimal? Qty_Draft_Distribusi { get; set; }
        public decimal? Qty_Draft_Sisa { get; set; }
        public DateTime? WaktuDiterima { get; set; }
        public DateTime? WaktuTransaksi { get; set; }
        public string NomorTransaksi { get; set; }
        public string NomorPengajuanPembelian { get; set; }
        public int? IdGudang { get; set; }
        public string KodeGudang { get; set; }
        public string NamaGudang { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }

    public class DistribusiBarangDraftDto
    {
        public int? IdPdam { get; set; }
        public int? IdBarangMasuk { get; set; }
        public int? IdBarang { get; set; }
        public string KodeBarang { get; set; }
        public string NamaBarang { get; set; }
        public decimal? Qty { get; set; }
    }
}
