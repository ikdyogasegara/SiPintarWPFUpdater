using System.Collections.ObjectModel;

namespace AppBusiness.Data.DTOs
{
    public class ReportModelSourceDto
    {
        public int IdPdam { get; set; }
        public int IdModel { get; set; }
        public int IdSource { get; set; }
        public string Name { get; set; }
        public string Query { get; set; }
        public string Saved { get; set; }

        public ObservableCollection<ReportModelPropDto> Props { get; set; } = new ObservableCollection<ReportModelPropDto>();
    }
}
