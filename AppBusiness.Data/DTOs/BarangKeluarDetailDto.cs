using System;

namespace AppBusiness.Data.DTOs
{
    public class BarangKeluarDetailDto
    {
        public int? IdDetailBarangKeluar { get; set; }
        public int? IdPdam { get; set; }
        public int? IdBarangKeluar { get; set; }
        public int? IdBarang { get; set; }
        public string KodeBarang { get; set; }
        public string NamaBarang { get; set; }
        public string SatuanBarang { get; set; }
        public int? IdGudang { get; set; }
        public string KodeGudang { get; set; }
        public string NamaGudang { get; set; }
        public decimal? Qty { get; set; }
        public decimal? Harga_Jual { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? WaktuDikeluarkan { get; set; }
        public int? IdKeperluan { get; set; }
        public string KodeKeperluan { get; set; }
        public string Keperluan { get; set; }
        public bool? FlagVerifikasi { get; set; }
        public DateTime? WaktuTransaksi { get; set; }
        public string NomorTransaksi { get; set; }
        public string NomorPengajuanPengeluaran { get; set; }
        public int? IdUser { get; set; }
        public string NamaUser { get; set; }
        public int? IdKategoriBarangKeluar { get; set; }
        public string Kategori { get; set; }
        public int? IdBagianMemintaBarang { get; set; }
        public string NamaBagianMemintaBarang { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
