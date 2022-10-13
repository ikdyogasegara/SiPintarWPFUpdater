using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamSummaryProduktifitasBacameterDto
    {
        public int? IdPdam { get; set; }
        public int? IdPeriode { get; set; }
        public string Kategori { get; set; }
        public DateTime? WaktuBacaMulai { get; set; }
        public DateTime? WaktuBacaSampaiDengan { get; set; }
        public DateTime? WaktuKirimHasilBacaMulai { get; set; }
        public DateTime? WaktuKirimHasilBacaSampaiDengan { get; set; }
        public int? IdRayon { get; set; }
        public int? IdWilayah { get; set; }
        public int? IdKelurahan { get; set; }
        public int? IdPetugasBaca { get; set; }
        public int? IdUserRequest { get; set; }
    }
}
