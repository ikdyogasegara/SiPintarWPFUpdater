using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AppBusiness.Data.DTOs
{
    public class MasterReportDto
    {
        public int? IdPdam { get; set; }
        public int? IdReport { get; set; }
        public string Name { get; set; }
        public int? IdReportMainGroup { get; set; }
        public int? IdReportSubGroup { get; set; }
        public string KeyLabel { get; set; }
        public List<ReportModelParamDto> Params { get; set; } = new List<ReportModelParamDto>();
        public List<ReportModelSortDto> Sorts { get; set; } = new List<ReportModelSortDto>();
        public List<string> Fields { get; set; } = new List<string>();

        public ObservableCollection<KeyValuePair<string, string>> ContohDataField { get; set; } =
            new ObservableCollection<KeyValuePair<string, string>>();
    }
}
