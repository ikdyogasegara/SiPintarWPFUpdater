using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterPerkiraan3Dto : ICloneable
    {
        public int? IdPdam { get; set; }
        public int? IdPerkiraan3 { get; set; }
        public string KodePerkiraan3 { get; set; }
        public string NamaPerkiraan3 { get; set; }
        public int? IdPerkiraan2 { get; set; }
        public string KodePerkiraan2 { get; set; }
        public string NamaPerkiraan2 { get; set; }
        public int? IdJenisVoucher { get; set; }
        public string KodeJenisVoucher { get; set; }
        public string NamaJenisVoucher { get; set; }
        public int? IdGolAktiva { get; set; }
        public string KodeGolAktiva { get; set; }
        public string NamaGolAktiva { get; set; }
        public int? IdAkunEtap { get; set; }
        public string KodeAkunEtap { get; set; }
        public string NamaAkunEtap { get; set; }
        public string HeaderAkunEtap { get; set; }
        public int? IdLHKMaster { get; set; }
        public int? IdLPUMaster { get; set; }
        public int? IdEkuitasMaster { get; set; }
        public int? IdLabaRugiMaster { get; set; }
        public DateTime? WaktuUpdate { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }

    }
}
