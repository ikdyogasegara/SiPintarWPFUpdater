namespace AppBusiness.Data.DTOs
{
    public class ParamMasterPegawaiMkgDto
    {
        public int? IdPdam { get; set; }
        public int? IdPegawai { get; set; }
        public int? Mkg_Thn { get; set; } = 0;
        public int? Mkg_Bln { get; set; } = 0;
        public int? MkgBerkala_Thn { get; set; } = 0;
        public int? MkgBerkala_Bln { get; set; } = 0;
        public int? MkgPangkat_Thn { get; set; } = 0;
        public int? MkgPangkat_Bln { get; set; } = 0;
        public int? Real_Thn { get; set; } = 0;
        public int? Real_Bln { get; set; } = 0;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterPegawaiMkgFilterDto : ParamMasterPegawaiMkgDto
    {
        public string NoPegawai { get; set; }
        public string NamaPegawai { get; set; }
        public int? IdStatusPegawai { get; set; }
        public int? IdAgama { get; set; }
        public int? IdAreaKerja { get; set; }
        public int? IdDepartemen { get; set; }
        public int? IdDivisi { get; set; }
        public int? IdJabatan { get; set; }
        public int? IdJenisKelamin { get; set; }
        public bool? FlagKawin { get; set; }
        public int? IdPendidikan { get; set; }
        public int? IdGolonganPegawai { get; set; }
    }
}
