using System;

namespace AppBusiness.Data.DTOs
{
    public class PengajuanPengeluaranBarangDto
    {
        public int? IdPdam { get; set; }
        public int? IdPengajuanPengeluaran { get; set; }
        public string NomorPengajuanPengeluaran { get; set; }
        public string NomorRegistrasi { get; set; }
        public int? IdGudang { get; set; }
        public string KodeGudang { get; set; }
        public string NamaGudang { get; set; }
        public int? IdBagianMemintaBarang { get; set; }
        public string NamaBagianMemintaBarang { get; set; }
        public int? IdKategoriBarangKeluar { get; set; }
        public string Kategori { get; set; }
        public DateTime? TglPengajuan { get; set; }
        public string DiGunakanUntuk { get; set; }
        public int? IdUser { get; set; }
        public string NamaUser { get; set; }
        public bool? FlagSelesai { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }

    public class CetakPengajuanPengeluaranBarangDto
    {
        public string NomorPengajuanPengeluaran { get; set; }
        public string NomorRegistrasi { get; set; }
        public string KodeGudang { get; set; }
        public string NamaGudang { get; set; }
        public string NamaBagianMemintaBarang { get; set; }
        public string Kategori { get; set; }
        public DateTime TglPengajuan { get; set; } = DateTime.Now;
        public string DiGunakanUntuk { get; set; }
        public string NamaUser { get; set; }
    }
}
