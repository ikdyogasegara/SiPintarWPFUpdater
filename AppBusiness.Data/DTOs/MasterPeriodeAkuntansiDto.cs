using System;
using System.Collections.Generic;
using System.Text;

namespace AppBusiness.Data.DTOs
{
    public class MasterPeriodeAkuntansiDto
    {
        public int? IdPdam { get; set; }
        public int? IdPeriode { get; set; }
        public int? Tahun { get; set; }
        public int? KodePeriode { get; set; }
        public string NamaPeriode { get; set; }
        public bool? FlagJra { get; set; }
        public DateTime? WaktuUpdateJra { get; set; }
        public bool? FlagJrna { get; set; }
        public DateTime? WaktuUpdateJrna { get; set; }
        public bool? FlagJpk { get; set; }
        public DateTime? WaktuUpdateJpk { get; set; }
        public bool? FlagJpbik { get; set; }
        public DateTime? WaktuUpdateJpbik { get; set; }
        public bool? FlagDhjp { get; set; }
        public DateTime? WaktuUpdateDhjp { get; set; }
        public bool? FlagJbk { get; set; }
        public DateTime? WaktuUpdateJbk { get; set; }
        public bool? FlagJPenyusutan { get; set; }
        public DateTime? WaktuUpdateJPenyusutan { get; set; }
        public bool? FlagJKoreksi { get; set; }
        public DateTime? WaktuUpdateJKoreksi { get; set; }
        public bool? FlagJu { get; set; }
        public DateTime? WaktuUpdateJu { get; set; }
        public bool? FlagTutupBuku { get; set; }
        public DateTime? WaktuUpdateTutupBuku { get; set; }
        public bool? FlagHapus { get; set; }
        public DateTime? WaktuUpdate { get; set; }

    }
}
