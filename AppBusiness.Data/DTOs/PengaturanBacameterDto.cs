using System;

namespace AppBusiness.Data.DTOs
{
    public class PengaturanBacameterDto
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public string Host { get; set; }
        public string User { get; set; }
        public string Pass { get; set; }
        public string Port { get; set; }
        public string Database { get; set; }
        public int? SelisihBulanBaca { get; set; }
        public bool? AksesFotoMeter { get; set; }
        public string LokasiFotoMeter { get; set; }
        public bool? Status { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
