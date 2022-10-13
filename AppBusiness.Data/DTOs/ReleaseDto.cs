using System;

namespace AppBusiness.Data.DTOs
{
    public class ReleaseDto
    {
        public int? Id { get; set; }
        public string Versi { get; set; }
        public string Aplikasi { get; set; }
        public string Url { get; set; }
        public DateTime? Waktu_Create { get; set; }
        public bool? FlagRelease { get; set; }
        public DateTime? Waktu_Release { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
