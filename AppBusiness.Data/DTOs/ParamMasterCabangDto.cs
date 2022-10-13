using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterCabangDto
    {
        public int? IdPdam { get; set; }
        public int? IdCabang { get; set; }
        public string KodeCabang { get; set; }
        public string NamaCabang { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterCabangFilterDto : ParamMasterCabangDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
