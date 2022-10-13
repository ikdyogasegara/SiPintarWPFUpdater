using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBusiness.Data.DTOs
{
    public class MasterInPelayananNonAirDto
    {
        public int? IdPdam { get; set; }
        public int? IdInPelayananNonAir { get; set; }
        public int? IdPerkiraan3 { get; set; }
        public string KodePerkiraan3 { get; set; }
        public string NamaPerkiraan3 { get; set; }
        public string Keterangan { get; set; }
        public int? IdWilayah { get; set; }
        public string KodeWilayah { get; set; }
        public string NamaWilayah { get; set; }
        public int? IdGolongan { get; set; }
        public string KodeGolongan { get; set; }
        public string NamaGolongan { get; set; }
        public int? IdJenisNonAir { get; set; }
        public string KodeJenisNonAir { get; set; }
        public string NamaJenisNonAir { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public DateTime? WaktuUpdate { get; set; }
    }
}
