using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterKelainanDto
    {
        public int? IdPdam { get; set; }
        public int? IdKelainan { get; set; }
        public string KodeKelainan { get; set; }
        public string Kelainan { get; set; }
        public string JenisKelainan { get; set; } //enum Biasa, Tindak Lanjut
        public string Deskripsi { get; set; }
        public int? Posisi { get; set; }
        public bool? Blokir { get; set; } = false;
        public bool? Status { get; set; } = false;
        public DateTime? WaktuUpdate { get; set; }
    }
}
