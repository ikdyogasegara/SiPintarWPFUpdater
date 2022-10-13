using System;

namespace SiPintar.Helpers.BacameterV4
{
    public class HasilBaca
    {
        public string IdPelanggan { get; set; }
        public decimal? StanSkrg { get; set; }
        public decimal? StanLalu { get; set; }
        public decimal? StanAngkat { get; set; }
        public decimal? PakaiSkrg { get; set; }
        public string Kelainan { get; set; }
        public DateTime? WaktuBaca { get; set; }
        public bool? FlagBaca { get; set; }
        public string PetugasBaca { get; set; }
        public bool? Taksasi { get; set; }
        public DateTime? WaktuVerifikasi { get; set; }
        public bool? Taksir { get; set; }

        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public bool? AdaFotoRumah { get; set; }
        public bool? AdaVideo { get; set; }
        public string Memo { get; set; }

    }
}
