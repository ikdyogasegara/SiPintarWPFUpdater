using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamRekeningLimbahTerbitkanPelangganDto
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public int? IdPeriode { get; set; }
        public List<int?> IdPelangganLimbah { get; set; }
    }
}
