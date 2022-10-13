using System;
using System.Collections.Generic;
using System.Text;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterInPelayananNonAirDto
    {
        public int? IdPdam { get; set; }
        public int? IdInPelayananNonAir { get; set; }
        public int? IdPerkiraan3 { get; set; }
        public string Keterangan { get; set; }
        public int? IdWilayah { get; set; }
        public int? IdGolongan { get; set; }
        public int? IdJenisNonAir { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }


    public class ParamMasterInPelayananNonAirFilterDto : ParamMasterInPelayananNonAirDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }

}
