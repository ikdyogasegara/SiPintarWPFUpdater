using System;
using System.Collections.Generic;
using System.Text;

namespace AppBusiness.Data.DTOs
{
    public class ReportModelParamDto
    {
        public int IdPdam { get; set; }
        public int IdModel { get; set; }
        public int IdParam { get; set; }
        public string Name { get; set; }
        public string DataType { get; set; }
        public string ParamType { get; set; }
        public int? IdListData { get; set; }
        public int? IdListCustomData { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public string Query { get; set; }
        public bool Required { get; set; }

        public MasterTipePermohonanConfigListDataDto ListDataDetail { get; set; }
        public ReportFilterCustomDto ListCustomDataDetail { get; set; }
    }
}
