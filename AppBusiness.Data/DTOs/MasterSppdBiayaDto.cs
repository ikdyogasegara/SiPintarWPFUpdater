using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterSppdBiayaDto
    {
        public int? IdPdam { get; set; }
        public int? IdBiayaSppd { get; set; }
        public string Deskripsi { get; set; }
        public decimal? Biaya { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
