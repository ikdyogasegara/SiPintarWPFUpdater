using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterKodeGajiDto
    {
        public int? IdPdam { get; set; }
        public int? IdKodeGaji { get; set; }
        public string KodeGaji { get; set; }
        public string Deskripsi { get; set; }
        public string Keterangan { get; set; }
        public bool? FlagGaji { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
