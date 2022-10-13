namespace AppBusiness.Data.DTOs
{
    public class ParamMasterPostingLaporanOpnameBarangDto
    {
        public int? IdPdam { get; set; }
        public int? IdPeriode { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterPostingLaporanOpnameBarangFilterDto : ParamMasterPostingLaporanOpnameBarangDto
    {
    }
}
