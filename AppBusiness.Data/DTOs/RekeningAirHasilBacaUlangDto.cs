using System;

namespace AppBusiness.Data.DTOs
{
    public class RekeningAirHasilBacaUlangDto
    {
        public int? IdPdam { get; set; }
        public int? IdPelangganAir { get; set; }
        public int? IdPeriode { get; set; }
        public DateTime? WaktuBaca { get; set; }
        public string PetugasBaca { get; set; }
        public string Kelainan { get; set; }
        public string Lampiran { get; set; }
        public decimal? StanLalu { get; set; } = 0;
        public decimal? StanSkrg { get; set; } = 0;
        public decimal? StanAngkat { get; set; } = 0;
        public decimal? Pakai { get; set; } = 0;
        public bool? Taksasi { get; set; } = false;
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int? IdUserRequest { get; set; }
    }
}
