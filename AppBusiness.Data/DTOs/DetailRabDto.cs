namespace AppBusiness.Data.DTOs
{
    public class DetailRabDto
    {
        public string Kode { get; set; }
        public string Tipe { get; set; }
        public string Uraian { get; set; }
        public string Satuan { get; set; }
        public decimal? HargaSatuan { get; set; } = 0;
        public decimal? Qty { get; set; } = 0;
        public decimal? Jumlah { get; set; } = 0;
        public decimal? Ppn { get; set; } = 0;
        public decimal? Keuntungan { get; set; } = 0;
        public decimal? JasaDariBahan { get; set; } = 0;
        public decimal? Total { get; set; } = 0;

        public string Kelompok { get; set; } //ongkos
        public string PostBiaya { get; set; } //ongkos
        public bool? FlagBiayaDibebankanKePdam { get; set; } = false;
        public bool? FlagDialihkanKevendor { get; set; } = false;
    }
}
