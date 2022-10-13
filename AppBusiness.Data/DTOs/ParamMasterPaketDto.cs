using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterPaketDto
    {
        public int? IdPdam { get; set; }
        public int? IdPaket { get; set; }
        public string KodePaket { get; set; }
        public string NamaPaket { get; set; }
        public int? IdPaketMaterial { get; set; }
        public List<ParamMasterPaketMaterialDetailDto> DetailMaterial { get; set; }
        public int? IdPaketOngkos { get; set; }
        public List<ParamMasterPaketOngkosDetailDto> DetailOngkos { get; set; }
        public decimal? PpnMaterial { get; set; } = 0;
        public decimal? PpnMaterialTambahan { get; set; } = 0;
        public decimal? PpnOngkos { get; set; } = 0;
        public decimal? PpnOngkosTambahan { get; set; } = 0;
        public decimal? PersentaseKeuntungan { get; set; } = 0;
        public decimal? PersentaseJasaDariBahan { get; set; } = 0;
        public decimal? HargaNetMaterial { get; set; } = 0;
        public decimal? HargaNetOngkos { get; set; } = 0;
        public decimal? HargaNetPaket { get; set; } = 0;
        public bool? Status { get; set; } = false;
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterPaketFilterDto : ParamMasterPaketDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
