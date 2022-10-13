namespace AppBusiness.Data.DTOs
{
    public class ParamMasterGolonganPegawaiDto
    {
        public int? IdPdam { get; set; }
        public int? IdGolonganPegawai { get; set; }
        public string KodeGolonganPegawai { get; set; }
        public string GolonganPegawai { get; set; }
        public int? Urutan { get; set; } = 0;
        public int? Tingkat { get; set; } = 0;
        public decimal? Tunjangan { get; set; } = 0;
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterGolonganPegawaiFilterDto : ParamMasterGolonganPegawaiDto
    {
    }
}
