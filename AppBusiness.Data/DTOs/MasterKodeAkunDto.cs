using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterKodeAkunDto
    {
        public int? IdPdam { get; set; }
        public int? IdKodeAkun { get; set; }
        public string KodeAkun { get; set; }
        public string Deskripsi { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
