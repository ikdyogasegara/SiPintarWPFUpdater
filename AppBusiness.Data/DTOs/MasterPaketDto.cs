using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class MasterPaketDto
    {
        public int? IdPdam { get; set; }
        public int? IdPaket { get; set; }
        public string KodePaket { get; set; }
        public string NamaPaket { get; set; }
        public int? IdPaketMaterial { get; set; }
        public string KodePaketMaterial { get; set; }
        public string NamaPaketMaterial { get; set; }
        public List<MasterPaketMaterialDetailDto> DetailMaterial { get; set; }
        public int? IdPaketOngkos { get; set; }
        public string KodePaketOngkos { get; set; }
        public string NamaPaketOngkos { get; set; }
        public List<MasterPaketOngkosDetailDto> DetailOngkos { get; set; }
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
        public DateTime? WaktuUpdate { get; set; }
    }
}
