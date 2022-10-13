using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterDmaDto
    {
        public int? IdPdam { get; set; }
        public int? IdDma { get; set; }
        public string KodeDma { get; set; }
        public string NamaDma { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
