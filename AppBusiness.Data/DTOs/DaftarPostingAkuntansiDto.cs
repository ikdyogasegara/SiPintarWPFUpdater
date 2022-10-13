using System;
using System.Collections.Generic;
using System.Text;

namespace AppBusiness.Data.DTOs
{
    public class DaftarPostingAkuntansiDto
    {
        public int? IdPdam { get; set; }
        public int? IdPeriode { get; set; }
        public int? KodePeriode { get; set; }
        public string NamaPeriode { get; set; }
        public bool? StatusPosting { get; set; } = false;
        public DateTime? WaktuPosting { get; set; }
        public bool? StatusProses { get; set; } = false;
        public DateTime? WaktuProses { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
