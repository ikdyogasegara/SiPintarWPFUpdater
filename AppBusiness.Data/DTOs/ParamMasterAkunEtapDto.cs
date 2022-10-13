using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterAkunEtapDto
    {
        public int? IdPdam { get; set; }
        public int? IdAkunEtap { get; set; }
        public string KodeAkunEtap { get; set; }
        public string NamaAkunEtap { get; set; }
        public bool? FlagKelompokEtap { get; set; }
        public string TipeAkunEtap { get; set; } = "-";
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterAkunEtapFilterDto : ParamMasterAkunEtapDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
