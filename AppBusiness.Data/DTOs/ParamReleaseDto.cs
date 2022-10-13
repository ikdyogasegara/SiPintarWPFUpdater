using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamReleaseDto
    {
        public int? Id { get; set; }
        public string Versi { get; set; }
        public string Aplikasi { get; set; }
        public string Url { get; set; }
        public bool? FlagHapus { get; set; } = false;
    }

    public class ParamReleaseFilterDto : ParamReleaseDto
    {
        public bool? FlagRelease { get; set; }
        public DateTime? Waktu_Create_Mulai { get; set; }
        public DateTime? Waktu_Create_Sampai_Dengan { get; set; }
        public DateTime? Waktu_Release_Mulai { get; set; }
        public DateTime? Waktu_Release_Sampai_Dengan { get; set; }
        public bool? HanyaLastRelease { get; set; }
    }

    public class ParamSetReleaseDto
    {
        public int? Id { get; set; }
        public bool? FlagRelease { get; set; }
    }
}
