using System;
using System.Collections.Generic;
using System.Text;

namespace AppBusiness.Data.DTOs
{
    public class ParamDaftarPostingAkuntansiDto
    {
        public int? IdPdam { get; set; }
        public int? IdPeriode { get; set; }
        public int? KodePeriode { get; set; }
        public bool? StatusPosting { get; set; } = false;
        public DateTime? WaktuPosting { get; set; }
        public bool? StatusProses { get; set; } = false;
        public DateTime? WaktuProses { get; set; }
        public int? IdUserRequest { get; set; }
        public bool? FlagHapus { get; set; } = false;
    }

    public class ParamDaftarPostingAkuntansiFilterDto : ParamDaftarPostingAkuntansiDto
    {
        public DateTime? WaktuUpdate { get; set; }
        public string NamaPeriode { get; set; }
    }
}
