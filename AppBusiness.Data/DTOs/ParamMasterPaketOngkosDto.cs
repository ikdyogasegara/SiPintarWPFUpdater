using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterPaketOngkosDto
    {
        public int? IdPdam { get; set; }
        public int? IdPaketOngkos { get; set; }
        public string KodePaketOngkos { get; set; }
        public string NamaPaketOngkos { get; set; }
        public string Keterangan { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
        public List<ParamMasterPaketOngkosDetailDto> Detail { get; set; }
    }

    public class ParamMasterPaketOngkosDetailDto
    {
        public int? IdOngkos { get; set; }
        public decimal? Qty { get; set; } = 0;
    }

    public class ParamMasterPaketOngkosFilterDto : ParamMasterPaketOngkosDto
    {
    }
}
