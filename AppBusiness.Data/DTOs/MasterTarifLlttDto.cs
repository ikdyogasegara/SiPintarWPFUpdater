using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterTarifLlttDto : ICloneable
    {
        public int? IdPdam { get; set; }
        public int? IdTarifLltt { get; set; }
        public string KodeTarifLltt { get; set; }
        public string NamaTarifLltt { get; set; }
        public int? PeriodeMulaiBerlaku { get; set; }
        public string NamaPeriodeMulaiBerlaku { get; set; }
        public string NomorSk { get; set; }
        public decimal? Biaya { get; set; } = 0;
        public bool? Status { get; set; } = false;
        public DateTime? WaktuUpdate { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
