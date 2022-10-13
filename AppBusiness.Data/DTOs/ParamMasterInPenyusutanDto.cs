using System;
using System.Collections.Generic;
using System.Text;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterInPenyusutanDto
    {
        public int? IdPdam { get; set; }
        public int? IdInPenyusutan { get; set; }
        public int? IdAkunAktiva { get; set; }
        public int? IdAkunAkmPeny { get; set; }
        public int? IdAkunBiaya { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }
    public class ParamMasterInPenyusutanFilterDto : ParamMasterInPenyusutanDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
