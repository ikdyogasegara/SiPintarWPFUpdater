using System;
using System.Collections.Generic;
using System.Text;

namespace AppBusiness.Data.DTOs
{
    public class NeracaDto
    {
        public int? IdPdam { get; set; }
        public int? IdNeraca { get; set; }
        public int? IdPeriode { get; set; }
        public int? KodePeriode { get; set; }
        public string NamaPeriode { get; set; }
        public int? Tahun { get; set; }
        public string NomorAktiva { get; set; }
        public string NamaAktiva { get; set; }
        public string KodeAktiva { get; set; }
        public decimal? JumlahAktiva { get; set; }
        public string NomorPassiva { get; set; }
        public string NamaPassiva { get; set; }
        public string KodePassiva { get; set; }
        public decimal? JumlahPassiva { get; set; }
        public bool? FlagHapus { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
