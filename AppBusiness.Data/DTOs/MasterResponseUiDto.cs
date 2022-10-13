using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterResponseUiDto
    {
        public int? ID { get; set; }
        public int? IdPlatform { get; set; }
        public string NamaPlatform { get; set; }
        public int? IdTipe { get; set; }
        public string NamaTipe { get; set; }
        public string Page { get; set; }
        public string Key { get; set; }
        public string Message { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
