namespace AppBusiness.Data.DTOs
{
    public class ParamMasterGudangDto
    {
        public int? IdPdam { get; set; }
        public int? IdGudang { get; set; }
        public string KodeGudang { get; set; }
        public string NamaGudang { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterGudangFilterDto : ParamMasterGudangDto
    {
    }
}
