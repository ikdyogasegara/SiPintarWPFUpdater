using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterLoggerDto
    {
        public int? ID { get; set; }
        public int? IdPdam { get; set; }
        public int? IdUser { get; set; }
        public string Nama { get; set; }
        public int? IdRole { get; set; }
        public bool? Sukses { get; set; }
        public string Tipe { get; set; }
        public string Log { get; set; }
        public string Value { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterLoggerFilterDto : ParamMasterLoggerDto
    {
        public DateTime? WaktuUpdateMulai { get; set; }
        public DateTime? WaktuUpdateSampaiDengan { get; set; }
    }
}
