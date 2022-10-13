﻿using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterPerkiraan2Dto : ICloneable
    {
        public int? IdPdam { get; set; }
        public int? IdPerkiraan2 { get; set; }
        public string KodePerkiraan2 { get; set; }
        public string NamaPerkiraan2 { get; set; }
        public int? IdPerkiraan1 { get; set; }
        public string KodePerkiraan1 { get; set; }
        public string NamaPerkiraan1 { get; set; }
        public int? IdNeracaMaster { get; set; }
        public int? IdArusKasMaster { get; set; }
        public DateTime? WaktuUpdate { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
