using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterTipePendaftaranSambunganDto
    {
        public int? IdPdam { get; set; }
        public int? IdTipePendaftaranSambungan { get; set; }
        public string NamaTipePendaftaranSambungan { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterTipePendaftaranSambunganFilterDto : ParamMasterTipePendaftaranSambunganDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
