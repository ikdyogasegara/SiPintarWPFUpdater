using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterPaketMaterialDto
    {
        public int? IdPdam { get; set; }
        public int? IdPaketMaterial { get; set; }
        public string KodePaketMaterial { get; set; }
        public string NamaPaketMaterial { get; set; }
        public string Keterangan { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
        public List<ParamMasterPaketMaterialDetailDto> Detail { get; set; }
    }

    public class ParamMasterPaketMaterialDetailDto
    {
        public int? IdMaterial { get; set; }
        public decimal? Qty { get; set; } = 0;
    }

    public class ParamMasterPaketMaterialFilterDto : ParamMasterPaketMaterialDto
    {
    }
}
