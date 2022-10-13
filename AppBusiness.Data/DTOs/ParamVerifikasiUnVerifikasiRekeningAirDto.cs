using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamVerifikasiUnVerifikasiRekeningAirDto
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public int? IdPeriode { get; set; }
        public List<int?> IdPelangganAir { get; set; }
        public string PasswordVerifikasiSemua { get; set; }
        public string PasswordUser { get; set; }
        public DateTime? WaktuVerifikasi { get; set; }

    }
}
