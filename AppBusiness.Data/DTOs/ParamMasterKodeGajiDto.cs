namespace AppBusiness.Data.DTOs
{
    public class ParamMasterKodeGajiDto
    {
        public int? IdPdam { get; set; }
        public int? IdKodeGaji { get; set; }
        public string KodeGaji { get; set; }
        public string Deskripsi { get; set; }
        public string Keterangan { get; set; }
        public bool? FlagGaji { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterKodeGajiFilterDto : ParamMasterKodeGajiDto
    {
    }
}
