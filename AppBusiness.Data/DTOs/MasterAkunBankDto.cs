using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterAkunBankDto
    {
        public int? IdPdam { get; set; }
        [Obsolete("This property is obsolete. Use IdParameter instead.", false)]
        public int? IdAkunBank { get; set; }
        public int? IdParameter { get; set; }
        [Obsolete("This property is obsolete. Use KodeParameter instead.", false)]
        public string KodeAkunBank { get; set; }
        public string KodeParameter { get; set; }
        [Obsolete("This property is obsolete. Use NamaParameter instead.", false)]
        public string NamaAkunBank { get; set; }
        public string NamaParameter { get; set; }
        public string KodePerkiraan { get; set; }
        public string FlagBank { get; set; }
        public bool? FlagVc { get; set; }
        public bool? FlagNeraca { get; set; }
        public bool? FlagDhhd { get; set; }
        public bool? FlagJbk { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public DateTime? WaktuUpdate { get; set; }
    }
}
