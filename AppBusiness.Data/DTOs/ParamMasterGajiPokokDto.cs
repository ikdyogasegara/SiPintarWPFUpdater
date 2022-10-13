namespace AppBusiness.Data.DTOs
{
    public class ParamMasterGajiPokokDto
    {
        public int? IdPdam { get; set; }
        public int? IdGajiPokok { get; set; }
        public decimal? Gaji { get; set; } = 0;
        public int? IdGolonganPegawai { get; set; }
        public int? MasaKerja { get; set; } = 0;
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterGajiPokokFilterDto : ParamMasterGajiPokokDto
    {
        public string KodeGolonganPegawai { get; set; }
        public string GolonganPegawai { get; set; }
    }
}
