using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterStatusPegawaiDto
    {
        public int? IdPdam { get; set; }
        public int? IdStatusPegawai { get; set; }
        public string StatusPegawai { get; set; }
        public bool? FlagProsesTipeKeluarga { get; set; } = false;
        public DateTime? WaktuUpdate { get; set; }
    }
}
