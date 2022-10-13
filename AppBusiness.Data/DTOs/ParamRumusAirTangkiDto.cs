namespace AppBusiness.Data.DTOs
{
    public class ParamRumusAirTangkiDto
    {
        public int? IdPdam { get; set; }
        public int? IdTarifTangki { get; set; }
        public decimal? Jumlahm3 { get; set; } = 0;
        public decimal? JarakKm { get; set; } = 0;
        public decimal? JumlahArmada { get; set; } = 0;
        public int? IdUserRequest { get; set; }
    }
}
