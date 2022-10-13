using System;

namespace AppBusiness.Data.DTOs
{
    public class NeracaMasterDto
    {
        public int? IdPdam { get; set; }
        public int? IdNeracaMaster { get; set; }
        public int? KodeNeraca { get; set; }
        public string Uraian1 { get; set; }
        public string Uraian2 { get; set; }
        public string Uraian3 { get; set; }
        public bool? FlagAktiva { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
