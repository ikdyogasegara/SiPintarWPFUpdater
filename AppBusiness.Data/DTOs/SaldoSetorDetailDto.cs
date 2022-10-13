using System;

namespace AppBusiness.Data.DTOs
{
    public class SaldoSetorDetailDto
    {
        public int? IdPdam { get; set; }

        public int? IdDetailSetor { get; set; }
        public int? IdSetor { get; set; }
        public int? IdBank { get; set; }
        public string? Ref { get; set; }

        public decimal? Jumlah { get; set; }
    }
}
