using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterParameterAkuntansiDto
    {
        public int? IdPdam { get; set; }
        public int? IdParameter { get; set; }
        public int? IdJenisParameter { get; set; }
        public int? IdPerkiraan { get; set; }
        public string KodePerkiraan { get; set; }
        public string NamaPerkiraan { get; set; }

        public int? IdPerkiraanAktiva { get; set; }

        public string KodePerkiraanAktiva { get; set; }
        public string NamaPerkiraanAktiva { get; set; }

        public string KodeParameter { get; set; }

        public string Keterangan { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public DateTime? WaktuUpdate { get; set; }
    }
}
