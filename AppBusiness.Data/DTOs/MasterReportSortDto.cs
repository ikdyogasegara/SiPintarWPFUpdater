using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterReportSortDto
    {
        public int? IdReportSort { get; set; }
        public int? IdReport { get; set; }
        public int? IdPdam { get; set; }
        public string Nama { get; set; }
        public string Value { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
