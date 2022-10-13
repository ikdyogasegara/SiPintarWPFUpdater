using System;
using System.Collections.Generic;
using System.Text;

namespace AppBusiness.Data.DTOs
{
    public class MasterJenisPenerimaanPengeluaranDto : ICloneable
    {
        public int? IdPdam { get; set; }
        public int? IdJenis { get; set; }
        public string KodeJenis { get; set; }
        public string NamaJenis { get; set; }
        public string TipeJenis { get; set; } = "-";
        public DateTime? WaktuUpdate { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
