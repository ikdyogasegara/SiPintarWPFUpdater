using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamReleaseLogDto
    {
        public int? IdPdam { get; set; }
        public string ComputerId { get; set; }
        public string Versi { get; set; }
        public string Aplikasi { get; set; }
        public string Log { get; set; }
    }

    public class ParamReleaseLogFilterDto : ParamReleaseLogDto
    {
        public DateTime? WaktuUpdateMulai { get; set; }
        public DateTime? WaktuUpdateSampaiDengan { get; set; }
    }
}
