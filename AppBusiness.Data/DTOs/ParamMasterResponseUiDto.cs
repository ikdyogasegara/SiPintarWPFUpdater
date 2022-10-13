using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterResponseUiDto
    {
        public int? IdUserRequest { get; set; }
        public int? ID { get; set; }
        public int? IdPlatform { get; set; }
        public int? IdTipe { get; set; }
        public string Page { get; set; }
        public string Key { get; set; }
        public string Message { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public DateTime? WaktuUpdate { get; set; }
    }

    public class ParamMasterResponseUiFilterDto : ParamMasterResponseUiDto
    {
    }
}
