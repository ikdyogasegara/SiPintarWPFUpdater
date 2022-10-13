using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterBlokDto
    {
        public int? IdPdam { get; set; }
        public int? IdBlok { get; set; }
        public string KodeBlok { get; set; }
        public string NamaBlok { get; set; }
        public int? IdRayon { get; set; }
        public string KodeRayon { get; set; }
        public string NamaRayon { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
