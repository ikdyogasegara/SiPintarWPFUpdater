namespace AppBusiness.Data.DTOs
{
    public class ParamMasterLisensiDto
    {
        public int? IdPdam { get; set; }
        public int? IdLisensi { get; set; }
        public int? IdPegawai { get; set; }
        public string Aplikasi { get; set; }
        public string KodeDevice { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterLisensiFilterDto : ParamMasterLisensiDto
    {
        public string NamaPegawai { get; set; }
    }
}
