using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterSetPotonganBulananDto
    {
        public int? IdPdam { get; set; }
        public int? IdPotonganBulanan { get; set; }
        public int? IdKodeGaji { get; set; }
        public string KodeGaji { get; set; }
        public string NamaKodeGaji { get; set; }
        public int? KodePeriode { get; set; }
        public int? IdJenisPotongan { get; set; }
        public string KodeJenisPotongan { get; set; }
        public string NamaJenisPotongan { get; set; }
        public string Beban { get; set; }
        public bool? FlagAngsuran { get; set; }
        public int? AngsuranKe { get; set; }
        public bool? FlagAbsensi { get; set; }
        public int? IdUpahKehadiran { get; set; }
        public string KodeUpahKehadiran { get; set; }
        public string UpahKehadiran { get; set; }
        public bool? FlagPersen { get; set; }
        public string PersenDari { get; set; }
        public decimal? Nominal { get; set; }
        public decimal? NominalMin { get; set; }
        public decimal? NominalMax { get; set; }
        public bool? FlagPotongPph { get; set; }
        public bool? FlagPersenPph { get; set; }
        public decimal? NominalPph { get; set; }
        public string Keterangan { get; set; }
        public bool? FlagSemuaPegawai { get; set; }
        public int? IdAgama { get; set; }
        public string Agama { get; set; }
        public int? IdAreaKerja { get; set; }
        public string KodeAreaKerja { get; set; }
        public string AreaKerja { get; set; }
        public int? IdDepartemen { get; set; }
        public string KodeDepartemen { get; set; }
        public string Departemen { get; set; }
        public int? IdDivisi { get; set; }
        public string Divisi { get; set; }
        public int? IdGolonganPegawai { get; set; }
        public string KodeGolonganPegawai { get; set; }
        public string GolonganPegawai { get; set; }
        public int? IdJabatan { get; set; }
        public string Jabatan { get; set; }
        public int? IdJenisKelamin { get; set; }
        public string JenisKelamin { get; set; }
        public int? IdPendidikan { get; set; }
        public string Pendidikan { get; set; }
        public int? IdTipeKeluarga { get; set; }
        public string KodeTipeKeluarga { get; set; }
        public int? IdStatusPegawai { get; set; }
        public string StatusPegawai { get; set; }
        public int? IdPegawai { get; set; }
        public string NoPegawai { get; set; }
        public string NamaPegawai { get; set; }
        public int? Mkg { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
