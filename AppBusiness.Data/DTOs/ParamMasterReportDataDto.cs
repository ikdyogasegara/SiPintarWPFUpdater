using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterReportDataDto
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public int? IdModel { get; set; }
        public List<ParamMasterReportDataFilterDto> Filters { get; set; } = new List<ParamMasterReportDataFilterDto>();
        public int? IdSort { get; set; }
        public int? Page { get; set; } = 1;
        public int? PerBatch { get; set; } = 1000;
        public bool? Template { get; set; } = true;
    }

    public class ParamMasterReportDataFilterDto
    {
        public int? IdFilter { get; set; }
        public dynamic Value1 { get; set; }
        public dynamic Value2 { get; set; }
    }
}
