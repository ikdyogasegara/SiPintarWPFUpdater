using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterHarianKasDto
    {
        public int? IdPdam { get; set; }
        public int? IdLhkMaster { get; set; }
        public string KodeLhk { get; set; }
        public string Uraian1 { get; set; }
        public string Uraian2 { get; set; }
        public string Uraian3 { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public DateTime? WaktuUpdate { get; set; }
    }
}
