using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class JurnalUmumDto
    {
        public int? IdPdam { get; set; }
        public int? IdTransaksi { get; set; }
        public string NomorTransaksi { get; set; }
        public string Uraian { get; set; }
        public string Penjelasan { get; set; }
        public decimal? Jumlah { get; set; } = 0;
        public DateTime? TanggalTransaksi { get; set; }
        public string Kategori { get; set; }
        public bool? FlagPosting { get; set; } = false;
        public bool? FlagHapus { get; set; } = false;
        public DateTime? WaktuUpdate { get; set; }
        public List<JurnalUmumDetailDto> JUDetail { get; set; }
    }
}
