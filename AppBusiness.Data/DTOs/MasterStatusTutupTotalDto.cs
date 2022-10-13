using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterStatusTutupTotalDto
    {
        public int? IdPdam { get; set; }
        public int? IdStatusTutupTotal { get; set; }
        public string StatusTutupTotal { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
