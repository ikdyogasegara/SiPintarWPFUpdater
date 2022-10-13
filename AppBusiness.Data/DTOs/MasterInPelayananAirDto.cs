using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterInPelayananAirDto : ICloneable
    {
        public int? IdPdam { get; set; }
        public int? IdInPelayananAir { get; set; }
        public int? IdWilayah { get; set; }
        public string KodeWilayah { get; set; }
        public string NamaWilayah { get; set; }
        public int? IdGolongan { get; set; }
        public string KodeGolongan { get; set; }
        public string NamaGolongan { get; set; }
        public int? IdPerkiraan3Debet { get; set; }
        public string KodePerkiraan3Debet { get; set; }
        public string NamaPerkiraan3Debet { get; set; }
        public int? IdPerkiraan3Kredit { get; set; }
        public string KodePerkiraan3Kredit { get; set; }
        public string NamaPerkiraan3Kredit { get; set; }
        public bool? FlagPembentukRekair { get; set; }
        public string Keterangan { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public DateTime? WaktuUpdate { get; set; }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
