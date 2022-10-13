using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterReportSubGroupDto
    {
        public int? IdPdam { get; set; }
        public int? IdReportSubGroup { get; set; }
        public int? IdReportMainGroup { get; set; }
        public string NamaMainGroup { get; set; }
        public string Nama { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public DateTime? WaktuUpdate { get; set; }
    }
}
