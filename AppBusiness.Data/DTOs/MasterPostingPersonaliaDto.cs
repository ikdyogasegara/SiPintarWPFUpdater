using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterPostingPersonaliaDto
    {
        public int? IdPdam { get; set; }
        public int? IdPosting { get; set; }
        public int? KodePeriode { get; set; }
        public string NamaPeriode { get; set; }
        public DateTime? WaktuPosting { get; set; }
        public int? IdUser { get; set; }
        public string NamaUser { get; set; }
        public bool? FlagKunci { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
