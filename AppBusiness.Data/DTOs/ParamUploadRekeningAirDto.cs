using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamUploadRekeningAirDto
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public int? IdPeriode { get; set; }
        public int? IdPelangganAir { get; set; }
        public DateTime? WaktuUpload { get; set; }
    }
}
