using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamPublishUnPublishRekeningLlttDto
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public int? IdPeriode { get; set; }
        public List<int?> IdPelangganLltt { get; set; }
        public string PasswordUser { get; set; }
    }
}
