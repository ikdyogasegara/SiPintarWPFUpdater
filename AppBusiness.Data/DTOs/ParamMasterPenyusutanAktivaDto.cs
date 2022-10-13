using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterPenyusutanAktivaDto
    {
        public int? IdPdam { get; set; }
        public int? IdGolAktiva { get; set; }
        public string KodeGolAktiva { get; set; }
        public string NamaGolAktiva { get; set; }
        public int? JmlTahun { get; set; }
        public decimal? JmlPersen { get; set; }
        public string KodeSusut { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterPenyusutanAktivaFilterDto : ParamMasterPenyusutanAktivaDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
