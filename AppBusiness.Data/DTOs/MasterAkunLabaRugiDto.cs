using System;
using System.Collections.Generic;
using System.Text;

namespace AppBusiness.Data.DTOs
{
    public class MasterAkunLabaRugiDto
    {
        public int? IdPdam { get; set; }
        public int? IdLabaRugi { get; set; }
        public string KodeLabaRugi { get; set; }
        public string NamaLabaRugi { get; set; }
        public string kodeJenis { get; set; }
        public DateTime? WaktuUpdate { get; set; }
        public bool? FlagHapus { get; set; }
    }
}
