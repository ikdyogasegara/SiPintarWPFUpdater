using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterDasarAstekDto
    {
        public int? IdPdam { get; set; }
        public int? IdDasarAstek { get; set; }
        public int? IdPegawai { get; set; }
        public string NoPegawai { get; set; }
        public string NamaPegawai { get; set; }
        public decimal? Jumlah { get; set; } = 0;
        public DateTime? WaktuUpdate { get; set; }
    }
}
