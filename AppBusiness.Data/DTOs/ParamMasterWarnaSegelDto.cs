namespace AppBusiness.Data.DTOs
{
    public class ParamMasterWarnaSegelDto
    {
        public int? IdPdam { get; set; }
        public int? IdWarnaSegel { get; set; }
        public string WarnaSegel { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterWarnaSegelFilterDto : ParamMasterWarnaSegelDto
    {
    }
}
