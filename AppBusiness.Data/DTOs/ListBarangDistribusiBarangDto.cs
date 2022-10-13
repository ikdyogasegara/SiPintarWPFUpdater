namespace AppBusiness.Data.DTOs
{
    public class ListBarangDistribusiBarangDto
    {
        public int? IdPdam { get; set; }
        public int? IdBarang { get; set; }
        public string KodeBarang { get; set; }
        public string NamaBarang { get; set; }
        public string HargaJual { get; set; }
        public string SatuanBarang { get; set; }
        public decimal Stock { get; set; }
    }
}
