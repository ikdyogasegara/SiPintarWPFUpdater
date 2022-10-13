using System;

namespace AppBusiness.Data.DTOs
{
    public class PermohonanPelangganAirDetailDto
    {
        public string Parameter { get; set; }
        public string TipeData { get; set; }
        public dynamic Value { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
