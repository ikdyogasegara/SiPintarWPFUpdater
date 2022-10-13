using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterTipePerkiraanDto
    {
        public int? IdPdam { get; set; }
        public int? IdTipe { get; set; }
        public string KodeTipe { get; set; }
        public string NamaTipe { get; set; }
        public string Header { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public DateTime? WaktuUpdate { get; set; }
    }
}
