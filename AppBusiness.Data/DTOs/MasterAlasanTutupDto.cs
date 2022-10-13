using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterAlasanTutupDto
    {
        public int? IdPdam { get; set; }
        public int? IdAlasanTutup { get; set; }
        public string AlasanTutup { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
