using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterPerkiraan2Dto
    {
        public int? IdPdam { get; set; }
        public int? IdPerkiraan2 { get; set; }
        public string KodePerkiraan2 { get; set; }
        public string NamaPerkiraan2 { get; set; }
        public int? IdPerkiraan1 { get; set; }
        public int? IdNeracaMaster { get; set; }
        public int? IdArusKasMaster { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterPerkiraan2FilterDto : ParamMasterPerkiraan2Dto
    {
        public string KodePerkiraan1 { get; set; }
        public string NamaPerkiraan1 { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
