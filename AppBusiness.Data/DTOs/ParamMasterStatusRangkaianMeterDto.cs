namespace AppBusiness.Data.DTOs
{
    public class ParamMasterStatusRangkaianMeterDto
    {
        public int? IdPdam { get; set; }
        public int? IdStatusRangkaianMeter { get; set; }
        public string StatusRangkaianMeter { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterStatusRangkaianMeterFilterDto : ParamMasterStatusRangkaianMeterDto
    {
    }
}
