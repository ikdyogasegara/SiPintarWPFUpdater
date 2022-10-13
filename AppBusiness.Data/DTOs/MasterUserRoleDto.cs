using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class MasterUserRoleDto
    {
        public int? IdRole { get; set; }
        public int? IdPdam { get; set; }
        public string NamaRole { get; set; }
        public string NamaPdam { get; set; }
        public List<MasterUserRoleAccessDto> Akses { get; set; }
        public int? IdModule { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
