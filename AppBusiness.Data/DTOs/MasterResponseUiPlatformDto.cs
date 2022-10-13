using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterResponseUiPlatformDto
    {
        public int? IdPlatform { get; set; }
        public string NamaPlatform { get; set; }
        public bool? FlagHapus { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
