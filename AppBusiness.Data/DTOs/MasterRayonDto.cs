using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterRayonDto
    {
        public int? IdPdam { get; set; }
        public int? IdRayon { get; set; }
        public string KodeRayon { get; set; }
        public string NamaRayon { get; set; }
        public int? IdArea { get; set; }
        public string KodeArea { get; set; }
        public string NamaArea { get; set; }
        public int? IdWilayah { get; set; }
        public string KodeWilayah { get; set; }
        public string NamaWilayah { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
