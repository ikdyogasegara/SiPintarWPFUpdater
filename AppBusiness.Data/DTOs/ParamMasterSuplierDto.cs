namespace AppBusiness.Data.DTOs
{
    public class ParamMasterSuplierDto
    {
        public int? IdPdam { get; set; }
        public int? IdSuplier { get; set; }
        public string KodeSuplier { get; set; }
        public string NamaContactPerson { get; set; }
        public string NamaPerusahaan { get; set; }
        public string Jabatan { get; set; }
        public string Alamat { get; set; }
        public string Npwp { get; set; }
        public string NoTelp { get; set; }
        public string NoHp { get; set; }
        public string Email { get; set; }
        public int? Rating { get; set; }
        public string Notes { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterSuplierFilterDto : ParamMasterSuplierDto
    {
    }
}
