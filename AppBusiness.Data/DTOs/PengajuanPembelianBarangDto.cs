using System;

namespace AppBusiness.Data.DTOs
{
    public class PengajuanPembelianBarangDto
    {
        public int? IdPdam { get; set; }
        public int? IdPengajuanPembelian { get; set; }
        public string NomorPengajuanPembelian { get; set; }
        public int? IdGudang { get; set; }
        public string KodeGudang { get; set; }
        public string NamaGudang { get; set; }
        public DateTime? TglPengajuan { get; set; }
        public string DiGunakanUntuk { get; set; }
        public int? IdUser { get; set; }
        public string NamaUser { get; set; }
        public bool? FlagVerifikasi { get; set; }
        public DateTime? Tgl_Voucher { get; set; }
        public string Order_Pembelian_Nomor { get; set; }
        public DateTime? Order_Pembelian_Tgl_Laporan { get; set; }
        public int? Order_Pembelian_IdSuplier { get; set; }
        public string Order_Pembelian_Suplier_NamaContactPerson { get; set; }
        public string Order_Pembelian_Suplier_NamaPerusahaan { get; set; }
        public int? Order_Pembelian_Rating_Suplier { get; set; }
        public string Surat_Perjanjian_Nomor { get; set; }
        public DateTime? Surat_Perjanjian_Tgl_Laporan { get; set; }
        public int? Surat_Perjanjian_IdSuplier { get; set; }
        public string Surat_Perjanjian_Suplier_NamaContactPerson { get; set; }
        public string Surat_Perjanjian_Suplier_NamaPerusahaan { get; set; }
        public int? Surat_Perjanjian_Rating_Suplier { get; set; }
        public string Surat_Perjanjian_Berita_Acara { get; set; }
        public bool? FlagSelesai { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
