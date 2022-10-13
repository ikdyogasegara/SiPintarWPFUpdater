using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterPeriodePersonaliaDto
    {
        public int? IdPdam { get; set; }
        public int? IdPeriode { get; set; }
        public int? Tahun { get; set; }
        public int? KodePeriode { get; set; }
        public string NamaPeriode { get; set; }
        public int? IdKodeGaji { get; set; }
        public string KodeGaji { get; set; }
        public string Deskripsi { get; set; }
        public bool? Status { get; set; } = false;
        public DateTime? WaktuUpdate { get; set; }
    }
}
