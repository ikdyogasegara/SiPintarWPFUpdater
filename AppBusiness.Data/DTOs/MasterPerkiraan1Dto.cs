using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterPerkiraan1Dto : ICloneable
    {
        public int? IdPdam { get; set; }
        public int? IdPerkiraan1 { get; set; }
        public string KodePerkiraan1 { get; set; }
        public string NamaPerkiraan1 { get; set; }
        public DateTime? WaktuUpdate { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
