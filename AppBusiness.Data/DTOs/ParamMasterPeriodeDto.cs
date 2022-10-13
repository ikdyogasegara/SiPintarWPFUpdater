using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterPeriodeDto
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public int? IdPeriode { get; set; }
        public int? KodePeriode { get; set; }
        public DateTime? TglMulaiDenda1 { get; set; }
        public DateTime? TglMulaiDenda2 { get; set; }
        public DateTime? TglMulaiDenda3 { get; set; }
        public DateTime? TglMulaiDenda4 { get; set; }
        public DateTime? TglMulaiDendaPerBulan { get; set; }
        public bool? Status { get; set; } = true;
        public bool? FlagHapus { get; set; } = false;

        #region update tgldenda
        public int? IdRayon { get; set; }
        public int? IdWilayah { get; set; }
        #endregion

    }

    public class ParamMasterPeriodeFilterDto : ParamMasterPeriodeDto
    {
        public bool? ShowDetail { get; set; } = false;
        public int? Tahun { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }

    public class ParamMasterPeriodeChartLimitDto
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public int? Value { get; set; } = 0;
    }
}
