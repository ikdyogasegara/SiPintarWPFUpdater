using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterKeperluanDto
    {
        public int? IdPdam { get; set; }
        public int? IdKeperluan { get; set; }
        public string KodeKeperluan { get; set; }
        public string Keperluan { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
