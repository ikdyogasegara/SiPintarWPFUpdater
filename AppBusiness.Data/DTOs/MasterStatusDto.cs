using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterStatusDto
    {
        public int? IdPdam { get; set; }
        public int? IdStatus { get; set; }
        public string NamaStatus { get; set; }
        public bool? Rekening_Air_Include { get; set; } = false;
        public bool? Rekening_Limbah_Include { get; set; } = false;
        public bool? Rekening_LLTT_Include { get; set; } = false;
        public bool? TanpaBiayaPemakaianAir { get; set; } = false;
        public DateTime? WaktuUpdate { get; set; }
    }
}
