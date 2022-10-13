using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class MutasiJabatanDto
    {
        public int? IdPdam { get; set; }
        public int? IdMutasi { get; set; }
        public string NoSk { get; set; }
        public string JenisMutasi { get; set; }
        public DateTime? TglSk { get; set; }
        public DateTime? TglBerlaku { get; set; }
        public DateTime? WaktuInput { get; set; }
        public string Keterangan { get; set; }
        public bool? Verifikasi { get; set; }
        public DateTime? WaktuVerifikasi { get; set; }
        public string FotoSk { get; set; }
        public DateTime? Waktuupdate { get; set; }
        public List<MutasiJabatanDetailDto> Detail { get; set; }
    }
}
