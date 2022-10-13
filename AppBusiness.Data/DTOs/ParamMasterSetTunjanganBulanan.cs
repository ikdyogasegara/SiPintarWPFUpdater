namespace AppBusiness.Data.DTOs
{
    public class ParamMasterSetTunjanganBulananDto
    {
        public int? IdPdam { get; set; }
        public int? IdTunjanganBulanan { get; set; }
        public int? IdKodeGaji { get; set; }
        public int? KodePeriode { get; set; }
        public int? IdJenisTunjangan { get; set; }
        public string Beban { get; set; }
        public bool? FlagAbsensi { get; set; } = false;
        public int? IdUpahKehadiran { get; set; }
        public bool? FlagPersen { get; set; } = false;
        public string PersenDari { get; set; }
        public decimal? Nominal { get; set; } = 0;
        public decimal? NominalMin { get; set; } = 0;
        public decimal? NominalMax { get; set; } = 0;
        public bool? FlagPotongPph { get; set; } = false;
        public bool? FlagPersenPph { get; set; } = false;
        public decimal? NominalPph { get; set; } = 0;
        public string Keterangan { get; set; }
        public bool? FlagSemuaPegawai { get; set; } = false;
        public int? IdAgama { get; set; }
        public int? IdAreaKerja { get; set; }
        public int? IdDepartemen { get; set; }
        public int? IdDivisi { get; set; }
        public int? IdGolonganPegawai { get; set; }
        public int? IdJabatan { get; set; }
        public int? IdJenisKelamin { get; set; }
        public int? IdPendidikan { get; set; }
        public int? IdTipeKeluarga { get; set; }
        public int? IdStatusPegawai { get; set; }
        public int? IdPegawai { get; set; }
        public int? Mkg { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterSetTunjanganBulananFilterDto : ParamMasterSetTunjanganBulananDto
    {
        public string NoPegawai { get; set; }
        public string NamaPegawai { get; set; }
    }

    public class ParamMasterSetTunjanganBulananSalinDataDto
    {
        public int? IdPdam { get; set; }
        public int? KodePeriodeAsal { get; set; }
        public int? IdKodeGajiAsal { get; set; }
        public int? IdJenisTunjanganAsal { get; set; }
        public int? KodePeriodeTujuan { get; set; }
        public int? IdKodeGajiTujuan { get; set; }
        public int? IdUserRequest { get; set; }
    }
}
