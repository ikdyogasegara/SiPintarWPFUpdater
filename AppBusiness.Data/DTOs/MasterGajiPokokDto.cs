using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterGajiPokokDto
    {
        public int? IdPdam { get; set; }
        public int? IdGajiPokok { get; set; }
        public decimal? Gaji { get; set; } = 0;
        public int? IdGolonganPegawai { get; set; }
        public string KodeGolonganPegawai { get; set; }
        public string GolonganPegawai { get; set; }
        public int? MasaKerja { get; set; } = 0;
        public DateTime? WaktuUpdate { get; set; }
    }
}
