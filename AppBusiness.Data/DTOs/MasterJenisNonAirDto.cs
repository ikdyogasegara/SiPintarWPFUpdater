using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class MasterJenisNonAirDto
    {
        public int? IdPdam { get; set; }
        public int? IdJenisNonAir { get; set; }
        public string KodeJenisNonAir { get; set; }
        public string NamaJenisNonAir { get; set; }
        public string KodeSurat { get; set; }
        public bool? Status { get; set; } = false;
        public decimal? PersentasePpn { get; set; }
        public List<MasterJenisNonAirDetailDto> DetailBiaya { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }

    public class MasterJenisNonAirDetailDto
    {
        public string Parameter { get; set; }
        public string PostBiaya { get; set; }
        public decimal? Biaya { get; set; }
        public bool? IsLocked { get; set; } = true;
    }
}
