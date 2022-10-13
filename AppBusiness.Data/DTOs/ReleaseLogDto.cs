using System;

namespace AppBusiness.Data.DTOs
{
    public class ReleaseLogDto
    {
        public string ComputerId { get; set; }
        public string Versi { get; set; }
        public string Aplikasi { get; set; }
        public int? IdPdam { get; set; }
        public string NamaPdam { get; set; }
        public string Log { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
