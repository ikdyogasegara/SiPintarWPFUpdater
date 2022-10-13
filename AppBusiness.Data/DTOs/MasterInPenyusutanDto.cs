using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterInPenyusutanDto
    {
        public int? IdPdam { get; set; }
        public int? IdInPenyusutan { get; set; }
        public int? IdAkunAktiva { get; set; }
        public string KodeAkunAktiva { get; set; }
        public string NamaAkunAktiva { get; set; }
        public int? IdAkunAkmPeny { get; set; }
        public string KodeAkunAkmPeny { get; set; }
        public string NamaAkunAkmPeny { get; set; }
        public int? IdAkunBiaya { get; set; }
        public string KodeAkunBiaya { get; set; }
        public string NamaAkunBiaya { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public DateTime? WaktuUpdate { get; set; }
    }
}
