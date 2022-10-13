using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterPemeliharaanLainDto
    {
        public int? IdPdam { get; set; }
        public int? IdPemeliharaanLain { get; set; }
        public string KodePemeliharaanLain { get; set; }
        public string NamaPemeliharaanLain { get; set; }
        public decimal? Pemeliharaan { get; set; } = 0;
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterPemeliharaanLainFilterDto : ParamMasterPemeliharaanLainDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
