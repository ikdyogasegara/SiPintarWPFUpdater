using System;

namespace AppBusiness.Data.DTOs
{
    public class DashboardMasterAccessDto
    {
        public int? IdAccess { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
