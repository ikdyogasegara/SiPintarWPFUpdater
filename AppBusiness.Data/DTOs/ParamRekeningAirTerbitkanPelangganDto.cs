﻿using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamRekeningAirTerbitkanPelangganDto
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public int? KodePeriode { get; set; }
        public List<int?> IdPelangganAir { get; set; }
    }
}
