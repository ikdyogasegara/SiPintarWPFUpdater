using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterStatusPembayaranDto
    {
        public int? IdPdam { get; set; }
        public int? IdStatusPembayaran { get; set; }
        public string StatusPembayaran { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterStatusPembayaranFilterDto : ParamMasterStatusPembayaranDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
