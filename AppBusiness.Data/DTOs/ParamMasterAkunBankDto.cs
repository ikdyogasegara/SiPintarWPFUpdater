using System;
using System.Collections.Generic;
using System.Text;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterAkunBankDto
    {
        public int? IdPdam { get; set; }
        public int? IdAkunBank { get; set; }
        public string KodeAkunBank { get; set; }
        public string NamaAkunBank { get; set; }
        public string KodePerkiraan { get; set; }
        public string FlagBank { get; set; }
        public bool? FlagVc { get; set; }
        public bool? FlagNeraca { get; set; }
        public bool? FlagDhhd { get; set; }
        public bool? FlagJbk { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }
    public class ParamMasterAkunBankFilterDto : ParamMasterAkunBankDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
