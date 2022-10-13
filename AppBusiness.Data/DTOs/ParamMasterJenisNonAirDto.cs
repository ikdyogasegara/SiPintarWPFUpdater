using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterJenisNonAirDto
    {
        public int? IdPdam { get; set; }
        public int? IdJenisNonAir { get; set; }
        public string KodeJenisNonAir { get; set; }
        public string NamaJenisNonAir { get; set; }
        public string KodeSurat { get; set; }
        public bool? Status { get; set; } = true;
        public decimal? PersentasePpn { get; set; } = 0;
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
        public List<ParamMasterJenisNonAirDetailDto> DetailBiaya { get; set; }
    }

    public class ParamMasterJenisNonAirDetailDto
    {
        public string Parameter { get; set; }
        public string PostBiaya { get; set; }
        public decimal? Biaya { get; set; }
        public bool? IsLocked { get; set; } = true;
    }

    public class ParamMasterJenisNonAirFilterDto : ParamMasterJenisNonAirDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
