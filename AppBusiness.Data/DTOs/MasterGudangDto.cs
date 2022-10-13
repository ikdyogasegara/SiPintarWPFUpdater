using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterGudangDto
    {
        public int? IdPdam { get; set; }
        public int? IdGudang { get; set; }
        public string KodeGudang { get; set; }
        public string NamaGudang { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
