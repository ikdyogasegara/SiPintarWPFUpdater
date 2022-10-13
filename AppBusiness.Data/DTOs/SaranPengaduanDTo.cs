using System;

namespace AppBusiness.Data.DTOs
{
    public class SaranPengaduanDto
    {
        public int? IdSaranPengaduan { get; set; }
        public int? IdPdam { get; set; }
        public int? IdModule { get; set; }
        public string NamaPetugas { get; set; }
        public string Bagian { get; set; }
        public string NamaPdam { get; set; }
        public string NoHp { get; set; }
        public string Email { get; set; }
        public int? Rating { get; set; }
        public string Komentar { get; set; }
        public bool FlagHapus { get; set; }
        public DateTime? WaktuCreate { get; set; }
        public DateTime? WaktuUpdate { get; set; }
        public int? IdUserRequest { get; set; }
    }
}
