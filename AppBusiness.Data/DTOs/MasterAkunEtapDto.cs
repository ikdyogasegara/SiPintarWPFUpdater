using System;
using System.Collections.Generic;
using System.Text;

namespace AppBusiness.Data.DTOs
{
    public class MasterAkunEtapDto : ICloneable
    {
        public int? IdPdam { get; set; }
        public int? IdAkunEtap { get; set; }
        public string KodeAkunEtap { get; set; }
        public string NamaAkunEtap { get; set; }
        public bool? FlagKelompokEtap { get; set; }
        public string TipeAkunEtap { get; set; } = "-";
        public DateTime? WaktuUpdate { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
