using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterGolonganPegawaiDto
    {
        public int? IdPdam { get; set; }
        public int? IdGolonganPegawai { get; set; }
        public string KodeGolonganPegawai { get; set; }
        public string GolonganPegawai { get; set; }
        public int? Urutan { get; set; }
        public int? Tingkat { get; set; }
        public decimal? Tunjangan { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
