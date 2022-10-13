using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterSaranPertanyaanDto
    {
        public int? IdPertanyaan { get; set; }
        public string Pertanyaan { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
        public DateTime? WaktuCreate { get; set; }
    }

    public class ParamMasterSaranPertanyaanFilterDto : ParamMasterSaranPertanyaanDto
    {
    }
}
