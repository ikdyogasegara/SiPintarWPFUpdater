using System;

namespace AppBusiness.Data.DTOs
{
    public class RiwayatPostingDto
    {
        public int Id { get; set; }
        public int? IdPdam { get; set; }
        public string NamaPdam { get; set; }
        public int? IdPeriode { get; set; }
        public int? KodePeriode { get; set; }
        public string NamaPeriode { get; set; }
        public int? TahunPeriodeDRD { get; set; }
        public string Jenis { get; set; }
        public int? TahunPosting { get; set; }
        public DateTime? WaktuPosting { get; set; }
        public int? IdUser { get; set; }
        public string Nama { get; set; }
        public string Catatan { get; set; }
        public bool? FlagHapus { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
