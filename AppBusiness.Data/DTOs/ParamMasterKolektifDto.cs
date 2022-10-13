using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterKolektifDto
    {
        public int? IdPdam { get; set; }
        public int? IdKolektif { get; set; }
        public string KodeKolektif { get; set; }
        public string NamaKolektif { get; set; }
        public string Keterangan { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterKolektifFilterDto : ParamMasterKolektifDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
