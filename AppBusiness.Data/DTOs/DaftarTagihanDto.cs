namespace AppBusiness.Data.DTOs
{
    public class DaftarTagihanDto
    {
        public int? IdPdam { get; set; }
        public int? IdPeriode { get; set; }
        public int? IdPelangganAir { get; set; }
        public int? IdPelangganLimbah { get; set; }
        public int? IdPelangganLltt { get; set; }
        public int? IdNonAir { get; set; }
        public string Rekening { get; set; }
        public int? KodePeriode { get; set; }
        public string NamaPeriode { get; set; }
        public string Keterangan { get; set; }
        public decimal? Tagihan { get; set; } = 0;
        public decimal? Denda { get; set; } = 0;
        public decimal? Total { get; set; } = 0;
    }
}
