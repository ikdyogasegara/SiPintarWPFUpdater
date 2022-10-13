namespace AppBusiness.Data.DTOs
{
    public class ParamMasterReportSubGroupDto : MasterReportSubGroupDto
    {
        public int? IdUserRequest { get; set; }
        public int? PageSize { get; set; } = 100000;
        public int? CurrentPage { get; set; } = 1;
    }
}
