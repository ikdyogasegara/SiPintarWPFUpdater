namespace AppBusiness.Data.DTOs
{
    public class PermohonanNonPelangganRabDetailCetakDto
    {
        public string Tipe { get; set; }
        public string Kode { get; set; }
        public string Uraian { get; set; }
        public decimal HargaSatuan { get; set; } = 0;
        public string Satuan { get; set; }
        public decimal Qty { get; set; } = 0;
        public decimal SebelumPpn { get; set; } = 0;
        public decimal Ppn { get; set; } = 0;
        public decimal Keuntungan { get; set; } = 0;
        public decimal Jumlah { get; set; } = 0;
        public string Kategori { get; set; }
        public decimal QtyRkp { get; set; } = 0;
        public bool FlagBiayaDibebankanKePdam { get; set; }
        public bool FlagDialihkanKeVendor { get; set; }
        public bool FlagPaket { get; set; }
        public bool FlagPersil { get; set; }
    }
}
