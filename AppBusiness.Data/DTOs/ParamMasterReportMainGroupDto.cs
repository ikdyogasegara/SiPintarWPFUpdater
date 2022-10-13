namespace AppBusiness.Data.DTOs
{
    public class ParamMasterReportMainGroupDto : MasterReportMainGroupDto
    {
        public int? IdUserRequest { get; set; }
        public int? PageSize { get; set; } = 10;
        public int? CurrentPage { get; set; } = 1;
    }
}
