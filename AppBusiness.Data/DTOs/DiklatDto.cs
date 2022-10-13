using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class DiklatDto
    {
        public int? IdPdam { get; set; }
        public int? IdDiklat { get; set; }
        public string NoSertifikat { get; set; }
        public string Uraian { get; set; }
        public int? Tahun { get; set; }
        public DateTime? TglAwal { get; set; }
        public DateTime? TglAkhir { get; set; }
        public string Tempat { get; set; }
        public string Penyelenggara { get; set; }
        public DateTime? Waktuupdate { get; set; }
        public List<DiklatDetailDto> Detail { get; set; }
    }
}
