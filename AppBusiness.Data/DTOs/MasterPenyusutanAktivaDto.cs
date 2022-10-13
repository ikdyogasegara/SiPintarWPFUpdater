using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterPenyusutanAktivaDto
    {
        public int? IdPdam { get; set; }
        public int? IdGolAktiva { get; set; }
        public string KodeGolAktiva { get; set; }
        public string NamaGolAktiva { get; set; }
        public int? JmlTahun { get; set; } = 0;
        public decimal? JmlPersen { get; set; } = decimal.Zero;
        public string KodeSusut { get; set; }
        public bool? FlagHapus { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
