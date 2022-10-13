using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterLisensiDto
    {
        public int? IdLisensi { get; set; }
        public int? IdPdam { get; set; }
        public string NamaPdam { get; set; }
        public string Aplikasi { get; set; }
        public int? IdPegawai { get; set; }
        public string NamaPegawai { get; set; }
        public string KodeDevice { get; set; }
        public string KodeLisensi { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
