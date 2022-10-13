using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AppBusiness.Data.DTOs
{
    public class CetakDto
    {
        public ObservableCollection<CetakDataDto> Data { get; set; }
        public ObservableCollection<KeyValuePair<string, Type>> ParamStructure { get; set; }
        public dynamic Param { get; set; }
        public int TotalPage { get; set; } = 1;
        public int PerBatch { get; set; } = 1000;
    }

    public class CetakDataDto
    {
        public string Nama { get; set; }
        public ObservableCollection<KeyValuePair<string, Type>> DataStructure { get; set; }
        public dynamic Data { get; set; }
    }
}
