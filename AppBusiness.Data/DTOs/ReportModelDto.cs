using System.Collections.ObjectModel;

namespace AppBusiness.Data.DTOs
{
    public class ReportModelDto
    {
        public int IdPdam { get; set; }
        public int IdModel { get; set; }
        public int IdModule { get; set; }
        public int IdMainGroup { get; set; }
        public int IdSubGroup { get; set; }
        public string NamaPdam { get; set; }
        public string Name { get; set; }
        public string Module { get; set; }
        public string MainGroup { get; set; }
        public string SubGroup { get; set; }
        public string LabelKey { get; set; }
        public ObservableCollection<ReportModelSourceDto> Sources { get; set; }
        public ObservableCollection<ReportModelParamDto> Params { get; set; }
        public ObservableCollection<ReportModelSortDto> Sorts { get; set; }
    }
}
