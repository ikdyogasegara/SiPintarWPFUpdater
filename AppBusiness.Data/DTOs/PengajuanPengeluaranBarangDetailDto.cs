using System;

namespace AppBusiness.Data.DTOs
{
    public class PengajuanPengeluaranBarangDetailDto
    {
        public int? IdPdam { get; set; }
        public int? IdPengajuanPengeluaran { get; set; }
        public string NomorPengajuanPengeluaran { get; set; }
        public string NomorRegistrasi { get; set; }
        public int? IdKategoriBarangKeluar { get; set; }
        public string Kategori { get; set; }
        public int? IdBarang { get; set; }
        public string KodeBarang { get; set; }
        public string NamaBarang { get; set; }
        public string SatuanBarang { get; set; }
        public decimal? Qty { get; set; }
        public decimal? Harga_Jual { get; set; }
        public decimal? Amount { get; set; }
        public string Keterangan { get; set; }
        public DateTime? TglPengajuan { get; set; }
        public int? IdUser { get; set; }
        public string NamaUser { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }

    public class CetakPengajuanPengeluaranBarangDetailDto
    {
        public string NomorPengajuanPengeluaran { get; set; }
        public string NomorRegistrasi { get; set; }
        public string Kategori { get; set; }
        public string KodeBarang { get; set; }
        public string NamaBarang { get; set; }
        public string SatuanBarang { get; set; }
        public decimal Qty { get; set; } = decimal.Zero;
        public decimal Harga_Jual { get; set; } = decimal.Zero;
        public decimal Amount { get; set; } = decimal.Zero;
        public string Keterangan { get; set; }
        public DateTime TglPengajuan { get; set; } = DateTime.Now;
        public string NamaUser { get; set; }
    }
}
