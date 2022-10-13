using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class DashboardUserDto
    {
        public int? Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public DateTime? WaktuUpdate { get; set; }

        public List<DashboardUserAccessDto> Access { get; set; } = new List<DashboardUserAccessDto>();
    }
}
