using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class MasterPaketOngkosDto
    {
        public int? IdPdam { get; set; }
        public int? IdPaketOngkos { get; set; }
        public string KodePaketOngkos { get; set; }
        public string NamaPaketOngkos { get; set; }
        public string Keterangan { get; set; }
        public DateTime? WaktuUpdate { get; set; }
        public List<MasterPaketOngkosDetailDto> Detail { get; set; }
    }

    public class MasterPaketOngkosDetailDto
    {
        public int? IdOngkos { get; set; }
        public string KodeOngkos { get; set; }
        public string NamaOngkos { get; set; }
        public string Kelompok { get; set; }
        public string PostBiaya { get; set; }
        public string Satuan { get; set; }
        public decimal? Qty { get; set; } = 0;
        public decimal? Biaya { get; set; } = 0;
        public string Perhitungan { get; set; }
        public int? PersentaseDari_IdPaketMaterial { get; set; }
        public decimal? Persentase { get; set; } = 0;
        public decimal? Total { get; set; } = 0;
    }
}
