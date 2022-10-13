using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterAlasanBatalDto
    {
        public int? IdPdam { get; set; }
        public int? IdAlasanBatal { get; set; }
        public string AlasanBatal { get; set; }
        public bool? FlagTransaksi { get; set; } = false;
        public bool? FlagPermohonan { get; set; } = false;
        public bool? FlagSpk { get; set; } = false;
        public bool? FlagRab { get; set; } = false;
        public bool? FlagBa { get; set; } = false;
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterAlasanBatalFilterDto : ParamMasterAlasanBatalDto
    {
        public DateTime? WaktuUpdate { get; set; }

    }
}
