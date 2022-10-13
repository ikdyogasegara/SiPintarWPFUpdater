using System;

namespace AppBusiness.Data.DTOs
{
    public class BarangHabisDto
    {
        public int? IdPdam { get; set; }
        public int? IdBarang { get; set; }
        public string KodeBarang { get; set; }
        public string NamaBarang { get; set; }
        public int? IdSatuanBarang { get; set; }
        public string SatuanBarang { get; set; }
        public int? IdJenisBarang { get; set; }
        public string KodeJenisBarang { get; set; }
        public string NamaJenisBarang { get; set; }
        public int? IdTipeBarang { get; set; }
        public string NamaTipeBarang { get; set; }
        public int? IdDiameterBarang { get; set; }
        public string DiameterBarang { get; set; }
        public int? IdKodeAkun { get; set; }
        public string KodeAkun { get; set; }
        public string Deskripsi { get; set; }
        public string SeriBarang { get; set; }
        public int? MinQty { get; set; }
        public decimal? Stock { get; set; }
        public decimal? HargaBeli { get; set; }
        public string Loker { get; set; }
        public string Foto { get; set; }
        public bool? Status { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
