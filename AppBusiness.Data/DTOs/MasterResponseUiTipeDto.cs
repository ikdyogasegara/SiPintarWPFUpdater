using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterResponseUiTipeDto
    {
        public int? IdTipe { get; set; }
        public string NamaTipe { get; set; }
        public bool? FlagHapus { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
