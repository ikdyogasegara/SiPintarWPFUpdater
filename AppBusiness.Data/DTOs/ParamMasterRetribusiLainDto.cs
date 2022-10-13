using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterRetribusiLainDto
    {
        public int? IdPdam { get; set; }
        public int? IdRetribusiLain { get; set; }
        public string KodeRetribusiLain { get; set; }
        public string NamaRetribusiLain { get; set; }
        public decimal? Retribusi { get; set; } = 0;
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterRetribusiLainFilterDto : ParamMasterRetribusiLainDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
