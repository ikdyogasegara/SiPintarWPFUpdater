using System;

namespace AppBusiness.Data.DTOs
{
    public class RekeningNonAirDetailDto
    {
        public string Parameter { get; set; }
        public string PostBiaya { get; set; }
        public decimal? Value { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
