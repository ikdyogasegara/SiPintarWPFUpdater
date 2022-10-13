namespace AppBusiness.Data.DTOs
{
    public class ParamMasterKodeAkunDto
    {
        public int? IdPdam { get; set; }
        public int? IdKodeAkun { get; set; }
        public string KodeAkun { get; set; }
        public string Deskripsi { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterKodeAkunFilterDto : ParamMasterKodeAkunDto
    {
    }
}
