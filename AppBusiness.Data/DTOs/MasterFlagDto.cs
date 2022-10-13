using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterFlagDto
    {
        public int? IdPdam { get; set; }
        public int? IdFlag { get; set; }
        public string NamaFlag { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
