namespace AppBusiness.Data.DTOs
{
    public class ProgressBacaDto
    {
        public int? IdPdam { get; set; }
        public int? IdPeriode { get; set; }
        public int? JumlahRayon { get; set; }
        public int? TotalSemua { get; set; }
        public int? TotalM3Terbaca { get; set; }
        public int? TotalSudahBaca { get; set; }
        public int? TotalBelumBaca { get; set; }
        public int? TotalSudahBacaKelainan { get; set; }
        public int? TotalRequestBacaUlang { get; set; }
        public int? IdUserRequest { get; set; }
    }
}
