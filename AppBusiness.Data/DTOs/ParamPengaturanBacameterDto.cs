namespace AppBusiness.Data.DTOs
{
    public class ParamPengaturanBacameterDto
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public string Host { get; set; }
        public string User { get; set; }
        public string Pass { get; set; }
        public string Port { get; set; }
        public string Database { get; set; }
        public int? SelisihBulanBaca { get; set; } = 0;
        public bool? AksesFotoMeter { get; set; } = false;
        public string LokasiFotoMeter { get; set; }
        public bool? Status { get; set; } = false;
    }

    public class ParamPengaturanBacameterFilterDto : ParamPengaturanBacameterDto
    {
    }
}
