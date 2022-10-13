namespace AppBusiness.Data.DTOs
{
    public class MasterReportParameterDto
    {
        public int? IdParameter { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string DataType { get; set; }
        public string ParamType { get; set; }
        public int? IdListData { get; set; }
        public bool? Required { get; set; }
        public MasterTipePermohonanConfigListDataDto ListDataDetail { get; set; }
    }
}
