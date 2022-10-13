using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterMeteraiDto
    {
        public int ID { get; set; }
        public int? IdPdam { get; set; }
        public int? KodePeriodeMulaiBerlaku { get; set; }
        public string NamaPeriodeMulaiBerlaku { get; set; }
        public virtual string NamaPeriodeAkhir { get; set; }

        public decimal? Batas1 { get; set; } = 0;
        public decimal? Batas2 { get; set; } = 0;
        public decimal? Batas3 { get; set; } = 0;
        public decimal? Meterai1 { get; set; } = 0;
        public decimal? Meterai2 { get; set; } = 0;
        public decimal? Meterai3 { get; set; } = 0;
        public DateTime? WaktuUpdate { get; set; }
    }
}
