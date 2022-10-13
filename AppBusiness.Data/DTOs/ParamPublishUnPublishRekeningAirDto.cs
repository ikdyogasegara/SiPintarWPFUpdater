using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamPublishUnPublishRekeningAirDto
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public int? IdPeriode { get; set; }
        public List<int?> IdPelangganAir { get; set; }
        public string PasswordPublishSemua { get; set; }
        public string PasswordUser { get; set; }
        public DateTime? WaktuPublish { get; set; }
        public bool? LockVerifikasiBacameter { get; set; } = true;
    }
}
