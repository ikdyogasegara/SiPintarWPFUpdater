using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterPegawaiGajiKontrakDto
    {
        public int? IdPdam { get; set; }
        public int? IdPegawai { get; set; }
        public int? IdGajiKontrak { get; set; }
        public string NoPegawai { get; set; }
        public string NamaPegawai { get; set; }
        public decimal? Jumlah { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
