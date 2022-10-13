using System;
using System.Collections.Generic;
using System.Text;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterParameterAkuntansiDto
    {
        public int? IdPdam { get; set; }
        public int? IdParameter { get; set; }
        public int? IdJenisParameter { get; set; }
        public int? IdPerkiraan { get; set; }
        public int? IdPerkiraanAktiva { get; set; }
        public string KodeParameter { get; set; }

        public string Keterangan { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }
    public class ParamMasterParameterAkuntansiFilterDto : ParamMasterParameterAkuntansiDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
