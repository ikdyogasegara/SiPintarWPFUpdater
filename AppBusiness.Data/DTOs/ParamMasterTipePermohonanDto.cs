using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterTipePermohonanDto
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public int? IdTipePermohonan { get; set; }
        public string KodeTipePermohonan { get; set; }
        public string NamaTipePermohonan { get; set; }
        public int? IdJenisNonAir { get; set; }
        public string Kategori { get; set; }
        public bool? FlagPelangganAir { get; set; }
        public bool? FlagPelangganLimbah { get; set; }
        public bool? FlagPelangganLltt { get; set; }
        public bool? FlagNonPelanggan { get; set; }
        public bool? Step_Spk { get; set; }
        public bool? Step_Rab { get; set; }
        public bool? Step_BeritaAcara { get; set; }
        public bool? Step_Verifikasi { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public List<ParamMasterTipePermohonanDetailDto> DetailParameter { get; set; }
    }

    public class ParamMasterTipePermohonanDetailDto
    {
        public string Parameter { get; set; }
        public string TipeData { get; set; }
        public int? IdListData { get; set; }
        public bool? IsRequired { get; set; } = true;
    }

    public class ParamMasterTipePermohonanFilterDto : ParamMasterTipePermohonanDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
