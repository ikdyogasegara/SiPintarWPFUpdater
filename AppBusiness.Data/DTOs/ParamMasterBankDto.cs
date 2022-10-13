using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterBankDto
    {
        public int? IdPdam { get; set; }
        public int? IdBank { get; set; }
        public string NamaBank { get; set; }
        public string NoRekening { get; set; }
        public string Keterangan { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterBankFilterDto : ParamMasterBankDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
