using System.Collections.ObjectModel;

namespace AppBusiness.Data.DTOs
{
    public class ParamFilterReportModelDto
    {
        public int? IdPdam { get; set; }
        public int? IdModel { get; set; }
        public int? IdModule { get; set; }
        public int? IdMainGroup { get; set; }
        public int? IdSubGroup { get; set; }
        public bool? ListOnly { get; set; }
        public string Name { get; set; }
        public int? PageSize { get; set; }
        public int? CurrentPage { get; set; }
    }

    public class ParamReportModelDto : ParamFilterReportModelDto
    {
        public string LabelKey { get; set; }
        public ObservableCollection<ParamReportModelSourceDto> Sources { get; set; } = new ObservableCollection<ParamReportModelSourceDto>();
        public ObservableCollection<ParamReportModelParamDto> Params { get; set; } = new ObservableCollection<ParamReportModelParamDto>();
        public ObservableCollection<ParamReportModelSortDto> Sorts { get; set; } = new ObservableCollection<ParamReportModelSortDto>();
    }

    public class ParamReportModelSourceDto
    {
        public int? IdPdam { get; set; }
        public int? IdModel { get; set; }
        public int? IdSource { get; set; }
        public string Name { get; set; }
        public string Query { get; set; }
        public string Saved { get; set; }
        public ObservableCollection<ParamReportModelPropertyDto> Props { get; set; } = new ObservableCollection<ParamReportModelPropertyDto>();
    }

    public class ParamReportModelPropertyDto
    {
        public int? IdPdam { get; set; }
        public int? IdModel { get; set; }
        public int? IdSource { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class ParamReportModelParamDto
    {
        public int? IdPdam { get; set; }
        public int? IdModel { get; set; }
        public string Name { get; set; }
        public string ParamType { get; set; }
        public string DataType { get; set; }
        public int? IdListData { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public string Query { get; set; }
        public bool Required { get; set; }
    }

    public class ParamReportModelSortDto
    {
        public int? IdPdam { get; set; }
        public int? IdModel { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
