using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBusiness.Data.DTOs
{
    public class EkuitasMasterDto
    {
        public int? IdPdam { get; set; }
        public int? IdEkuitasMaster { get; set; }
        public string KodeEkuitasMaster { get; set; }
        public string Uraian { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public DateTime? WaktuUpdate { get; set; }
    }
}
