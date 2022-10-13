namespace AppBusiness.Data.DTOs
{
    public class ParamMasterWarnaGantiMeterDto
    {
        public int? IdPdam { get; set; }
        public int? IdWarnaGantiMeter { get; set; }
        public string KodeWarnaGantiMeter { get; set; }
        public string WarnaGantiMeter { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterWarnaGantiMeterFilterDto : ParamMasterWarnaGantiMeterDto
    {
    }
}
