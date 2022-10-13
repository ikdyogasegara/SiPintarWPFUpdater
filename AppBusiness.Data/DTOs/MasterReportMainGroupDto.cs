using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class MasterReportMainGroupDto
    {
        public int? IdReportMainGroup { get; set; }
        public int? IdPdam { get; set; }
        public string Nama { get; set; }
        public string NamaPdam { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public DateTime? WaktuUpdate { get; set; }

        public List<MasterReportSubGroupDto> SubGroup { get; set; } = new List<MasterReportSubGroupDto>();
    }
}
