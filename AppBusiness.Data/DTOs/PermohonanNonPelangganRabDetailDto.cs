using System;

namespace AppBusiness.Data.DTOs
{
    public class PermohonanNonPelangganRabDetailDto
    {
        public int? Id { get; set; }
        public int? IdPdam { get; set; }
        public int? IdPermohonan { get; set; }
        public string Tipe { get; set; }
        public string Kode { get; set; }
        public string Uraian { get; set; }
        public decimal? HargaSatuan { get; set; }
        public string Satuan { get; set; }
        public decimal? Qty { get; set; }
        public decimal? Jumlah { get; set; }
        public decimal? Ppn { get; set; }
        public decimal? Keuntungan { get; set; }
        public decimal? JasaDariBahan { get; set; }
        public decimal? Total { get; set; }
        public string Kategori { get; set; }
        public string Kelompok { get; set; }
        public string PostBiaya { get; set; }
        public decimal? QtyRkp { get; set; }
        public bool? FlagBiayaDibebankanKePdam { get; set; } = false;
        public bool? FlagDialihkanKeVendor { get; set; } = false;
        public bool? FlagPaket { get; set; } = false;
        public bool? FlagDistribusi { get; set; } = false;
        public DateTime? Waktuupdate { get; set; }
    }
}
