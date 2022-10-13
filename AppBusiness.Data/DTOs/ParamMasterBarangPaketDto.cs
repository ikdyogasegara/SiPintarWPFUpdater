using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterBarangPaketDto
    {
        public int? IdPdam { get; set; }
        public int? IdPaketBarang { get; set; }
        public string NamaPaketBarang { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
        public List<ParamMasterBarangPaketDetailDto> Detail { get; set; }
    }

    public class ParamMasterBarangPaketDetailDto
    {
        public int? IdBarang { get; set; }
        public decimal? Qty { get; set; } = 0;
    }

    public class ParamMasterBarangPaketFilterDto : ParamMasterBarangPaketDto
    {
        public int? IdSatuanBarang { get; set; }
        public int? IdBarang { get; set; }
        public string KodeBarang { get; set; }
        public string NamaBarang { get; set; }
    }
}
