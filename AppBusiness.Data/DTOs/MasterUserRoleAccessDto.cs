using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterUserRoleAccessDto
    {
        public int? IdPdam { get; set; }
        public int? IdRoleAccess { get; set; }
        public int? IdModule { get; set; }
        public int? IdAccess { get; set; }
        public string NamaModule { get; set; }
        public string NamaAccess { get; set; }
        public bool? Value { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
