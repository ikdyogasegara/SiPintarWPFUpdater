using System;

namespace AppBusiness.Data.DTOs
{
    public class DashboardUserAccessDto
    {
        public int? Id { get; set; }
        public int? IdUser { get; set; }
        public int? IdAccess { get; set; }
        public int? Value { get; set; } = 0;
        public DateTime? WaktuUpdate { get; set; }

        public DashboardMasterAccessDto Access { get; set; }
    }
}
