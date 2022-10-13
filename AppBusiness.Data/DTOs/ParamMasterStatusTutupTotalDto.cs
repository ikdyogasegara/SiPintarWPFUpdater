using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterStatusTutupTotalDto
    {
        public int? IdPdam { get; set; }
        public int? IdStatusTutupTotal { get; set; }
        public string StatusTutupTotal { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterStatusTutupTotalFilterDto : ParamMasterStatusTutupTotalDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
