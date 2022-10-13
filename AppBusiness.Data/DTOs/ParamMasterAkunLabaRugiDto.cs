using System;
using System.Collections.Generic;
using System.Text;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterAkunLabaRugiDto
    {
        public int? IdPdam { get; set; }
        public int? IdLabaRugi { get; set; }
        public string KodeLabaRugi { get; set; }
        public string NamaLabaRugi { get; set; }
        public string kodeJenis { get; set; }
        public bool? FlagHapus { get; set; } = false;
    }
    public class ParamMasterAkunLabaRugiFilterDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
