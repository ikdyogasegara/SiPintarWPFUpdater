using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterCabangDto
    {
        public int? IdPdam { get; set; }
        public int? IdCabang { get; set; }
        public string KodeCabang { get; set; }
        public string NamaCabang { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
