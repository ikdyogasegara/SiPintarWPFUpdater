using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamRequestBacaUlangRekeningAirDto
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public int? IdPeriode { get; set; }
        public List<int?> IdPelangganAir { get; set; }
        public string PasswordRequestBacaUlangSemua { get; set; }
        public string PasswordUser { get; set; }
    }
}
