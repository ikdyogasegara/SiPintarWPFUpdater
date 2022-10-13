namespace AppBusiness.Data.DTOs
{
    public class ResultKalkulasiAirTangkiDto
    {
        public decimal? BiayaAir { get; set; }
        public decimal? BiayaAdministrasi { get; set; }
        public decimal? BiayaOperasional { get; set; }
        public decimal? BiayaTransport { get; set; }
        public decimal? BiayaTotal { get; set; }
        public decimal? Ppn { get; set; }
        public decimal? Total { get; set; }
    }
}
