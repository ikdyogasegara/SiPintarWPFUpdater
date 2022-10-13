using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class MasterUserModuleAccessDto
    {
        public int? IdModule { get; set; }
        public string NamaModule { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public List<MasterUserAccessDto> Access { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
