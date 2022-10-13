using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamVerifikasiHapusSecaraAkuntansiRekeningAirDto
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public List<int?> IdRekeningAir { get; set; }
        public DateTime? TglHapusSecaraAkuntansi { get; set; }
        public string PasswordUser { get; set; }
    }
}
