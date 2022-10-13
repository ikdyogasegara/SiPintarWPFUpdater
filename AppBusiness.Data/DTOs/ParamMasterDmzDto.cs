using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterDmzDto
    {
        public int? IdPdam { get; set; }
        public int? IdDmz { get; set; }
        public string KodeDmz { get; set; }
        public string NamaDmz { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterDmzFilterDto : ParamMasterDmzDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
