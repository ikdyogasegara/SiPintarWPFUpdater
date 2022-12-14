using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterDasarAskesDto
    {
        public int? IdPdam { get; set; }
        public int? IdDasarAskes { get; set; }
        public int? IdPegawai { get; set; }
        public string NoPegawai { get; set; }
        public string NamaPegawai { get; set; }
        public decimal? Jumlah { get; set; } = 0;
        public DateTime? WaktuUpdate { get; set; }
    }
}
