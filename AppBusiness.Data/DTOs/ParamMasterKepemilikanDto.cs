using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterKepemilikanDto
    {
        public int? IdPdam { get; set; }
        public int? IdKepemilikan { get; set; }
        public string KodeKepemilikan { get; set; }
        public string NamaKepemilikan { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterKepemilikanFilterDto : ParamMasterKepemilikanDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
