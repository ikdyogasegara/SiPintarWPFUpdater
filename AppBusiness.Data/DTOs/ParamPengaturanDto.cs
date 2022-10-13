namespace AppBusiness.Data.DTOs
{
    public class ParamPengaturanDto
    {
        public int? ID { get; set; }
        public int? IdPdam { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamPengaturanFilterDto : ParamPengaturanDto
    {
    }
}
