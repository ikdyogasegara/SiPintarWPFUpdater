using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterKelainanDto
    {
        public int? IdPdam { get; set; }
        public int? IdKelainan { get; set; }
        public string KodeKelainan { get; set; }
        public string Kelainan { get; set; }
        public string JenisKelainan { get; set; }
        public string Deskripsi { get; set; }
        public int? Posisi { get; set; } = 0;
        public bool? Blokir { get; set; } = false;
        public bool? Status { get; set; } = false;
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; } = 0;
    }

    public class ParamMasterKelainanFilterDto : ParamMasterKelainanDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
