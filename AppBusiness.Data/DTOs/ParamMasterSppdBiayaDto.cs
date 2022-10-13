namespace AppBusiness.Data.DTOs
{
    public class ParamMasterSppdBiayaDto
    {
        public int? IdPdam { get; set; }
        public int? IdBiayaSppd { get; set; }
        public string Deskripsi { get; set; }
        public decimal? Biaya { get; set; } = 0;
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterSppdBiayaFilterDto : ParamMasterSppdBiayaDto
    {
    }
}
