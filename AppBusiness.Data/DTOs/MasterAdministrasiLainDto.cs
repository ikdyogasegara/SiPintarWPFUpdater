using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterAdministrasiLainDto
    {
        public int? IdPdam { get; set; }
        public int? IdAdministrasiLain { get; set; }
        public string KodeAdministrasiLain { get; set; }
        public string NamaAdministrasiLain { get; set; }
        public decimal? Administrasi { get; set; } = 0;
        public DateTime? WaktuUpdate { get; set; }
    }
}
