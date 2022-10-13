using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterKeperluanDto
    {
        public int? IdPdam { get; set; }
        public int? IdKeperluan { get; set; }
        public string KodeKeperluan { get; set; }
        public string Keperluan { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterKeperluanFilterDto : ParamMasterKeperluanDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
