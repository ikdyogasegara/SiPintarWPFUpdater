namespace AppBusiness.Data.DTOs
{
    public class ParamKonfirmasiHasilBacaUlangDto
    {
        public int? IdPdam { get; set; }
        public int? IdPelangganAir { get; set; }
        public int? IdPeriode { get; set; }
        public bool? Konfirmasi { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamKonfirmasiHasilBacaUlangFilterDto : ParamKonfirmasiHasilBacaUlangDto
    {
    }
}
