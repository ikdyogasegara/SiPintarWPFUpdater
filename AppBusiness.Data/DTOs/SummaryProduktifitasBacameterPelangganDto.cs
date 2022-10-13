namespace AppBusiness.Data.DTOs
{
    public class SummaryProduktifitasBacameterPelangganDto
    {
        public string Bulan { get; set; }
        public int? TotalPelanggan { get; set; }
        public int? SudahBaca { get; set; }
        public int? FotoMeter { get; set; }
        public int? FotoRumah { get; set; }
        public int? Video { get; set; }
        public int? Kelainan { get; set; }
        public int? Gps { get; set; }
        public int? Taksir { get; set; }
    }
}
