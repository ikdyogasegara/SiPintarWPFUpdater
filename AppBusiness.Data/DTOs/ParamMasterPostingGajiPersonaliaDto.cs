namespace AppBusiness.Data.DTOs
{
    public class ParamMasterPostingGajiPersonaliaDto
    {
        public int? IdPdam { get; set; }
        public int? KodePeriode { get; set; }
        public int? IdKodeGaji { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterPostingGajiPersonaliaFilterDto : ParamMasterPostingGajiPersonaliaDto
    {
    }
}
