using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterPegawaiGajiHarianDto
    {
        public int? IdPdam { get; set; }
        public int? IdPegawai { get; set; }
        public int? IdGajiHarian { get; set; }
        public string NoPegawai { get; set; }
        public string NamaPegawai { get; set; }
        public decimal? Jumlah { get; set; }
        public string Tipe { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
