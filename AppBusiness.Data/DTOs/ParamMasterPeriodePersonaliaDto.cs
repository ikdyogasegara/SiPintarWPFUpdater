namespace AppBusiness.Data.DTOs
{
    public class ParamMasterPeriodePersonaliaDto
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public int? IdPeriode { get; set; }
        public int? KodePeriode { get; set; }
        public int? IdKodeGaji { get; set; }
        public bool? Status { get; set; } = true;
        public bool? FlagHapus { get; set; } = false;
    }

    public class ParamMasterPeriodePersonaliaFilterDto : ParamMasterPeriodePersonaliaDto
    {
        public int? Tahun { get; set; }
    }
}
