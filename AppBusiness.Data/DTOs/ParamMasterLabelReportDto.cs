namespace AppBusiness.Data.DTOs
{
    public class ParamMasterLabelReportDto
    {
        public int? IdPdam { get; set; }
        public string Key { get; set; }
    }

    public class ParamMasterLabelReportFilterDto : ParamMasterLabelReportDto
    {
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
