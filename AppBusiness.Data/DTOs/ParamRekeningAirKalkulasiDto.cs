using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamRekeningAirKalkulasiDto
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public bool? Simulasi { get; set; } = false;
        public int? IdGolongan { get; set; }
        public int? IdDiameter { get; set; }
        public decimal? Pakai { get; set; }
        public int? KodePeriode { get; set; }
        public int? IdAdministrasiLain { get; set; }
        public int? IdPemeliharaanLain { get; set; }
        public int? IdRetribusiLain { get; set; }
        public int? IdStatus { get; set; }
        public int? IdFlag { get; set; }
        public DateTime? TglMulaiDenda1 { get; set; }
        public DateTime? TglMulaiDenda2 { get; set; }
        public DateTime? TglMulaiDenda3 { get; set; }
        public DateTime? TglMulaiDenda4 { get; set; }
        public DateTime? TglMulaiDendaPerBulan { get; set; }
    }
}
