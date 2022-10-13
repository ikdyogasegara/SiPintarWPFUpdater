using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterPeriodeTglDendaDto
    {
        public int? IdPdam { get; set; }
        public int? IdPeriode { get; set; }
        public int? IdRayon { get; set; }
        public DateTime? TglMulaiDenda1 { get; set; }
        public DateTime? TglMulaiDenda2 { get; set; }
        public DateTime? TglMulaiDenda3 { get; set; }
        public DateTime? TglMulaiDenda4 { get; set; }
        public DateTime? TglMulaiDendaPerBulan { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
