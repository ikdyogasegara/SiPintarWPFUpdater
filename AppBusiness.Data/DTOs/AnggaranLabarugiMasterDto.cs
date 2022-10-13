using System;
using System.Collections.Generic;
using System.Text;

namespace AppBusiness.Data.DTOs
{
    public class AnggaranLabaRugiMasterDto
    {
        public int? IdPdam { get; set; }
        public int? IdGroupLabaRugi { get; set; }
        public string Header { get; set; }
        public string Footer { get; set; }
        public string Uraian { get; set; }
        public bool? FlagHapus { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
