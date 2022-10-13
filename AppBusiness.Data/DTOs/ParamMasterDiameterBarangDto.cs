namespace AppBusiness.Data.DTOs
{
    public class ParamMasterDiameterBarangDto
    {
        public int? IdPdam { get; set; }
        public int? IdDiameterBarang { get; set; }
        public string DiameterBarang { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterDiameterBarangFilterDto : ParamMasterDiameterBarangDto
    {
    }
}
