using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterPekerjaanDto
    {
        public int? IdPdam { get; set; }
        public int? IdPekerjaan { get; set; }
        public string NamaPekerjaan { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
