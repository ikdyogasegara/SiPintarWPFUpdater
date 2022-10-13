using System;

namespace AppBusiness.Data.DTOs
{
    public class PengaturanDto
    {
        public int? ID { get; set; }
        public int? IdPdam { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public bool? FlagHapus { get; set; }
        public DateTime? WaktuUpdate { get; set; }
        public int? IdUserRequest { get; set; }
    }
}
