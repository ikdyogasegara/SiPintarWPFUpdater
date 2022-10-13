using System;

namespace AppBusiness.Data.DTOs
{
    public class RiwayatPemakaianAirDto
    {
        public int? IdPdam { get; set; }
        public int? IdPelangganAir { get; set; }
        public string NoSamb { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public int? IdPeriode { get; set; }
        public int? TahunPeriode { get; set; }
        public int? KodePeriode { get; set; }
        public string NamaPeriode { get; set; }
        public DateTime? WaktuBaca { get; set; }
        public string PetugasBaca { get; set; }
        public string Kelainan { get; set; }
        public decimal? StanLalu { get; set; }
        public decimal? StanSkrg { get; set; }
        public decimal? StanAngkat { get; set; }
        public decimal? Pakai { get; set; }
        public decimal? BiayaPemakaian { get; set; }
        public int? IdUserRequest { get; set; }
    }
}
