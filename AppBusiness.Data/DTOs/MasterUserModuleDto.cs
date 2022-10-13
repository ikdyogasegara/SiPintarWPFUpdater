using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterUserModuleDto
    {
        public int? IdModule { get; set; }
        public string NamaModule { get; set; }
        public string Tipe { get; set; } = "basic";
        public bool? FlagHapus { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
