using System;
using System.Collections.Generic;
using System.Text;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterInPelayananAirDto
    {
        public int? IdPdam { get; set; }
        public int? IdInPelayananAir { get; set; }
        public int? IdWilayah { get; set; }
        public int? IdGolongan { get; set; }
        public int? IdPerkiraan3Kredit { get; set; }
        public int? IdPerkiraan3Debet { get; set; }
        public bool? FlagPembentukRekair { get; set; }
        public string Keterangan { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }


    public class ParamMasterInPelayananFilterDto : ParamMasterInPelayananAirDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
