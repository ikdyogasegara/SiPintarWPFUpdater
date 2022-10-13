using System;

namespace AppBusiness.Data.DTOs
{
    public class StatusKeluargaPegawaiDto
    {
        public int? IdPdam { get; set; }
        public int? IdPegawai { get; set; }
        public string NoPegawai { get; set; }
        public string NamaPegawai { get; set; }
        public string Alamat { get; set; }
        public bool? FlagKawin { get; set; }
        public DateTime? TglKawin { get; set; }
        public int? IdTipeKeluarga { get; set; }
        public string KodeTipeKeluarga { get; set; }
        public int? JumlahAnak { get; set; } = 0;
        public decimal? Ptkp { get; set; } = 0;
        public int? IdStatusPegawai { get; set; }
        public string StatusPegawai { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
