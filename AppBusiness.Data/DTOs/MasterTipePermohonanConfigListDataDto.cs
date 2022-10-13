using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterTipePermohonanConfigListDataDto
    {
        public int? IdListData { get; set; }
        public string EndPoint { get; set; }
        public string FieldKey { get; set; }
        public string FieldDisplayValue1 { get; set; }
        public string FieldDisplayName1 { get; set; }
        public string FieldDisplayValue2 { get; set; }
        public string FieldDisplayName2 { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
