using System;

namespace AppBusiness.Data.DTOs
{
    public class PermohonanPelangganLlttRabDetailDto
    {
        public int? Id { get; set; }
        public int? IdPdam { get; set; }
        public int? IdPermohonan { get; set; }
        public string Tipe { get; set; }
        public string Kode { get; set; }
        public string Nama { get; set; }
        public decimal? Harga { get; set; }
        public string Satuan { get; set; }
        public decimal? Qty { get; set; }
        public decimal? SebelumPpn { get; set; }
        public decimal? Ppn { get; set; }
        public decimal? Keuntungan { get; set; }
        public decimal? JasaDariBahan { get; set; }
        public decimal? Jumlah { get; set; }
        public string Kategori { get; set; }
        public decimal? QtyRkp { get; set; }
        public bool? FlagBiayaDibebankanKePdam { get; set; } = false;
        public bool? FlagDialihkanKeVendor { get; set; } = false;
        public bool? FlagPaket { get; set; } = false;
        public bool? FlagDistribusi { get; set; } = false;
        public DateTime? Waktuupdate { get; set; }
    }
}
