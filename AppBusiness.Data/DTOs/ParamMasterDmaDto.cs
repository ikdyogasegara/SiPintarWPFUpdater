using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterDmaDto
    {
        public int? IdPdam { get; set; }
        public int? IdDma { get; set; }
        public string KodeDma { get; set; }
        public string NamaDma { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterDmaFilterDto : ParamMasterDmaDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
