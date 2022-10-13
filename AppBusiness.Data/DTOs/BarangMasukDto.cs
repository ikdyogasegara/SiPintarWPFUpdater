using System;

namespace AppBusiness.Data.DTOs
{
    public class BarangMasukDto
    {
        public int? IdPdam { get; set; }
        public int? IdBarangMasuk { get; set; }
        public DateTime? WaktuTransaksi { get; set; }
        public string NomorTransaksi { get; set; }
        public string NomorPengajuanPembelian { get; set; }
        public string NomorSuratJalan { get; set; }
        public string NomorLpb { get; set; }
        public int? IdPeriode { get; set; }
        public int? KodePeriode { get; set; }
        public string NamaPeriode { get; set; }
        public int? IdPeriode_Voucher { get; set; }
        public int? KodePeriode_Voucher { get; set; }
        public string NamaPeriode_Voucher { get; set; }
        public int? IdUser { get; set; }
        public string NamaUser { get; set; }
        public int? IdGudang { get; set; }
        public string KodeGudang { get; set; }
        public string NamaGudang { get; set; }
        public string DiGunakanUntuk { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? TotalAmount_Tanpa_Ppn { get; set; }
        public DateTime? Tgl_Voucher { get; set; }
        public string Order_Pembelian_Nomor { get; set; }
        public DateTime? Order_Pembelian_Tgl_Laporan { get; set; }
        public int? Order_Pembelian_IdSuplier { get; set; }
        public int? Order_Pembelian_Rating_Suplier { get; set; }
        public string Surat_Perjanjian_Nomor { get; set; }
        public DateTime? Surat_Perjanjian_Tgl_Laporan { get; set; }
        public int? Surat_Perjanjian_IdSuplier { get; set; }
        public int? Surat_Perjanjian_Rating_Suplier { get; set; }
        public string Surat_Perjanjian_Berita_Acara { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
