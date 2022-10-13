namespace AppBusiness.Data.DTOs
{
    public class ParamMasterPostingPersonaliaDto
    {
        public int? IdPdam { get; set; }
        public int? IdPosting { get; set; }
        public int? KodePeriode { get; set; }
        public int? IdUser { get; set; }
        public bool? FlagKunci { get; set; } = false;
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterPostingPersonaliaFilterDto : ParamMasterPostingPersonaliaDto
    {
    }
}
