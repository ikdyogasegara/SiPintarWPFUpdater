using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamPerbaruiDataRekeningLlttDto
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public int? IdPeriode { get; set; }
        public int? KodePeriode { get; set; }
        public List<int?> IdPelangganLltt { get; set; }
    }
}
