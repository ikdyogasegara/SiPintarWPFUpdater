using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterWarnaSegelDto
    {
        public int? IdPdam { get; set; }
        public int? IdWarnaSegel { get; set; }
        public string WarnaSegel { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
