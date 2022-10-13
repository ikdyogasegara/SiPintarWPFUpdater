using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterInPersediaanDto : ICloneable
    {
        public int? IdPdam { get; set; }
        public int? IdInPersediaan { get; set; }
        public int? IdJenis { get; set; }
        public string KodeJenisBarang { get; set; }
        public string NamaJenisBarang { get; set; }
        public int? IdKeperluan { get; set; }
        public string KodeKeperluan { get; set; }
        public string Keperluan { get; set; }
        public int? IdDebet { get; set; }
        public string KodeDebet { get; set; }
        public string NamaDebet { get; set; }
        public int? IdKredit { get; set; }
        public string KodeKredit { get; set; }
        public string NamaKredit { get; set; }
        public int? Aktiva { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public DateTime? WaktuUpdate { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
