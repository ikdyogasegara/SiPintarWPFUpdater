using System;

namespace AppBusiness.Data.DTOs
{
    public class BarangKeluarDto
    {
        public int? IdPdam { get; set; }
        public int? IdBarangKeluar { get; set; }
        public DateTime? WaktuTransaksi { get; set; }
        public string NomorTransaksi { get; set; }
        public string NomorPengajuanPengeluaran { get; set; }
        public int? IdPeriode { get; set; }
        public int? KodePeriode { get; set; }
        public string NamaPeriode { get; set; }
        public int? IdUser { get; set; }
        public string NamaUser { get; set; }
        public int? IdGudang { get; set; }
        public string KodeGudang { get; set; }
        public string NamaGudang { get; set; }
        public int? IdKategoriBarangKeluar { get; set; }
        public string Kategori { get; set; }
        public int? IdBagianMemintaBarang { get; set; }
        public string NamaBagianMemintaBarang { get; set; }
        public int? IdDivisiMemintaBarang { get; set; }
        public string DivisiMemintaBarang { get; set; }
        public string DiGunakanUntuk { get; set; }
        public decimal? TotalAmount { get; set; }
        public int? FlagVerifikasi_Selesai { get; set; }
        public string VerifikasiText { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
