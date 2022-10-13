using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterKolektifDto
    {
        public int? IdPdam { get; set; }
        public int? IdKolektif { get; set; }
        public string KodeKolektif { get; set; }
        public string NamaKolektif { get; set; }
        public string Keterangan { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
