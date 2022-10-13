using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterPegawaiGajiCapegDto
    {
        public int? IdPdam { get; set; }
        public int? IdPegawai { get; set; }
        public string NoPegawai { get; set; }
        public string NamaPegawai { get; set; }
        public string KodeGolonganPegawai { get; set; }
        public string Jabatan { get; set; }
        public string Departemen { get; set; }
        public string Divisi { get; set; }
        public string AreaKerja { get; set; }
        public int? Mkg_Thn { get; set; }
        public int? Mkg_Bln { get; set; }
        public decimal? Gaji { get; set; }
        public decimal? Persentase { get; set; }
        public decimal? Jumlah { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
