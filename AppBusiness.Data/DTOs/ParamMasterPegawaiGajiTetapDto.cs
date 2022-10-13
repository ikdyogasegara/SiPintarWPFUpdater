namespace AppBusiness.Data.DTOs
{
    public class ParamMasterPegawaiGajiTetapDto
    {
        public int? IdPdam { get; set; }
        public int? IdPegawai { get; set; }
        public decimal? Jumlah { get; set; } = 0;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterPegawaiGajiTetapPostingDto
    {
        public int? IdPdam { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterPegawaiGajiTetapFilterDto : ParamMasterPegawaiGajiTetapDto
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
    }
}
