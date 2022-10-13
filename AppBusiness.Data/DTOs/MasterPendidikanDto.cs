using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterPendidikanDto
    {
        public int? IdPdam { get; set; }
        public int? IdPendidikan { get; set; }
        public string Pendidikan { get; set; }
        public int? Urutan { get; set; }
        public int? IdGolonganPegawaiMax { get; set; }
        public string KodeGolonganPegawaiMax { get; set; }
        public string GolonganPegawaiMax { get; set; }
        public int? GolMaxIndek { get; set; }
        public int? IdGolonganPegawaiMin { get; set; }
        public string KodeGolonganPegawaiMin { get; set; }
        public string GolonganPegawaiMin { get; set; }
        public int? GolMinIndek { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
