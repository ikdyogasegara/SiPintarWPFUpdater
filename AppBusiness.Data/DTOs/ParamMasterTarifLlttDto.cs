using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterTarifLlttDto
    {
        public int? IdPdam { get; set; }
        public int? IdTarifLltt { get; set; }
        public string KodeTarifLltt { get; set; }
        public string NamaTarifLltt { get; set; }
        public int? PeriodeMulaiBerlaku { get; set; }
        public string NomorSk { get; set; }
        public decimal? Biaya { get; set; } = 0;
        public bool? Status { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterTarifLlttFilterDto : ParamMasterTarifLlttDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
