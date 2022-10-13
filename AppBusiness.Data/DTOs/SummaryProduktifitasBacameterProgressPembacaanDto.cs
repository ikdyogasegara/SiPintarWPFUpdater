namespace AppBusiness.Data.DTOs
{
    public class SummaryProduktifitasBacameterProgressPembacaanDto
    {
        public string PetugasBaca { get; set; }
        public int? Taksir { get; set; }
        public int? Kelainan { get; set; }
        public decimal? Pakai { get; set; }
        public decimal? BiayaPemakaian { get; set; }
        public int? SudahBaca { get; set; }
        public int? BacaManual { get; set; }
        public int? BelumBaca { get; set; }
        public int? TotalPelanggan { get; set; }
        public decimal? Persentase { get; set; }
    }
}
