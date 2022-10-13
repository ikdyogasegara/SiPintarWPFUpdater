namespace AppBusiness.Data.DTOs
{
    public class ParamMasterJabatanDto
    {
        public int? IdPdam { get; set; }
        public int? IdJabatan { get; set; }
        public string Jabatan { get; set; }
        public int? Urutan { get; set; } = 0;
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterJabatanFilterDto : ParamMasterJabatanDto
    {
    }
}
