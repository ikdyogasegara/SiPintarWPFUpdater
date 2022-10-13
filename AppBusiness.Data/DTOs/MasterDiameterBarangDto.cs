using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterDiameterBarangDto
    {
        public int? IdPdam { get; set; }
        public int? IdDiameterBarang { get; set; }
        public string DiameterBarang { get; set; }
        public bool? FlagHapus { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
