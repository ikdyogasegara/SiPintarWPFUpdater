using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamHitungUlangRekeningAirDto
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public int? IdPeriode { get; set; }
        public List<int?> IdPelangganAir { get; set; }
        public DateTime? WaktuKoreksi { get; set; }
    }
}
