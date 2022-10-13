using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBusiness.Data.DTOs
{
    public class LaporanPerputaranUangMasterDto
    {
        public int? IdPdam { get; set; }
        public int? IdPerputaranUangMaster { get; set; }
        public string Header { get; set; }
        public int? IdKelompok { get; set; }
        public string Uraian { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public DateTime? WaktuUpdate { get; set; }
    }
}
