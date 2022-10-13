using System;

namespace AppBusiness.Data.DTOs
{
    public class SummaryProduktifitasBacameterPetugasBacaPerHariDto
    {
        public DateTime? Tanggal { get; set; }
        public int? Target { get; set; }
        public int? Terbaca { get; set; }
        public int? SelisihHarian { get; set; }
        public DateTime? LastWaktuKirimHasilBaca { get; set; }
    }
}
