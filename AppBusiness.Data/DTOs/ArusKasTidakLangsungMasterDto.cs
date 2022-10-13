using System;

namespace AppBusiness.Data.DTOs
{
    public class ArusKasTidakLangsungMasterDto
    {
        public int? IdPdam { get; set; }
        public int? IdArusKasMaster { get; set; }
        public string KodeArusKas { get; set; }
        public string NoArusKas { get; set; }
        public string HeadArusKas { get; set; }
        public string DetailHeadArusKas { get; set; }
        public string FooterArusKas { get; set; }
        public string DetailFooterArusKas { get; set; }
        public string Uraian { get; set; }
        public bool? FlagHapus { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
