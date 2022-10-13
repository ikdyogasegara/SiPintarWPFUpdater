using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class MasterPaketMaterialDto
    {
        public int? IdPdam { get; set; }
        public int? IdPaketMaterial { get; set; }
        public string KodePaketMaterial { get; set; }
        public string NamaPaketMaterial { get; set; }
        public string Keterangan { get; set; }
        public decimal? Total { get; set; }
        public List<MasterPaketMaterialDetailDto> Detail { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }

    public class MasterPaketMaterialDetailDto
    {
        public int? IdMaterial { get; set; }
        public string KodeMaterial { get; set; }
        public string NamaMaterial { get; set; }
        public string Satuan { get; set; }
        public decimal? Qty { get; set; } = 0;
        public decimal? HargaJual { get; set; } = 0;
        public decimal? Total { get; set; } = 0;
    }
}
