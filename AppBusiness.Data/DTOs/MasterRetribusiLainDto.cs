using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterRetribusiLainDto
    {
        public int? IdPdam { get; set; }
        public int? IdRetribusiLain { get; set; }
        public string KodeRetribusiLain { get; set; }
        public string NamaRetribusiLain { get; set; }
        public decimal? Retribusi { get; set; } = 0;
        public DateTime? WaktuUpdate { get; set; }
    }
}
