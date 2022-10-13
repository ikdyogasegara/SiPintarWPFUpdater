using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterLoketDto
    {
        public int? IdPdam { get; set; }
        public int? IdLoket { get; set; }
        public string KodeLoket { get; set; }
        public string NamaLoket { get; set; }
        public int? IdWilayah { get; set; }
        public string KodeWilayah { get; set; }
        public string NamaWilayah { get; set; }
        public bool? FlagMitra { get; set; }
        public bool? Status { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
