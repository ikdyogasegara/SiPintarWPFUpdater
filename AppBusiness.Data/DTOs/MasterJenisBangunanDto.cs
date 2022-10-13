using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterJenisBangunanDto
    {
        public int? IdPdam { get; set; }
        public int? IdJenisBangunan { get; set; }
        public string NamaJenisBangunan { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
