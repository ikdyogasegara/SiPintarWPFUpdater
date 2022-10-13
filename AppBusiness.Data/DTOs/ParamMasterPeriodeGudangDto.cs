namespace AppBusiness.Data.DTOs
{
    public class ParamMasterPeriodeGudangDto
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public int? IdPeriode { get; set; }
        public int? KodePeriode { get; set; }
        public string NamaPeriode { get; set; }
        public bool? Status { get; set; } = true;
        public bool? FlagHapus { get; set; } = false;
    }

    public class ParamMasterPeriodeGudangFilterDto : ParamMasterPeriodeGudangDto
    {
        public int? Tahun { get; set; }
    }
}
