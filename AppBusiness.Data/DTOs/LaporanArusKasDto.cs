using System;
using System.Collections.Generic;
using System.Text;

namespace AppBusiness.Data.DTOs
{
    public class LaporanArusKasDto
    {
        public int? IdPdam { get; set; }
        public int? IdArusKas { get; set; }
        public int? IdPeriode { get; set; }
        public int? KodePeriode { get; set; }
        public string NamaPeriode { get; set; }
        public string KodeArusKas { get; set; }
        public int? Tahun { get; set; }
        public string NomorArusKas { get; set; }
        public string NomorArusKas1 { get; set; }
        public string HeaderArusKas { get; set; }
        public string HeaderArusKas1 { get; set; }
        public string FooterArusKas { get; set; }
        public string FooterArusKas1 { get; set; }
        public string Uraian { get; set; }
        public decimal? BulanIni { get; set; }
        public decimal? BulanLalu { get; set; }
        public bool? FlagHapus { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
