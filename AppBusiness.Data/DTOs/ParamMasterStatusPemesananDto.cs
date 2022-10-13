namespace AppBusiness.Data.DTOs
{
    public class ParamMasterStatusPemesananDto
    {
        public int? IdPdam { get; set; }
        public int? IdStatusPemesanan { get; set; }
        public string StatusPemesanan { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterStatusPemesananFilterDto : ParamMasterStatusPemesananDto
    {
    }
}
