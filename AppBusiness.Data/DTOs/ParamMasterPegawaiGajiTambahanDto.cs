namespace AppBusiness.Data.DTOs
{
    public class ParamMasterPegawaiGajiTambahanDto
    {
        public int? IdPdam { get; set; }
        public int? IdGajiTambahan { get; set; }
        public int? IdPegawai { get; set; }
        public string Keterangan { get; set; }
        public bool? FlagPersen { get; set; } = false;
        public decimal? Nominal { get; set; } = 0;
        public int? IdKodeGaji { get; set; }
        public int? KodePeriode { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterPegawaiGajiTambahanFilterDto : ParamMasterPegawaiGajiTambahanDto
    {
        public string NoPegawai { get; set; }
        public string NamaPegawai { get; set; }
        public int? IdGolonganPegawai { get; set; }
        public int? IdAgama { get; set; }
        public int? IdAreaKerja { get; set; }
        public int? IdDepartemen { get; set; }
        public int? IdDivisi { get; set; }
        public int? IdJabatan { get; set; }
        public int? IdJenisKelamin { get; set; }
        public bool? FlagKawin { get; set; }
        public int? IdPendidikan { get; set; }
        public int? IdTipeKeluarga { get; set; }
    }


    public class ParamMasterPegawaiGajiTambahanAmbilDariPeriodeDto
    {
        public int? IdPdam { get; set; }
        public int? IdKodeGajiAsal { get; set; }
        public int? KodePeriodeAsal { get; set; }
        public int? IdKodeGaji { get; set; }
        public int? KodePeriode { get; set; }
        public int? IdUserRequest { get; set; }
    }
}
