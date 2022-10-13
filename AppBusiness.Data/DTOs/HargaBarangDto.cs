namespace AppBusiness.Data.DTOs
{
    public class HargaBarangDto
    {
        public int? Id { get; set; }
        public int? IdBarang { get; set; }
        public string KodeBarang { get; set; }
        public string NamaBarang { get; set; }
        public string NomorTransaksi { get; set; }
        public decimal? Harga { get; set; }
        public decimal? Qty_Stock { get; set; }
        public int? IdSatuanBarang { get; set; }
        public string Kode { get; set; }
    }
}
