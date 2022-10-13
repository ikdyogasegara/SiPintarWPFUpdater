using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class MasterPdamDto
    {
        public int? IdPdam { get; set; }
        public string NamaPdam { get; set; }
        public string Provinsi { get; set; }
        public string Kota { get; set; }
        public string AlamatLengkap { get; set; }
        public string Tipe { get; set; } = "basic";
        public DateTime? WaktuUpdate { get; set; }
        public List<MasterPdamDetailDto> Detail { get; set; }
    }

    public class MasterPdamDetailDto
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
