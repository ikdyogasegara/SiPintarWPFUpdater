namespace AppBusiness.Data.DTOs
{
    public class ParamMasterJenisBarangDto
    {
        public int? IdPdam { get; set; }
        public int? IdJenisBarang { get; set; }
        public string KodeJenisBarang { get; set; }
        public string NamaJenisBarang { get; set; }
        public int? IdTipeBarang { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterJenisBarangFilterDto : ParamMasterJenisBarangDto
    {
    }
}
