using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterPerkiraan1Dto
    {
        public int? IdPdam { get; set; }
        public int? IdPerkiraan1 { get; set; }
        public string KodePerkiraan1 { get; set; }
        public string NamaPerkiraan1 { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }

    }

    public class ParamMasterPerkiraan1FilterDto : ParamMasterPerkiraan1Dto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
