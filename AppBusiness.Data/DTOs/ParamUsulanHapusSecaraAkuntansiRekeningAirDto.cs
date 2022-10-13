using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamUsulanHapusSecaraAkuntansiRekeningAirDto
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public int? IdPelangganAir { get; set; }
        public List<int?> IdPeriode { get; set; }
    }
}
