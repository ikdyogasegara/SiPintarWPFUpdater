namespace AppBusiness.Data.DTOs
{
    public class ParamMasterStatusPegawaiDto
    {
        public int? IdPdam { get; set; }
        public int? IdStatusPegawai { get; set; }
        public string StatusPegawai { get; set; }
        public bool? FlagProsesTipeKeluarga { get; set; } = false;
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterStatusPegawaiFilterDto : ParamMasterStatusPegawaiDto
    {
    }
}
