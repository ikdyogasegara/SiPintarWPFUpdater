using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterTarifLimbahDto
    {
        public int? IdPdam { get; set; }
        public int? IdTarifLimbah { get; set; }
        public string KodeTarifLimbah { get; set; }
        public string NamaTarifLimbah { get; set; }
        public int? PeriodeMulaiBerlaku { get; set; }
        public string NomorSk { get; set; }
        public decimal? Biaya { get; set; } = 0;
        public bool? Status { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterTarifLimbahFilterDto : ParamMasterTarifLimbahDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
