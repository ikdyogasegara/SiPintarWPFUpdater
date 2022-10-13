using System;

namespace AppBusiness.Data.DTOs
{
    public class SummaryProduktifitasBacameterPembacaanPerTanggalDto
    {
        public DateTime? Tanggal { get; set; }
        public int? Baca { get; set; }
        public int? Taksir { get; set; }
        public int? Kelainan { get; set; }
    }
}
