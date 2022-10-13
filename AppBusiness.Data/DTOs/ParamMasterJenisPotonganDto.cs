namespace AppBusiness.Data.DTOs
{
    public class ParamMasterJenisPotonganDto
    {
        public int? IdPdam { get; set; }
        public int? IdJenisPotongan { get; set; }
        public string KodeJenisPotongan { get; set; }
        public string NamaJenisPotongan { get; set; }
        public int? Urutan { get; set; } = 0;
        public string Tipe { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterJenisPotonganFilterDto : ParamMasterJenisPotonganDto
    {
    }
}
