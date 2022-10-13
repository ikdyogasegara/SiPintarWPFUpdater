using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamSetHasilBacaDto
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public int? IdPeriode { get; set; }
        public List<ListRekeningAir> ListHasilBaca { get; set; }
    }

    public class ListRekeningAir
    {
        public int? IdPelangganAir { get; set; }
        public int? MetodeBaca { get; set; }
        public DateTime? WaktuBaca { get; set; }
        public string PetugasBaca { get; set; }
        public string Kelainan { get; set; }
        public string Lampiran { get; set; }
        public decimal? StanLalu { get; set; } = 0;
        public decimal? StanSkrg { get; set; } = 0;
        public decimal? StanAngkat { get; set; } = 0;
        public decimal? Pakai { get; set; } = 0;
        public bool? Taksasi { get; set; } = false;
        public bool? Taksir { get; set; } = false;
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public bool? AdaFotoMeter { get; set; } = false;
        public bool? AdaFotoRumah { get; set; } = false;
        public bool? AdaVideo { get; set; } = false;
    }
}
