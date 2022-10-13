using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterPdamDto
    {
        public int? IdPdam { get; set; }
        public string NamaPdam { get; set; }
        public string Provinsi { get; set; }
        public string Kota { get; set; }
        public string AlamatLengkap { get; set; }
        public string Tipe { get; set; } = "basic";
        public List<ParamMasterPdamDetailDto> Detail { get; set; }
    }

    public class ParamMasterPdamDetailDto
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class ParamMasterPdamFilterDto : ParamMasterPdamDto
    {
    }
}
