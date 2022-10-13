using System;

namespace AppBusiness.Data.DTOs
{
    public class SaranPerbaikanDto
    {
        public int? IdSaranPengaduan { get; set; }
        public int? IdPertanyaan { get; set; }
        public DateTime? WaktuCreate { get; set; }
        public bool FlagPerluPerbaikan { get; set; }
        public DateTime? WaktuUpdate { get; set; }
        public int? IdUserRequest { get; set; }
    }
}
