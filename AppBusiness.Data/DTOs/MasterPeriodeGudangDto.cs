using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterPeriodeGudangDto
    {
        public int? IdPdam { get; set; }
        public int? IdPeriode { get; set; }
        public int? Tahun { get; set; }
        public int? KodePeriode { get; set; }
        public string NamaPeriode { get; set; }
        public bool? Status { get; set; } = false;
        public DateTime? WaktuUpdate { get; set; }
    }
}
