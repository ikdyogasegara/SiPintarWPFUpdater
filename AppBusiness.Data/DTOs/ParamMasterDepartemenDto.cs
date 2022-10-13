namespace AppBusiness.Data.DTOs
{
    public class ParamMasterDepartemenDto
    {
        public int? IdPdam { get; set; }
        public int? IdDepartemen { get; set; }
        public string KodeDepartemen { get; set; }
        public string Departemen { get; set; }
        public int? Urutan { get; set; } = 0;
        public int? Flag { get; set; } = 0;
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterDepartemenFilterDto : ParamMasterDepartemenDto
    {
    }
}
